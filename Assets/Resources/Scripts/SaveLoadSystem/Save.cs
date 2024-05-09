using System;
using System.Collections.Generic;
using Resources.Scripts.DialogSystem;

namespace Resources.Scripts.SaveLoadSystem
{
    [Serializable]
    public class Save
    {
        private Dictionary<string, DialogStatus> _dialogs;
        public Dictionary<PlotInfluenceType, int> PlotInfluences { get; private set; }

        public void SaveDialogs(Dialog[] dialogs)
        {
            foreach (var dialog in dialogs)
            {
                _dialogs.Add("Vova", dialog.status);   
            }
        }

        public void SavePlotInfluences(Dictionary<PlotInfluenceType, int> plotInfluences)
        {
            PlotInfluences = plotInfluences;
        }
    }
}
