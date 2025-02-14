// Creature Creator - https://github.com/daniellochner/Creature-Creator
// Copyright (c) Daniel Lochner

using UnityEngine;

namespace DanielLochner.Assets.CreatureCreator
{
    public class BoxCreature : MonoBehaviour
    {
        #region Fields
        [SerializeField] private CreatureConstructor displayPrefab;
        [SerializeField] private Click click;
        [Space]
        [SerializeField] private Vector3 force;

        private Camera mainCamera;
        private CreatureConstructor creatureConstructor;
        #endregion

        #region Methods
        private void Awake()
        {
            mainCamera = Camera.main;
        }

        public void Spawn(CreatureData creatureData)
        {
            creatureConstructor = Instantiate(displayPrefab, transform.position, transform.rotation, transform);
            creatureConstructor.Construct(creatureData);

            #region Legless
            if (creatureConstructor.Legs.Count == 0)
            {
                Mesh bodyMesh = new Mesh();
                creatureConstructor.SkinnedMeshRenderer.BakeMesh(bodyMesh);
                float minY = Mathf.Infinity;
                foreach (Vector3 vertex in bodyMesh.vertices)
                {
                    if (vertex.y < minY)
                    {
                        minY = vertex.y;
                    }
                }
                creatureConstructor.Body.localPosition = Vector3.up * -minY;
            }
            #endregion

            this.InvokeOverTime(delegate (float progress)
            {
                transform.localScale = Vector3.one * Mathf.Lerp(0, 1, progress);
            }, 0.5f);
        }
        public void ReplaceWithRagdoll()
        {
            if (creatureConstructor.gameObject.activeSelf == false)
            {
                return;
            }
            creatureConstructor.gameObject.SetActive(false);
            click.enabled = false;

            CreatureConstructor ragdoll = creatureConstructor.GetComponent<CreatureRagdoll>().Generate();
            foreach (Transform bone in ragdoll.Bones)
            {
                Press press = bone.gameObject.AddComponent<Press>();
                press.OnPress.AddListener(delegate
                {
                    if (Physics.Raycast(RectTransformUtility.ScreenPointToRay(mainCamera, Input.mousePosition), out RaycastHit hitInfo))
                    {
                        Vector3 dir = (hitInfo.point - mainCamera.transform.position).normalized;
                        hitInfo.rigidbody.AddForce((dir * force.z) + (Vector3.up * force.y), ForceMode.Impulse);
                    }
                });
            }

            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            foreach (LimbConstructor limb in ragdoll.Limbs)
            {
                limb.gameObject.layer = limb.FlippedLimb.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            }
        }
        #endregion
    }
}