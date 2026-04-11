using UnityEngine;
using UnityEngine.Video;

public class MenuController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject menuOpcoes, menuConfig, rawImage;
    private Animator animatorRawImage, animatorMenuOpcoes;
    void Start()
    {
        animatorRawImage = rawImage.GetComponent<Animator>();
        rawImage.SetActive(false);
        animatorMenuOpcoes = menuOpcoes.GetComponent<Animator>();
        menuOpcoes.SetActive(false);
    }

    
    void Update()
    {
        if (!videoPlayer.isPlaying && Input.anyKeyDown)
        { 
            videoPlayer.Play();
            rawImage.SetActive(true);
            menuOpcoes.SetActive(true);
            animatorRawImage.SetTrigger("FadeIn");
            animatorMenuOpcoes.SetTrigger("FadeInOpcoes");
        }
    }

    public void Config()
    {
        menuOpcoes.SetActive(false);
        menuConfig.SetActive(true);
    }

    public void ReturnMenuOpcoes()
    {
        menuOpcoes.SetActive(true);
        menuConfig.SetActive(false);
    }
}