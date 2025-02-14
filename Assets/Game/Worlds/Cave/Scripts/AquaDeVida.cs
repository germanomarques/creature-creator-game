using UnityEngine;

namespace DanielLochner.Assets.CreatureCreator
{
    public class AquaDeVida : MonoBehaviour
    {
        [SerializeField] private GameObject healFX;
        public void Heal(CreaturePlayerLocal player)
        {
            player.Health.HealthPercentage = 1f;
            Instantiate(healFX, player.transform.position, Quaternion.identity, Dynamic.Transform);
        }
    }
}