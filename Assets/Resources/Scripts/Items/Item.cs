using UnityEngine;

namespace Resources.Scripts.Items
{
    public abstract class Item: ScriptableObject
    {
        public new string name;
        [SerializeField]
        public Sprite sprite;
        public abstract void Use();
    }
}