// Creature Creator - https://github.com/daniellochner/Creature-Creator
// Copyright (c) Daniel Lochner

using Unity.Netcode;

namespace DanielLochner.Assets.CreatureCreator
{
    public class CreatureNonPlayerLocal : CreatureNonPlayer
    {
        public override void Setup()
        {
            base.Setup();
            Spawner.Spawn();
        }

        public override void OnDie()
        {
            base.OnDie();
            GetComponent<NetworkObject>().Despawn();
        }
    }
}