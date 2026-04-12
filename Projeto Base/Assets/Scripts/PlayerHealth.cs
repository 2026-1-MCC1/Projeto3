using UnityEngine;


// --- Modificadores de acesso para classes e variáveis ---
public class PlayerHealth : MonoBehaviour
{
    public int health = 6;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // --- Faz com que o jogador morra ---
    void Die()
    {
        Debug.Log("Jogador morreu");

        Destroy(gameObject);
    }
}