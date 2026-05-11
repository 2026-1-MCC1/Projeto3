using UnityEngine;
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
        ocupado = true;
    }
}