using UnityEngine;

public class Orbitar : MonoBehaviour
{
    public Transform alvo;      // Objeto que será o centro da órbita
    public float velocidade = 15f; // Velocidade da rotação

    void Update()
    {
        transform.RotateAround(alvo.position, Vector3.up, velocidade * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}