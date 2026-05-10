using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class ConfigsModel
{
    // --- Modificadores de classes ---
    public Resolution Resolution;

    public LimitFPS LimitFPS;

    // --- Modificadores de acesso enumerador ---
    public Quality Quality;

    // --- Modificadores de acesso booleanos ---

    public bool WindowsMode;

    public bool MusicVolume;

    public bool GlobalVolume;

    public bool EffectsVolume;

    public bool AutoSave;

    // --- Modificadores de acesso para classes e variáveis ---
    public float GlobalVolumeValue;
    public float MusicVolumeValue;
    public float EffectsVolumeValue;
}

[System.Serializable]
// --- Níveis gráficos do game ---
public enum Quality
{
    Low,
    Medium,
    High,
}

[System.Serializable]
// --- Resoluções do game ---
public class Resolution
{
    public int Width { get; set; }
    public int Height;
}

[System.Serializable]
// --- Modificadores de acesso booleanos para limite de FPS ---
public class LimitFPS
{
    public bool Limit;
    public int FPS;
}