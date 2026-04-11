using UnityEngine;
using UnityEngine.Video;

public class MenuController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject MenuInicial, MenuConfig, rawImage;
    private Animator animatorRawImage, animatorMenuInicial, animatorMenuConfig;
    void Start()
    {
        animatorRawImage = rawImage.GetComponent<Animator>();
        rawImage.SetActive(false);
        animatorMenuInicial = MenuInicial.GetComponent<Animator>();
        MenuInicial.SetActive(false);
        animatorMenuConfig = MenuConfig.GetComponent<Animator>();
        MenuConfig.SetActive(false);
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
        // Lógica para salvar as configurações
    }
}