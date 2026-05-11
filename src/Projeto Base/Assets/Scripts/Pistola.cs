using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pistola : MonoBehaviour
{
    [System.Serializable]
    public class ConfigCor
    {
        public CorBola tipoCor;
        public Material material;
    }

    [Header("Configurações do Tiro")]
    public GameObject esferaPrefab;
    public float velocidadeDisparo = 14f;
    public float tempoEntreDisparos = 0.3f;
    public float forcaCima = 3f;

    [Header("UI")]
    public TextMeshProUGUI textoMunicao;
    public Image showColor;

    [Header("Configuração das Cores")]
    public ConfigCor[] coresDisponiveis;

    [Header("Audio")]
    public AudioClip somDisparo;

    [Header("Referência")]
    public Transform pontoDisparo;

    private float proximoDisparo = 0f;
    private ConfigCor corAtual;
    private Camera cam;
    private AudioSource audioSource;

    void Start()
    {
        cam = Camera.main;
        audioSource = GetComponent<AudioSource>();
        corAtual = PegarCorAleatoria();
        AtualizarCorUI();
        if (textoMunicao != null) textoMunicao.text = "";
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= proximoDisparo)
        {
            Atirar();
            proximoDisparo = Time.time + tempoEntreDisparos;
        }
    }

    void Atirar()
    {
        if (esferaPrefab == null || coresDisponiveis == null || coresDisponiveis.Length == 0) return;

        if (somDisparo != null && audioSource != null)
            audioSource.PlayOneShot(somDisparo);

        ConfigCor corEscolhida = corAtual;

        Vector3 origem = pontoDisparo != null ? pontoDisparo.position : cam.transform.position;
        Vector3 direcao = cam.transform.forward;

        GameObject esfera = Instantiate(esferaPrefab, origem, Quaternion.LookRotation(direcao));

        Collider esferaCollider = esfera.GetComponent<Collider>();
        if (esferaCollider != null)
        {
            Collider[] playerColliders = transform.root.GetComponentsInChildren<Collider>();
            foreach (Collider c in playerColliders)
                Physics.IgnoreCollision(esferaCollider, c);
        }

        Renderer renderer = esfera.GetComponent<Renderer>();
        if (renderer != null && corEscolhida.material != null)
            renderer.material = corEscolhida.material;

        Rigidbody rb = esfera.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
            rb.linearVelocity = direcao * velocidadeDisparo + Vector3.up * forcaCima;
        }

        CorObjeto corObjeto = esfera.GetComponent<CorObjeto>();
        if (corObjeto != null)
        {
            corObjeto.cor = corEscolhida.tipoCor;
            corObjeto.InicializarCor();
        }

        LancamentoBola lb = esfera.GetComponent<LancamentoBola>();
        if (lb != null)
            lb.AgendarDestroy(5f);
        else
            Destroy(esfera, 5f);

        corAtual = PegarCorAleatoria();
        AtualizarCorUI();
    }

    ConfigCor PegarCorAleatoria()
    {
        if (coresDisponiveis == null || coresDisponiveis.Length == 0) return null;
        return coresDisponiveis[Random.Range(0, coresDisponiveis.Length)];
    }

    void AtualizarCorUI()
    {
        if (showColor != null && corAtual != null && corAtual.material != null)
            showColor.color = corAtual.material.color;
    }
}