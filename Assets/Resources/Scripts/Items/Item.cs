using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Scripts.Items
{
    public abstract class Item
    {
        public Sprite Sprite;
        public abstract void Use();

        public virtual bool IsActivationAvailable()
        {
            return true;
        }
    }
}
