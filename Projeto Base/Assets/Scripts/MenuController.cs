using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

// --- Modificadores de acesso para classes e variáveis ---
public class MenuController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject MenuInicial, MenuConfig, rawImage, Tutorial;
    private Animator animatorRawImage, animatorMenuInicial, animatorMenuConfig, animatorTutorial;

    public TMP_Dropdown resolution;
    public TMP_InputField textFPS;
    public Toggle LimitFPSToggle;
    public Slider globalVolumeSlider, musicVolumeSlider, effectsVolumeSlider;


    // --- Prepara o vídeo, pega os animators e desativa os elementos da UI no início ---
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


    // --- Ao pressionar qualquer tecla, inicia o vídeo, ativa a UI e dispara as animações ---
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


    // --- Método público que não retorna nenhum valor ---
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


    // --- Método privado que não retorna nenhum valor | este método carrega as configurações do jogo (objetos) salvas anteriormente  ---
    private void LoadConfigs()
    {
        try
        {
            var path = Application.persistentDataPath + "/ConfigData.save";

            if (!File.Exists(path))
                return;

            var binaryFormatter = new BinaryFormatter();

            ConfigsModel configs;

            using (var file = File.OpenRead(path))
            {
                configs = (ConfigsModel)binaryFormatter.Deserialize(file);
            }

            if (configs != null)
            {
                var option = resolution.options.Where(x => x.text == $"{configs.Resolution.Width}x{configs.Resolution.Height}").FirstOrDefault();

                if (option != null)
                {
                    resolution.value = resolution.options.IndexOf(option);
                }

                if (configs.LimitFPS != null)
                {
                    textFPS.text = configs.LimitFPS.FPS.ToString();
                    LimitFPSToggle.isOn = configs.LimitFPS.Limit;
                }

                AudioListener.volume = configs.GlobalVolumeValue;
                globalVolumeSlider.value = configs.GlobalVolumeValue;
                musicVolumeSlider.value = configs.MusicVolumeValue;
                effectsVolumeSlider.value = configs.EffectsVolumeValue;
            }
        }
        catch(Exception ex)
        {
            Debug.LogError("Erro ao carregar as configurações: " + ex.ToString()); 
        }
    }


    // --- Método privado que não retorna nenhum valor | este método salva as configurações do jogo (objetos) em uma pasta reservada que pode ser carregada posteriormente ---
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
            FPS = FPS,
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