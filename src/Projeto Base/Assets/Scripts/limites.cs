using UnityEngine;

public class limites : MonoBehaviour
{
    // O ponto para onde o jogador será teleportado
    public Transform destino;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();

            // Desativa temporariamente
            controller.enabled = false;

            // Coloca o jogador um pouco acima do destino
            Vector3 novaPosicao = destino.position;
            novaPosicao.y += controller.height / 5f; // Ajuste conforme necessário

            other.transform.position = novaPosicao;
            other.transform.rotation = destino.rotation;

            // Reativa
            controller.enabled = true;

            PlayerHealth ph = other.transform.root.GetComponent<PlayerHealth>();
            ph.TakeDamage(1);
        }
    }
}
