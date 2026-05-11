using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform alvo;
    public float velocidade = 15f;

    void Update()
    {
        transform.RotateAround(alvo.position, Vector3.up, velocidade * Time.deltaTime);
    }
}