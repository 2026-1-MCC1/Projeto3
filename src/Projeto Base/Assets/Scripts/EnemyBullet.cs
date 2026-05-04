using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // --- Variáveis globais (EnemyBullet) ---
    public float lifeTime = 5f;
    public int damage = 1;
    void Start()
    {
        // --- Destroi o objeto após um tempo de vida ---
        Destroy(gameObject, lifeTime);
    }
    bool hit = false;

    // --- Controla colisão: detecta o player, dá dano e destrói o disparo ---
    void OnTriggerEnter(Collider other)
    {
        if (hit) return;

        PlayerHealth ph = other.transform.root.GetComponent<PlayerHealth>();

        if(ph != null)
        {
           hit = true;
            GetComponent<Collider>().enabled = false;
            Debug.Log("Jogador atingido");
            ph.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}