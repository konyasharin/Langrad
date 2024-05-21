using System;
using UnityEngine;

namespace Resources.Scripts.UI
{
    [Serializable]
    public struct CanvasMove
    {
        public float totalMovementTime;
        public Vector2 fromPosition;
        public Vector2 toPosition;
    }
}
