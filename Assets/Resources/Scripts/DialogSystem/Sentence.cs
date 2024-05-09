using System;
using UnityEngine;

namespace Resources.Scripts.DialogSystem
{
    [Serializable]
    public struct Sentence
    {
        public string name;
        [TextArea]
        public string text;
    }
}
