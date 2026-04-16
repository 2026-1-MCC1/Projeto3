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

    public float jumpForce = 5f;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // "some" com o cursor do mouse 
    
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // --- Rotação horizontal do Player (eixo Y) ---
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0f, mouseX, 0f);

        // --- Rotação vertical da Camera (eixo X Local) ---
        float mouseY = Input.GetAxis("Mouse Y");
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(
            verticalRotation, -verticalClamp, verticalClamp);
        cameraContainer.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        
        // --- Movimentação WASD / Setas ---

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * h + transform.forward * v;
        transform.position += direction * moveSpeed * Time.deltaTime;
        animator.SetFloat("Horizontal", h);
        animator.SetFloat("Vertical", v);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // --- Tiro ---
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        }

    } 
    
}
