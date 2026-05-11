using UnityEngine;
using UnityEngine.SceneManagement;
using static CorObjeto;

public class AlvoCor : MonoBehaviour
{
    public CorBola cor;

    private bool ocupado = false;
   
    public bool EstaOcupado()
    {
        return ocupado;
    }

    public void Ocupar()
    {
        if (ocupado) return;

        ocupado = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ocupado) return;

        if (other.CompareTag("Bola"))
        {
            Ocupar();
        }
    }
}