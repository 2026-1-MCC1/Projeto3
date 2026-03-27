using UnityEngine;

public class PistolaSemiAuto : MonoBehaviour
{
    [Header("Configurações do Tiro")]
    public GameObject esferaPrefab;
    public Transform pontoDisparo;
    public float forcaDisparo = 200f;
    public float tempoEntreDisparos = 0.8f;

    private float proximoDisparo = 0f;

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
    }
    void Atirar()
    {
        if (esferaPrefab == null || pontoDisparo == null)
        {
            Debug.LogError("Prefab ou pontoDisparo não configurado!");
            return;
        }

        // Instancia a esfera
        GameObject esfera = Instantiate(
            esferaPrefab,
            pontoDisparo.position,
            pontoDisparo.rotation
        );

        // Aplica força
        Rigidbody rb = esfera.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(pontoDisparo.forward * forcaDisparo, ForceMode.Impulse);
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

        // Destroi depois de 5 segundos (evita acumular objetos)
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