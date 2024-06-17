using System;
using UnityEngine;

namespace Resources.Scripts.Actors.Player.ManaSystem
{
    [Serializable]
    public class ManaSettings
    {
        [Min(1)] public int maxMana;
        [Min(1)] public int restoreValue;
        [Min(0.5f)] public float restoreTime;
    }
}