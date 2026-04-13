using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class ConfigsModel : MonoBehaviour
{
    // --- Modificadores de classes ---
    public Resolution Resolution { get; set; }

    public LimitFPS LimitFPS { get; set; }

    // --- Modificadores de acesso enumerador ---
    public Quality Quality { get; set; }

    // --- Modificadores de acesso booleanos ---

    public bool WindowsMode { get; set; }

    public bool MusicVolume { get; set; }

    public bool GlobalVolume { get; set; }

    public bool EffectsVolume { get; set; }

    public bool AutoSave { get; set; }
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
    public int Height { get; set; }
}

[System.Serializable]
// --- Modificadores de acesso booleanos para limite de FPS ---
public class LimitFPS
{
    public bool Limit { get; set; }
    public int FPS { get; set; }
}