using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float speedRotation;
    [SerializeField] private float jumpForce;
    private bool canJump = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        Jumping();
    }

    private void HandleMovement()
    {
        float verticalInput = Input.GetAxis("Vertical"); // W/S
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && verticalInput > 0;

        // Definir velocidad según si está corriendo o caminando
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        currentSpeed *= verticalInput; // Si presiona "S", la velocidad será negativa

        // Aplicar movimiento
        Vector3 movement = transform.forward * currentSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        // Rotación si el personaje se mueve
        if (movement.magnitude > 0)
        {
            transform.Rotate(0, horizontalInput * speedRotation * Time.deltaTime, 0);
        }

        // Pasar valores al Animator
        animator.SetFloat("Speed", Mathf.Abs(currentSpeed));
        animator.SetBool("isRunning", isRunning);
    }


    private void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("isJumping", true);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ground");
            canJump = true;
            animator.SetBool("isJumping", false);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }

}
