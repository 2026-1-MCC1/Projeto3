using System.Collections;
using TMPro;
using UnityEngine;

public class PistolaSemiAuto : MonoBehaviour
{
    public GameObject esferaPrefab;
    public Transform pontoDisparo; // PONTA DA ARMA
    public float forcaDisparo = 500f;
    public float tempoEntreDisparos = 0.3f;
    public TextMeshProUGUI textoMunicao;
    public float tempoRecarga = 5f;

    private int munition = 30;
    private int munitionMax = 30;
    private float proximoDisparo = 0.6f;
    private bool recarregando = false;


    private void Start()
    {
        
    }

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
    IEnumerator Recarregar()
    {
        recarregando = true;

            yield return new WaitForSeconds(tempoRecarga);

            munition = munitionMax;
            recarregando = false;
    }
}