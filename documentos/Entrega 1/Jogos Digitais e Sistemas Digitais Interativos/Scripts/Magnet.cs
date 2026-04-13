using UnityEngine;

// --- Modificadores de acesso para classes e variáveis ---
public class Magnet : MonoBehaviour
{
    public float velocidade = 500f; 

    void Update()
    {
        // --- Rotação no eixo (Y) de um objeto ---
        transform.Rotate(Vector3.up * velocidade * Time.deltaTime);
    }
}
