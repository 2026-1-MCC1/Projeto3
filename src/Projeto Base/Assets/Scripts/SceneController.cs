using UnityEngine;

public class SceneController : MonoBehaviour
{
   private AudioSource[] audioSources;

    void Start()
    {
        audioSources = GameObject.FindObjectsOfType<AudioSource>();

        foreach (AudioSource audio in audioSources)
        {
            if (audio.gameObject.tag == "music")
                audio.volume = SceneConfigs.musicVolume / 100;

            if (audio.gameObject.tag == "effect")
                audio.volume = SceneConfigs.effectsVolume / 100;

            audio.volume = Mathf.Clamp(audio.volume, 0, SceneConfigs.globalVolume / 100);
        }
    }
}