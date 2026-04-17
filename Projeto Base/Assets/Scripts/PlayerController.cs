using UnityEngine;


// --- Modificadores de acesso para classes e variáveis ---
public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("Movimento")]
    public float moveSpeed = 7f;

    [Header("Mouse")]
    public float mouseSensitivity = 7f;
    public float verticalClamp = 60f;

    [Header("Referências")]
    public Transform cameraContainer;
<<<<<<< HEAD

    [Header("Tiro")]
    public GameObject bulletPrefab;
    public Transform muzzle;
=======
>>>>>>> main
    private Animator animator;
    private float verticalRotation = 0f;

    [Header("Pulo")]
    public float jumpForce = 7f;
    private Rigidbody rb;
    private bool estaNoChao;

    [SerializeField] private Transform Foot;
    [SerializeField] private LayerMask colisaoLayer;

    private CharacterController controller;
    private float forcaY;
    public float gravity = -20f;
<<<<<<< HEAD
<<<<<<< HEAD


    // --- Trava e retira o cursor, além de pegar referências do controller e animator ---
=======
    // Start is called once before the first execution of Update after the MonoBehaviour is created
>>>>>>> parent of 02c109c (atualizaÃ§Ã£o do player e platarforma)
=======


    // --- Trava e retira o cursor, além de pegar referências do controller e animator ---
>>>>>>> main
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponentInChildren<Animator>();
        }
    }
    float rotacaoY;
<<<<<<< HEAD
<<<<<<< HEAD
    
=======
    // Update is called once per frame
>>>>>>> parent of 02c109c (atualizaÃ§Ã£o do player e platarforma)
=======
    
>>>>>>> main
    void Update()
    {
        // --- Rotação horizontal do Player (eixo Y) ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        rotacaoY += mouseX;

        transform.rotation = Quaternion.Euler(0f, rotacaoY, 0f);

        // --- Rotação vertical da Camera (eixo X Local) ---
        float mouseY = Input.GetAxis("Mouse Y");
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(
            verticalRotation, -verticalClamp, verticalClamp);
        cameraContainer.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);


        // --- Movimentação WASD / Setas ---
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 forward = cameraContainer.forward;
        Vector3 right = cameraContainer.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moviment = forward * v + right * h;

        controller.Move(moviment * moveSpeed * Time.deltaTime);

        bool estaNoChao = controller.isGrounded;

        if (estaNoChao && forcaY < 0)
        {
            forcaY = -2f; // mantém colado no chão
        }

        animator.SetBool("EstaNoChao", estaNoChao);
        animator.SetFloat("Horizontal", h);
        animator.SetFloat("Vertical", v);

        // --- Pulo do Player ---
        estaNoChao = Physics.CheckSphere(Foot.position, 0.3f, colisaoLayer);
        animator.SetBool("EstaNoChao", estaNoChao);

        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            forcaY = 5f;
            animator.SetTrigger("Saltar");
        }

        if (forcaY > -9.81f)
        {
            forcaY += -9.81f * Time.deltaTime;
        }
        controller.Move(new Vector3(0f, forcaY, 0f) * Time.deltaTime);
    }
}
