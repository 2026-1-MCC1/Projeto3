using UnityEngine;

public class LancamentoBall : MonoBehaviour
{
    public string corDaBola;

    private Rigidbody rb;
    private bool encaixado = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (encaixado) return;

        // verifica se o collider é da cor correta
        if (!other.CompareTag(corDaBola)) return;

        encaixado = true;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.useGravity = false;
        rb.isKinematic = true;

        transform.position = other.transform.position;

        transform.SetParent(other.transform);
    }
}