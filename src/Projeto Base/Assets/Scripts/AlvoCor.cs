using UnityEngine;
using static CorObjeto;

public class AlvoCor : MonoBehaviour
{
    public CorBola cor;

    private GameObject bolaAtual;

    public bool TentarOcupar(GameObject bola)
    {
        // já existe bola nesse buraco
        if (bolaAtual != null)
            return false;

        bolaAtual = bola;

        return true;
    }
}