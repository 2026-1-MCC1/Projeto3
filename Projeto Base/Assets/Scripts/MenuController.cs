using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject MenuInicial, MenuConfig, rawImage, Tutorial;
    private Animator animatorRawImage, animatorMenuInicial, animatorMenuConfig, animatorTutorial;

    public Dropdown resolution, quality;
    public InputField textFPS;
    public Toggle LimitFPS, windowsMode, musicVolume, globalVolume, effectsVolume, autoSave;
    public Slider globalVolumeSlider, musicVolumeSlider, effectsVolumeSlider;

    void Start()
    {
        videoPlayer.Prepare();
        animatorRawImage = rawImage.GetComponent<Animator>();
        rawImage.SetActive(false);
        animatorMenuInicial = MenuInicial.GetComponent<Animator>();
        MenuInicial.SetActive(false);
        animatorMenuConfig = MenuConfig.GetComponent<Animator>();
        MenuConfig.SetActive(false);
        animatorTutorial = Tutorial.GetComponent<Animator>();
        Tutorial.SetActive(false);
    }

    void Update()
    {
        if (!videoPlayer.isPlaying && Input.anyKeyDown)
        { 
            videoPlayer.Play();
            rawImage.SetActive(true);
            MenuInicial.SetActive(true);
            animatorRawImage.SetTrigger("FadeIn");
            animatorMenuInicial.SetTrigger("FadeInInicial");
            animatorMenuConfig.SetTrigger("MenuConfigANM");
            animatorTutorial.SetTrigger("TutorialSlideIn");
        }
    }

    public void menuConfig()
    {
        MenuInicial.SetActive(false);
        MenuConfig.SetActive(true);
    }

    public void ReturnMenuInicial()
    {
        MenuInicial.SetActive(true);
        MenuConfig.SetActive(false);
        Tutorial.SetActive(false);
    }

    public void GoToTutorial()
    {
        MenuInicial.SetActive(false);
        Tutorial.SetActive(true);
    }
    public void Salvar()
    {
       SaveConfigs();
        ReturnMenuInicial();
    }
    public void ExitGame() 
    {
        Application.Quit();
    }

private void SaveConfigs() 
    {
        var configs = new ConfigsModel()
        {
            AutoSave = autoSave.isOn,
            WindowsMode = windowsMode.isOn,
            MusicVolume = musicVolume.isOn,
            GlobalVolume = globalVolume.isOn,
            EffectsVolume = effectsVolume.isOn,
            LimitFPS = new LimitFPS()
            {
                FPS = int.Parse(textFPS.text),
                Limit = LimitFPS.isOn
            },
            Quality = (Quality)quality.value      
        };


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

        var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Documents/";

        var binaryFormatter = new BinaryFormatter();
        var file = File.Create(path + "ConfigData.save");

        binaryFormatter.Serialize(file, configs);
        file.Close();
    }
}