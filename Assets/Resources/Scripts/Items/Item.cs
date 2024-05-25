using System;
using Resources.Scripts.ItemsData;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Scripts.Items
{
    public abstract class Item
    {
        public Sprite Sprite;
        public abstract void Use();
    }
}
