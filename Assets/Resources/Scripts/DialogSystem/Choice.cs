using System;

[Serializable]
public struct Choice
{
    public string text;
    public PlotInfluence[] plotInfluences;
    public DialogToggle[] dialogToggles;
}
