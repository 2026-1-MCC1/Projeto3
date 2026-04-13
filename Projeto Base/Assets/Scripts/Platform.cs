using UnityEngine;


// --- Modificadores de acesso para classes e variáveis ---
public class Platform : MonoBehaviour
{
    public Transform alvo;      // Objeto que será o centro da órbita
    public float velocidade = 15f; // Velocidade da rotação
    void Update()
    {
        transform.RotateAround(alvo.position, Vector3.up, velocidade * Time.deltaTime);
    }
<<<<<<< HEAD


    // --- Faz o objeto girar ao redor do alvo e se tornar filho da plataforma ao colidir ---
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Platform"))
            this.transform.parent = collision.transform;
    }


    // --- Ao sair da plataforma, remove o objeto como filho dela ---
=======
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Platform"))
            this.transform.parent = collision.transform;
    }

>>>>>>> parent of 02c109c (atualizaÃ§Ã£o do player e platarforma)
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Platform"))
            this.transform.parent = null;
        
    }
}