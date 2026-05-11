using UnityEngine;

public class EsferaPrincipal: MonoBehaviour
{
    public float velocidadeRotacao = 100f; // velocidade do giro
    public float tempoParaMudar = 2f;      // tempo para trocar direção

    private Vector3 direcao;
    private float tempo;

    void Start()
    {
        MudarDirecao();
    }

    void Update()
    {
        // gira na direção atual
        transform.Rotate(direcao * velocidadeRotacao * Time.deltaTime);

        // conta o tempo
        tempo += Time.deltaTime;

        // muda direção depois de um tempo
        if (tempo >= tempoParaMudar)
        {
            MudarDirecao();
            tempo = 0f;
        }
    }

    void MudarDirecao()
    {
        direcao = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;
    }
}