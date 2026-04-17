using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
<<<<<<< HEAD
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
=======
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
>>>>>>> main
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
<<<<<<< HEAD
    public int Height { get; set; }
=======
    public int Height;
>>>>>>> main
}

[System.Serializable]
// --- Modificadores de acesso booleanos para limite de FPS ---
public class LimitFPS
{
<<<<<<< HEAD
    public bool Limit { get; set; }
    public int FPS { get; set; }
=======
    public bool Limit;
    public int FPS;
>>>>>>> main
}