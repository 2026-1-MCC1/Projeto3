using UnityEngine;

public class CorObjeto : MonoBehaviour
{
    public CorBola cor;
    public Material[] materiais;

    void Start()
    {
        InicializarCor();
    }

    public void InicializarCor()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && materiais != null && (int)cor < materiais.Length)
        {
            renderer.material = materiais[(int)cor];
        }
    }

    public void AtualizarCorVisual()
    {
        InicializarCor();
    }
    public enum CorBola
    {
        Azul,
        Vermelha,
        Verde,
        Amarela,
        Roxa,
        Laranja,
        Rosa
    }
}