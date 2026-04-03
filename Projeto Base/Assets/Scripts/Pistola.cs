using System.Collections;
using TMPro;
using UnityEngine;

public class PistolaSemiAuto : MonoBehaviour
{
    [Header("Configurações do Tiro")]
    public GameObject esferaPrefab;
    public Transform pontoDisparo;
    public float forcaDisparo = 200f;
    public float tempoEntreDisparos = 0.8f;
    public TextMeshProUGUI textoMunicao;
    public float tempoRecarga = 5f;

    private int munition = 30;
    private int munitionMax = 30;
    private float proximoDisparo = 0f;
    private bool recarregando = false;

    [Header("Referências")]
    public Transform cameraContainer;

    void Update()
    {
        if (munition == 0 && !recarregando && Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Recarregar());
        }

        textoMunicao.text = "Balas: " + munition;

        if (Input.GetButtonDown("Fire1") && Time.time >= proximoDisparo && munition > 0 && !recarregando)
        {
            munition--;
            Atirar();
            proximoDisparo = Time.time + tempoEntreDisparos;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            munition = 30;
            StartCoroutine(Recarregar());
        }
        if (recarregando)
        {
            textoMunicao.text = "Recarregando...";
        }
        if (recarregando)
            return;
    }

    private void Start()

    {
        {
            tempoEntreDisparos = 0.2f;
            proximoDisparo = Time.time;
        }
        {
            cameraContainer = Camera.main.transform;
        }
    }
    void Atirar()
    {
        Vector3 direcao = Camera.main.transform.forward;

        if (esferaPrefab == null || pontoDisparo == null)
        {
            Debug.LogError("Prefab ou pontoDisparo não configurado!");
            return;
        }

        GameObject esfera = Instantiate(
            esferaPrefab,
            pontoDisparo.position,
            Quaternion.LookRotation(direcao)
        );

        // Forca RB
        Rigidbody rb = esfera.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(direcao * forcaDisparo, ForceMode.Impulse);
        }

        // Ignora colisão com a arma
        Collider colEsfera = esfera.GetComponent<Collider>();
        Collider colArma = GetComponent<Collider>();

        if (colEsfera != null && colArma != null)
        {
            Physics.IgnoreCollision(colEsfera, colArma);
        }

        // Cor aleatória
        Renderer renderer = esfera.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = CorAleatoria();
        }

        // Destroy 5 segundos
        Destroy(esfera, 5f);
    }

    Color CorAleatoria()
    {
        int random = Random.Range(0, 4);

        switch (random)
        {
            case 0: return Color.blue;
            case 1: return Color.green;
            case 2: return Color.red;
            case 3: return Color.yellow;
            default: return Color.white;
        }
    }
    IEnumerator Recarregar()
    {
        recarregando = true;

            yield return new WaitForSeconds(tempoRecarga);

            munition = munitionMax;
            recarregando = false;
    }
}