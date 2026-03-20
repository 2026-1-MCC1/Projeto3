using UnityEngine;

public class PistolaSemiAuto : MonoBehaviour
{
    public GameObject esferaPrefab;
    public Transform pontoDisparo; // PONTA DA ARMA
    public float forcaDisparo = 500f;
    public float tempoEntreDisparos = 0.3f;

    private float proximoDisparo = 0.6f;

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
        // Instancia exatamente na ponta da arma
        GameObject esfera = Instantiate(
            esferaPrefab,
            pontoDisparo.position,
            pontoDisparo.rotation
        );

        Rigidbody rb = esfera.GetComponent<Rigidbody>();
        rb.linearVelocity = pontoDisparo.forward * forcaDisparo;

        Renderer renderer = esfera.GetComponent<Renderer>();
        renderer.material.color = CorAleatoria();
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