using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class ConfigsModel : MonoBehaviour
{

    public Resolution Resolution { get; set; }

    public LimitFPS LimitFPS { get; set; }

    public Quality Quality { get; set; }

    public bool WindowsMode { get; set; }

    public bool MusicVolume { get; set; }

    public bool GlobalVolume { get; set; }

    public bool EffectsVolume { get; set; }

    public bool AutoSave { get; set; }
}

[System.Serializable]
public enum Quality
{
    Low,
    Medium,
    High,
}

[System.Serializable]
public class Resolution
{
    public int Width { get; set; }
    public int Height { get; set; }
}

[System.Serializable]
public class LimitFPS
{
    public bool Limit { get; set; }
    public int FPS { get; set; }
}