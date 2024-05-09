using System;

namespace Resources.Scripts.DialogSystem
{
    [Serializable]
    public struct Choice
    {
        public string text;
        public PlotInfluence[] plotInfluences;
        public DialogToggle[] dialogToggles;
    }
}
