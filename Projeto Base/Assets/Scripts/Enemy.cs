using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public GameObject bulletPrefab;
    public Transform pontoDisparo2;

    public float forcaDisparo = 20f;
    public float fireRate = 2f;

    private float timer;

    // Update is called once per frame
    void Update()
    {
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
       
        GameObject bullet = Instantiate(bulletPrefab, pontoDisparo2.position, pontoDisparo2.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = pontoDisparo2.forward * forcaDisparo;
    }
}
