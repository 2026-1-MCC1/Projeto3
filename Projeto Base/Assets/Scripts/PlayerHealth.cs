using UnityEngine;

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
    void Die()
    {
        Debug.Log("Jogador morreu");

        Destroy(gameObject);
    }
}