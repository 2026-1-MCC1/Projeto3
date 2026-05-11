using UnityEngine;

public class EsferaPrincipal : MonoBehaviour
{
    public float velocidadeRotacao = 100f;
    public float velocidadeRotacaoTensa = 150f;
    public float tempoParaMudar = 2f;

    private Vector3 direcao;
    private float tempo;
    private bool modoTenso = false;

    void Start()
    {
        MudarDirecao();
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
}