using UnityEngine;

public class LancamentoBall : MonoBehaviour
{
    private Rigidbody rb;
    private bool encaixado = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cesta") && !encaixado)
        {
            encaixado = true;

            // Para tudo
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Desliga física
            rb.useGravity = false;
            rb.isKinematic = true;

            // Coloca a bola no centro da cesta
            transform.position = other.bounds.center;
        }
    }
}