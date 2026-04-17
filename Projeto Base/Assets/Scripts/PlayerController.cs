using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("Movimento")]
    public float moveSpeed = 7f;

    [Header("Mouse")]
    public float mouseSensitivity = 7f;
    public float verticalClamp = 60f;

    [Header("Referências")]
    public Transform cameraContainer;
    private Animator animator;

    [Header("Pulo")]
    public float jumpForce = 17f;
    public float gravity = -12f;

    [SerializeField] private Transform Foot;

    private CharacterController controller;

    private float verticalRotation = 0f;
    private float rotacaoY;
    private float forcaY;
    private bool estaNoChao;

    // --- Plataforma ---
    private Transform plataformaAtual;
    private Vector3 lastPlatformPosition;
    private Quaternion lastPlatformRotation;

    // --- Tolerância (ANTI-QUEDA) ---
    private float tempoSemPlataforma = 0f;
    public float toleranciaPlataforma = 0.2f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // --- MOUSE ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        rotacaoY += mouseX;
        transform.rotation = Quaternion.Euler(0f, rotacaoY, 0f);

        float mouseY = Input.GetAxis("Mouse Y");
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalClamp, verticalClamp);
        cameraContainer.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // --- DETECÇÃO DA PLATAFORMA COM TOLERÂNCIA ---
        RaycastHit hit;

        if (Physics.Raycast(Foot.position, Vector3.down, out hit, 1.2f))
        {
            if (hit.collider.CompareTag("Platform"))
            {
                tempoSemPlataforma = 0f;

                if (plataformaAtual != hit.collider.transform)
                {
                    plataformaAtual = hit.collider.transform;
                    lastPlatformPosition = plataformaAtual.position;
                    lastPlatformRotation = plataformaAtual.rotation;
                }
            }
            else
            {
                tempoSemPlataforma += Time.deltaTime;
            }
        }
        else
        {
            tempoSemPlataforma += Time.deltaTime;
        }

        if (tempoSemPlataforma > toleranciaPlataforma)
        {
            plataformaAtual = null;
        }

        // --- MOVIMENTO WASD ---
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 forward = cameraContainer.forward;
        Vector3 right = cameraContainer.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moviment = forward * v + right * h;

        // --- MOVIMENTO DA PLATAFORMA ---
        Vector3 movimentoPlataforma = Vector3.zero;

        if (plataformaAtual != null)
        {
            Vector3 deltaPos = plataformaAtual.position - lastPlatformPosition;
            Quaternion deltaRot = plataformaAtual.rotation * Quaternion.Inverse(lastPlatformRotation);

            Vector3 relativePos = Foot.position - plataformaAtual.position;
            Vector3 rotatedPos = deltaRot * relativePos;

            Vector3 novaPos = plataformaAtual.position + rotatedPos;
            Vector3 movimentoRotacao = novaPos - Foot.position;

            Vector3 velocidadePlataforma = deltaPos / Time.deltaTime;

            movimentoPlataforma = movimentoRotacao + velocidadePlataforma * Time.deltaTime;

            lastPlatformPosition = plataformaAtual.position;
            lastPlatformRotation = plataformaAtual.rotation;
        }

        // --- 1. MOVE COM A PLATAFORMA ---
        if (plataformaAtual != null)
        {
            controller.Move(movimentoPlataforma);
        }

        // --- CHÃO ---
        estaNoChao = controller.isGrounded;

        if (controller.isGrounded)
        {
            if (forcaY < 0)
                forcaY = -2f;
        }

        if (plataformaAtual != null && controller.isGrounded)
        {
            controller.Move(Vector3.down * 2f * Time.deltaTime);
        }

        // --- PULO ---
        if (Input.GetKeyDown(KeyCode.Space) && (estaNoChao || plataformaAtual != null))
        {
            forcaY = jumpForce;
            animator.SetTrigger("Saltar");
        }

        // --- GRAVIDADE ---
        forcaY += gravity * Time.deltaTime;

        // --- MOVIMENTO DO PLAYER ---
        Vector3 movimentoFinal =
            moviment * moveSpeed +
            Vector3.up * forcaY;

        controller.Move(movimentoFinal * Time.deltaTime);

        // --- ANIMAÇÃO ---
        animator.SetBool("EstaNoChao", estaNoChao);
        animator.SetFloat("Horizontal", h);
        animator.SetFloat("Vertical", v);
    }
}