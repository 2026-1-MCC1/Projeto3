using UnityEngine;

public class Enemy : MonoBehaviour
{
    // --- Variáveis globais (enemy)---
    public Transform player;
    public GameObject bulletPrefab;
    public Transform pontoDisparo2;

    public float forcaDisparo = 30f;
    public float fireRate = 2f;

    private float timer;
    public Color[] cores;

    void Update()
    {
        // --- Segue o player com rotação suave e atira a cada certo tempo ---
        if (player == null) return;

        Quaternion targetRotation = Quaternion.LookRotation(player.position - pontoDisparo2.position);
        pontoDisparo2.rotation = Quaternion.Lerp(
            pontoDisparo2.rotation,
            targetRotation,
            Time.deltaTime * 5f);

        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            Atirar();
            timer = 0f;
        }
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
    void Atirar()
    {
        // --- Cria o disparo e o lança para a frente ---
        GameObject bullet = Instantiate(bulletPrefab, pontoDisparo2.position, pontoDisparo2.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = pontoDisparo2.forward * forcaDisparo;

        //--- escolhe cor aleatória---
        Color cor = CorAleatoria();
        //--- aplica na bala ---
        Renderer rend = bullet.GetComponent<Renderer>();

        if (rend != null)
        {
            rend.material.color = cor;
        }
    }
}