using UnityEngine;

public class PistolaSemiAuto : MonoBehaviour
{
    [Header("Configurações do Tiro")]
    public GameObject esferaPrefab;
    public Transform pontoDisparo;
    public float forcaDisparo = 200f;
    public float tempoEntreDisparos = 0.8f;

    private float proximoDisparo = 0f;

    [Header("Referências")]
    public Transform cameraContainer;

    void Update()
    {
        // Segurar botão para atirar continuamente
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Segurando botão");

            if (Time.time >= proximoDisparo)
            {
                Debug.Log("Atirando");

                Atirar();
                proximoDisparo = Time.time + tempoEntreDisparos;
            }
        }
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
}