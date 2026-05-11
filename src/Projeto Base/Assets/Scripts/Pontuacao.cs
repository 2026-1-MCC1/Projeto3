using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pontuacao : MonoBehaviour
{
   

    [Header("Configuração")]
    public int pontos = 0;
    public int totalPontos = 47;

    [Header("UI")]
    public TextMeshProUGUI textoPontos;

    [Header("Menu")]
    public string nomeCenaMenu = "Menu";


    void Start()
    {
        AtualizarUI();
    }

    public void AdicionarPonto()
    {
        pontos++;

        Debug.Log("Pontos: " + pontos);

        AtualizarUI();

        if (pontos >= totalPontos)
        {
            Vitoria();
        }
    }

    void AtualizarUI()
    {
        if (textoPontos != null)
        {
            textoPontos.text = pontos + " / " + totalPontos;
        }
    }

    void Vitoria()
    {
        Debug.Log("VITÓRIA!");

        Invoke(nameof(VoltarMenu), 3f);
    }

    void VoltarMenu()
    {
        SceneManager.LoadScene(nomeCenaMenu);
    }
}