using UnityEngine;

public class SceneController : MonoBehaviour
{
    private AudioSource[] audioSources;

    void Start()
    {
        ApplyAudio();
    }

    public void ApplyAudio()
    {
        audioSources = GameObject.FindObjectsOfType<AudioSource>();

        foreach (AudioSource audio in audioSources)
        {
            float baseVolume = 1f;

            if (audio.CompareTag("music"))
                baseVolume = SceneConfigs.musicVolume / 100f;

            else if (audio.CompareTag("effect"))
                baseVolume = SceneConfigs.effectsVolume / 100f;

            audio.volume = baseVolume * (SceneConfigs.globalVolume / 100f);
        }
    }
}