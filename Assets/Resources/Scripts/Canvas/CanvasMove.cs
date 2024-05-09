using System;
using UnityEngine;

namespace Resources.Scripts.Canvas
{
    [Serializable]
    public struct CanvasMove
    {
        public float totalMovementTime;
        public Vector2 fromPosition;
        public Vector2 toPosition;
    }
}
