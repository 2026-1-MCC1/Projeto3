using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EsferaPrincipal : MonoBehaviour
{
    [Header("Rotação")]
    public float velocidadeRotacao = 100f;
    public float velocidadeRotacaoTensa = 150f;
    public float tempoParaMudar = 2f;

    [Header("Pontuação")]
    public int pontos = 0;
    public int totalPontos = 47;

    [Header("UI")]
    public TextMeshProUGUI textoPontos;

    [Header("Menu")]
    public string nomeCenaMenu = "Menu";

    private Vector3 direcao;
    private float tempo;
    private bool modoTenso = false;

    void Start()
    {
        MudarDirecao();
        AtualizarUI();
    }

    void Update()
    {
        transform.Rotate(direcao * velocidadeRotacao * Time.deltaTime);

        tempo += Time.deltaTime;

        if (tempo >= tempoParaMudar)
        {
            MudarDirecao();
            tempo = 0f;
        }
    }

    public void AtivarModoTenso()
    {
        if (modoTenso) return;

        modoTenso = true;
        velocidadeRotacao = velocidadeRotacaoTensa;
    }

    void MudarDirecao()
    {
        direcao = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;
    }

    // =========================
    // PONTUAÇÃO
    // =========================

    public void AdicionarPonto()
    {
        pontos++;

        AtualizarUI();

        Debug.Log("Pontos: " + pontos);

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
        Debug.Log("ESFERA COMPLETA!");

        Invoke(nameof(VoltarMenu), 3f);
    }

    void VoltarMenu()
    {
        SceneManager.LoadScene(nomeCenaMenu);
    }
}