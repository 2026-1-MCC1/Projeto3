using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using static CorObjeto;

public class LancamentoBola : MonoBehaviour
{
    [Header("Menu")]
    public string nomeCenaMenu = "Menu";

    public GameObject efeitoEncaixe;
    public AudioClip clipEncaixe;

    private Rigidbody rb;
    private bool encaixado = false;
    private string corDaBola;
    private CorObjeto corObjeto;
    public static int Pontuacao = 0;
    private static int totalPontos = 47; 
    private TextMeshProUGUI textoPontos;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        corObjeto = GetComponent<CorObjeto>();
    }

    void Start()
    {
        textoPontos = GameObject.Find("Pontuacao").GetComponent<TextMeshProUGUI>();
        if (rb == null)
        {
            Debug.LogError("[LancamentoBola] Rigidbody não encontrado em " + gameObject.name);
            return;
        }

        if (corObjeto != null)
        {
            corDaBola = CorParaTag(corObjeto.cor);
        }
        else
        {
            Debug.LogError("[LancamentoBola] CorObjeto não encontrado em " + gameObject.name);
        }
    }

    public void DefinirCor(CorBola novaCor)
    {
        corDaBola = CorParaTag(novaCor);
    }

    public void AgendarDestroy(float tempo)
    {
        Invoke("AutoDestroy", tempo);
    }

    void AutoDestroy()
    {
        if (!encaixado)
            Destroy(gameObject);
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

    void OnTriggerEnter(Collider other)
    {
        // impede chamadas duplicadas
        if (encaixado)
            return;

        if (string.IsNullOrEmpty(corDaBola))
            return;

        if (!other.CompareTag(corDaBola))
            return;

        AlvoCor alvo = other.GetComponent<AlvoCor>();

        if (alvo == null)
            alvo = other.GetComponentInParent<AlvoCor>();

        // tenta ocupar o buraco
        if (alvo != null)
        {
            if (!alvo.TentarOcupar(gameObject))
            {
                Destroy(gameObject);
                return;
            }
        }

        encaixado = true;

        rb.detectCollisions = false;

        // DESATIVA O TRIGGER DO BURACO
        other.enabled = false;

        // desativa colisão da bola
        Collider col = GetComponent<Collider>();

        if (col != null)
            col.enabled = false;

        // pontuação
        Pontuacao++;

        if (textoPontos != null)
        {
            textoPontos.text = Pontuacao + " / " + totalPontos;
        }

        if (Pontuacao >= totalPontos)
        {
            Vitoria();
        }

        CancelInvoke("AutoDestroy");

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;

        Transform alvoTransform = other.transform;

        if (alvo != null)
            alvoTransform = alvo.transform;

        transform.position = alvoTransform.position;
        transform.SetParent(alvoTransform);

        // som
        if (clipEncaixe != null)
        {
            GameObject somObj = new GameObject("SomEncaixeTemp");

            AudioSource src = somObj.AddComponent<AudioSource>();

            src.clip = clipEncaixe;
            src.Play();

            Destroy(somObj, 3f);
        }

        // efeito
        if (efeitoEncaixe != null)
        {
            GameObject efeito = Instantiate(
                efeitoEncaixe,
                transform.position,
                Quaternion.identity
            );

            Destroy(efeito, 3f);
        }

        // música
        GerenciadorMusica gm = FindObjectOfType<GerenciadorMusica>();

        if (gm != null)
            gm.CestaPreenchida();
    }

    string CorParaTag(CorBola cor)
    {
        switch (cor)
        {
            case CorBola.Azul: return "Azul";
            case CorBola.Vermelha: return "Vermelha";
            case CorBola.Verde: return "Verde";
            case CorBola.Amarela: return "Amarela";
            case CorBola.Roxa: return "Roxa";
            case CorBola.Laranja: return "Laranja";
            case CorBola.Rosa: return "Rosa";
            default: return "Untagged";
        }
    }
}