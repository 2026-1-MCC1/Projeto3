using UnityEngine;

public class Sphere : MonoBehaviour
{
    public float forwardForce = 2f;
    public float upwardForce = 1.5f;
    public KeyCode launchKey = KeyCode.Space;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Segurança: garante que existe Rigidbody
        if (rb == null)
        {
            Debug.LogError("Rigidbody não encontrado!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(launchKey))
        {
            Launch();
        }
    }

    void Launch()
    {
        if (rb == null) return;

        // Zera movimento anterior
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Define velocidade controlada (sem bug de força acumulando)
        Vector3 direcao = transform.forward * forwardForce + Vector3.up * upwardForce;

        rb.linearVelocity = direcao;
    }
}