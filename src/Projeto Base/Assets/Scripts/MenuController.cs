using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using static UnityEngine.Rendering.STP;

public class MenuController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject MenuInicial, MenuConfig, rawImage, Tutorial, Creditos, MenuFases;

    private Animator animatorRawImage, animatorMenuInicial, animatorMenuConfig, animatorTutorial, animatorCreditos, animatorMenuFases;

    public TMP_Dropdown resolution;
    public TMP_InputField textFPS;
    public Toggle LimitFPSToggle;
    public Slider globalVolumeSlider, musicVolumeSlider, effectsVolumeSlider;
    public AudioSource videoAudioSource;
    private SceneController sceneController;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        ApplyConfigs();

        videoPlayer.Prepare();

        animatorRawImage = rawImage.GetComponent<Animator>();
        rawImage.SetActive(false);

        animatorMenuInicial = MenuInicial.GetComponent<Animator>();
        MenuInicial.SetActive(false);

        animatorMenuConfig = MenuConfig.GetComponent<Animator>();
        MenuConfig.SetActive(false);

        animatorTutorial = Tutorial.GetComponent<Animator>();
        Tutorial.SetActive(false);

        animatorCreditos = Creditos.GetComponent<Animator>();
        Creditos.SetActive(false);

        animatorMenuFases = MenuFases.GetComponent<Animator>();
        MenuFases.SetActive(false);

        // Inicia automaticamente após 3 segundos
        StartCoroutine(StartMenuAfterDelay(3f));

        sceneController = FindObjectOfType<SceneController>();
    }

    // Coroutine para delay
    IEnumerator StartMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        videoPlayer.Play();

        ApplyVideoVolume();

        rawImage.SetActive(true);
        MenuInicial.SetActive(true);

        animatorRawImage.SetTrigger("FadeIn");
        animatorMenuInicial.SetTrigger("FadeInInicial");
        animatorMenuConfig.SetTrigger("MenuConfigANM");
        animatorTutorial.SetTrigger("TutorialSlideIn");
        animatorCreditos.SetTrigger("CreditosSlideIn");
        animatorMenuFases.SetTrigger("MenuFasesSlideIn");
    }

    public void menuConfig()
    {
        LoadConfigs();

        MenuInicial.SetActive(false);
        MenuConfig.SetActive(true);
    }

    public void ReturnMenuInicial()
    {
        MenuInicial.SetActive(true);
        MenuConfig.SetActive(false);
        Tutorial.SetActive(false);
        Creditos.SetActive(false);
        MenuFases.SetActive(false);
    }

    public void GoToTutorial()
    {
        MenuInicial.SetActive(false);
        Tutorial.SetActive(true);
    }
    public void GoToCredits()
    {
        MenuInicial.SetActive(false);
        Creditos.SetActive(true);
    }

    public void GoToMenuFases()
    {
        MenuInicial.SetActive(false);
        MenuFases.SetActive(true);
    }
    public void Salvar()
    {
        SaveConfigs();
        ApplyConfigs();

        if (sceneController != null)
            sceneController.ApplyAudio(); 

        ApplyVideoVolume();

        ReturnMenuInicial();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void ApplyVideoVolume()
    {
        if (videoPlayer != null)
        {
            float volume = (SceneConfigs.musicVolume / 100f) * (SceneConfigs.globalVolume / 100f);
            videoAudioSource.volume = volume;

            Debug.Log("Volume do vídeo: " + volume);

            videoPlayer.SetDirectAudioVolume(0, volume);
        }
    }

    private void ApplyConfigs()
    {
        var configs = LoadConfigs();

        if (configs != null)
        {
            QualitySettings.vSyncCount = 0;
            Screen.SetResolution(configs.Resolution.Width,configs.Resolution.Height,FullScreenMode.ExclusiveFullScreen);
            Debug.Log("Resolução atual: " + Screen.width + "x" + Screen.height);

            if (configs.LimitFPS.Limit)
            {
                Application.targetFrameRate = configs.LimitFPS.FPS;
            }
            else
            {
                Application.targetFrameRate = Screen.currentResolution.refreshRateRatio.value > 0
                    ? (int)Screen.currentResolution.refreshRateRatio.value
                    : 60;
            }

            Debug.Log("FPS Limit aplicado: " + Application.targetFrameRate);
            Debug.Log("VSync: " + QualitySettings.vSyncCount);

            //vomules
            SceneConfigs.globalVolume = configs.GlobalVolumeValue;
            SceneConfigs.musicVolume = configs.MusicVolumeValue;
            SceneConfigs.effectsVolume = configs.EffectsVolumeValue;
        }
    }

    private ConfigsModel LoadConfigs()
    {
        try
        {
            var path = Application.persistentDataPath + "/ConfigData.save";

            if (!File.Exists(path))
                return null;

            var binaryFormatter = new BinaryFormatter();
            ConfigsModel configs;

            using (var file = File.OpenRead(path))
            {
                configs = (ConfigsModel)binaryFormatter.Deserialize(file);
            }

            if (configs != null)
            {
                var option = resolution.options
                    .Where(x => x.text == $"{configs.Resolution.Width}x{configs.Resolution.Height}")
                    .FirstOrDefault();

                if (option != null)
                {
                    resolution.value = resolution.options.IndexOf(option);
                }

                if (configs.LimitFPS != null)
                {
                    textFPS.text = configs.LimitFPS.FPS.ToString();
                    LimitFPSToggle.isOn = configs.LimitFPS.Limit;
                }

                globalVolumeSlider.value = configs.GlobalVolumeValue;
                musicVolumeSlider.value = configs.MusicVolumeValue;
                effectsVolumeSlider.value = configs.EffectsVolumeValue;
            }

            return configs;
        }
        catch (Exception)
        {
            return null;
        }
    }

    private void SaveConfigs()
    {
        var configs = new ConfigsModel();

        int FPS;

        if (!int.TryParse(textFPS.text, out FPS))
        {
            FPS = 60;
        }

        configs.LimitFPS = new LimitFPS()
        {
            FPS = LimitFPSToggle.isOn ? FPS : 0,
            Limit = LimitFPSToggle.isOn
        };

        configs.GlobalVolumeValue = globalVolumeSlider.value;
        configs.MusicVolumeValue = musicVolumeSlider.value;
        configs.EffectsVolumeValue = effectsVolumeSlider.value;

        var resolutionModel = new Resolution();

        switch (resolution.value)
        {
            case 0:
                resolutionModel.Width = 1920;
                resolutionModel.Height = 1080;
                break;
            case 1:
                resolutionModel.Width = 2560;
                resolutionModel.Height = 1440;
                break;
            case 2:
                resolutionModel.Width = 3840;
                resolutionModel.Height = 2160;
                break;
        }

        configs.Resolution = resolutionModel;

        var path = Application.persistentDataPath + "/ConfigData.save";

        var binaryFormatter = new BinaryFormatter();

        using (var file = File.Create(path))
        {
            binaryFormatter.Serialize(file, configs);
        }
    }
}