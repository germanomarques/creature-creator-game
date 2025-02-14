using System;
using Unity.Netcode;
using UnityEngine.SceneManagement;

namespace DanielLochner.Assets.CreatureCreator
{
    public class TeleportManager : MonoBehaviourSingleton<TeleportManager>
    {
        #region Fields
        public static CreatureData dataBuffer;
        #endregion

        #region Methods
        public virtual void TeleportTo(string map, CreatureData data)
        {
            WorldManager.Instance.IsUsingTeleport = true;

            dataBuffer = data;

            if (NetworkManager.Singleton.IsHost)
            {
                NetworkManager.Singleton.SceneManager.LoadScene(map, LoadSceneMode.Single);
            }
        }

        public virtual void OnLeave(string prevScene, string nextScene)
        {
        }
        public virtual void OnEnter(string prevScene, string nextScene)
        {
            EditorManager.Instance.Load(dataBuffer);
            dataBuffer = null;
        }
        #endregion
    }
}