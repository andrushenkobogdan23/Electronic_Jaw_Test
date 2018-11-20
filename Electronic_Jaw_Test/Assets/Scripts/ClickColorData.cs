using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "SettingsObjects/ClickColorData", order = 1)]
public class ClickColorData : ScriptableObject
{
    public FigureSettings[] Settings;
}

[Serializable]
public class FigureSettings
{
    public string prefabName;
    public float SecondsToChageColor;
    public ClicsColorMap[] clickColorMaping;

}

[Serializable]
public class ClicsColorMap
{
    public int minClick;
    public int maxClck;
    public Color color;
}
