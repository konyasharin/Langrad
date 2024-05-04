using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Save
{
    private Dictionary<string, DialogStatus> _dialogs;
    private Dictionary<PlotInfluence, int> _plotInfluences;
    public Dictionary<PlotInfluence, int> PlotInfluences { get; }

    public void SaveDialogs(Dialog[] dialogs)
    {
        foreach (var dialog in dialogs)
        {
            _dialogs.Add("Vova", dialog.dialogStatus);   
        }
    }

    public void SavePlotInfluences(Dictionary<PlotInfluence, int> plotInfluences)
    {
        _plotInfluences = new Dictionary<PlotInfluence, int>(plotInfluences);
    }
}
