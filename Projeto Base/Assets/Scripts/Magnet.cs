using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float velocidade = 500f; 

    void Update()
    {
        transform.Rotate(Vector3.up * velocidade * Time.deltaTime);
    }
}
