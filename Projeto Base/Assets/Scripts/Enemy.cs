using UnityEngine;

public class Enemy : MonoBehaviour
{
    // --- Variáveis globais (enemy)---
    public Transform player;
    public GameObject bulletPrefab;
    public Transform pontoDisparo2;

<<<<<<< HEAD
    public float forcaDisparo = 20f;
=======
    public float forcaDisparo = 30f;
>>>>>>> main
    public float fireRate = 2f;

    private float timer;

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
    void Atirar()
    {
        // --- Cria o disparo e o lança para a frente ---
        GameObject bullet = Instantiate(bulletPrefab, pontoDisparo2.position, pontoDisparo2.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = pontoDisparo2.forward * forcaDisparo;
    }
}
