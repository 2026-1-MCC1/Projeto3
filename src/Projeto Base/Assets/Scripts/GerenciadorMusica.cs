using UnityEngine;

public class GerenciadorMusica : MonoBehaviour
{
    [Header("Músicas")]
    public AudioSource musicaNormal;
    public AudioSource musicaTensa;

    [Header("Efeitos Tensão")]
    public AudioClip risadaMal;
    public AudioClip notControlMe;
    public GameObject raioNaCena;
    public GameObject efeitoTensao;
    public Vector3 posicaoEfeitoTensao = new Vector3(0, 2f, 0);

    [Header("Falas do Vilão")]
    public AudioClip nightmare;
    public AudioClip kid;
    public AudioClip[] falasAleatorias;

    [Header("Configuração")]
    public int totalCestas = 47;
    public int cestasPrecisaParaTensa = 20;

    [Header("Sequência de Áudio Tensão")]
    public float tempoParaNotControlMe = 4f;
    public float tempoAposNotControlMe = 7f;

    [Header("Referência")]
    public EsferaPrincipal esferaPrincipal;
    public Light luzDirecional;

    [Header("Configuração das Luzes")]
    public float intensidadeNormal = 2f;
    public float intensidadeTensa = 0.1f;

    private int cestasPreenchidas = 0;
    private bool tocandoTensa = false;
    private AudioSource audioSource;
    private MeshRenderer[] meshsRaio;
    private Collider[] colidersRaio;
    private int ultimaFalaIndex = -1;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (musicaNormal != null)
            musicaNormal.Play();

        if (luzDirecional != null)
            luzDirecional.intensity = intensidadeNormal;

        if (raioNaCena != null)
        {
            meshsRaio = raioNaCena.GetComponentsInChildren<MeshRenderer>(true);
            colidersRaio = raioNaCena.GetComponentsInChildren<Collider>(true);

            foreach (MeshRenderer m in meshsRaio)
                m.enabled = false;

            foreach (Collider c in colidersRaio)
                c.enabled = false;
        }

        // Nightmare toca 3 segundos após o início
        Invoke("FalarNightmare", 3f);
    }

    void Update()
    {
        if (cestasPreenchidas >= cestasPrecisaParaTensa && !tocandoTensa)
        {
            TrocarParaTensa();
        }
    }

    public void CestaPreenchida()
    {
        cestasPreenchidas++;
    }

    void FalarNightmare()
    {
        if (tocandoTensa) return;
        if (nightmare != null && audioSource != null)
        {
            audioSource.PlayOneShot(nightmare);
            // Após nightmare terminar, começa as falas aleatórias
            Invoke("FalarKid", nightmare.length + 5f);
        }
    }

    void FalarKid()
    {
        if (tocandoTensa) return;
        if (kid != null && audioSource != null)
        {
            audioSource.PlayOneShot(kid);
            Invoke("FalarAleatorio", kid.length + 5f);
        }
    }

    void FalarAleatorio()
    {
        if (tocandoTensa) return;
        if (falasAleatorias == null || falasAleatorias.Length == 0) return;

        int novoIndex;
        do
        {
            novoIndex = Random.Range(0, falasAleatorias.Length);
        } while (novoIndex == ultimaFalaIndex && falasAleatorias.Length > 1);

        ultimaFalaIndex = novoIndex;
        AudioClip fala = falasAleatorias[novoIndex];

        if (fala != null && audioSource != null)
        {
            audioSource.PlayOneShot(fala);
            Invoke("FalarAleatorio", fala.length + 10f);
        }
    }

    void TrocarParaTensa()
    {
        tocandoTensa = true;

        // Para todas as falas normais
        CancelInvoke("FalarNightmare");
        CancelInvoke("FalarKid");
        CancelInvoke("FalarAleatorio");

        // Para música normal
        if (musicaNormal != null)
        {
            musicaNormal.Stop();
            musicaNormal.volume = 0f;
            musicaNormal.enabled = false;
            musicaNormal.gameObject.SetActive(false);
        }

        // Toca música tensa
        if (musicaTensa != null)
        {
            musicaTensa.volume = 1f;
            musicaTensa.Play();
        }

        if (luzDirecional != null)
            luzDirecional.intensity = intensidadeTensa;

        if (meshsRaio != null)
            foreach (MeshRenderer m in meshsRaio)
                m.enabled = true;

        if (efeitoTensao != null)
        {
            GameObject efeito = Instantiate(efeitoTensao, posicaoEfeitoTensao, Quaternion.identity);
            Destroy(efeito, 5f);
        }

        if (esferaPrincipal != null)
            esferaPrincipal.AtivarModoTenso();

        // Sequência de áudio da tensão
        Invoke("TocarNotControlMe", tempoParaNotControlMe);
    }

    void TocarNotControlMe()
    {
        if (notControlMe != null && audioSource != null)
        {
            audioSource.PlayOneShot(notControlMe);
            Invoke("TocarRisada", notControlMe.length + tempoAposNotControlMe);
        }
    }

    void TocarRisada()
    {
        if (risadaMal != null && audioSource != null)
            audioSource.PlayOneShot(risadaMal);
    }
}