using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float lifeTime = 5f;
    public int damege = 1;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    bool hit = false;  
    void OnTriggerEnter(Collider other)
    {
        if (hit) return;

        PlayerHealth ph = other.transform.root.GetComponent<PlayerHealth>();

        if(ph != null)
        {
           hit = true;
            GetComponent<Collider>().enabled = false;
            Debug.Log("Jogador atingido");
            ph.TakeDamage(damege);

            Destroy(gameObject);
        }
    }
}