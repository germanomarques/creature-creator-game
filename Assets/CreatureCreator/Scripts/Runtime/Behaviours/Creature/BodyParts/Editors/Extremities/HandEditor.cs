﻿// Creature Creator - https://github.com/daniellochner/Creature-Creator
// Copyright (c) Daniel Lochner

namespace DanielLochner.Assets.CreatureCreator
{
    public class HandEditor : ExtremityEditor
    {
        #region Methods
        public override bool CanConnectToLimb(LimbConstructor limb)
        {
            return base.CanConnectToLimb(limb) && limb.Limb is Arm;
        }
        #endregion
    }
}