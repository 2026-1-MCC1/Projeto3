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

    [Header("Tiro")]
    public GameObject bulletPrefab;
    public Transform muzzle;

    private Animator animator;
    private float verticalRotation = 0f;

    [Header("Pulo")]
    public float jumpForce = 7f;
    private bool estaNoChao;

    [SerializeField] private Transform Foot;
    [SerializeField] private LayerMask colisaoLayer;

    private CharacterController controller;
    private float forcaY;
    public float gravity = -15f;

    // 🔥 NOVO: plataforma atual
    private Platform plataformaAtual;

    float rotacaoY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // --- ROTACAO PLAYER ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        rotacaoY += mouseX;
        transform.rotation = Quaternion.Euler(0f, rotacaoY, 0f);

        // --- ROTACAO CAMERA ---
        float mouseY = Input.GetAxis("Mouse Y");
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalClamp, verticalClamp);
        cameraContainer.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // --- MOVIMENTO ---
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 forward = cameraContainer.forward;
        Vector3 right = cameraContainer.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moviment = forward * v + right * h;

        // --- CHÃO ---
        estaNoChao = Physics.CheckSphere(Foot.position, 0.3f, colisaoLayer);

        if (estaNoChao && forcaY < 0)
        {
            forcaY = -2f;
        }

        // --- DETECTAR PLATAFORMA ---
        RaycastHit hit;
        if (Physics.Raycast(Foot.position, Vector3.down, out hit, 0.6f))
        {
            plataformaAtual = hit.collider.GetComponent<Platform>();
        }
        else
        {
            plataformaAtual = null;
        }

        // --- PULO ---
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            forcaY = jumpForce;
            animator.SetTrigger("Saltar");
        }

        // --- GRAVIDADE ---
        forcaY += gravity * Time.deltaTime;

        // --- MOVIMENTO FINAL (COM PLATAFORMA) ---
        Vector3 movimentoFinal = moviment * moveSpeed;

        if (plataformaAtual != null)
        {
            movimentoFinal += plataformaAtual.DeltaMovimento / Time.deltaTime;
        }

        movimentoFinal.y = forcaY;

        controller.Move(movimentoFinal * Time.deltaTime);

        // --- ANIMAÇÃO ---
        animator.SetBool("EstaNoChao", estaNoChao);
        animator.SetFloat("Horizontal", h);
        animator.SetFloat("Vertical", v);
    }
}