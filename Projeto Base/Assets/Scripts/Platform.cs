using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform alvo;
    public float velocidade = 15f;

    public Vector3 DeltaMovimento { get; private set; }

    private Vector3 ultimaPosicao;

    void Start()
    {
        ultimaPosicao = transform.position;
    }

    void Update()
    {
        transform.RotateAround(alvo.position, Vector3.up, velocidade * Time.deltaTime);

        DeltaMovimento = transform.position - ultimaPosicao;
        ultimaPosicao = transform.position;
    }
}