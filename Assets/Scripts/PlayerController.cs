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
    float currentSpeed = 0;
    bool isRunning = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
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

        //logica para aplicar del movimiento
        if (verticalInput != 0 || horizontalInput != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isRunning = true;
                currentSpeed = runSpeed;
            }
            else
            {
                isRunning = false;
                currentSpeed = walkSpeed;
            }
        }
        else
        {
            currentSpeed = 0;
        }

        //Moverse
        transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * walkSpeed * Time.deltaTime);

        //Rotar la camara con el Mouse
        float rotcamX = Input.GetAxis("Mouse X");
        transform.Rotate(0, rotcamX * speedRotation * Time.deltaTime, 0);

        // Aplicar movimiento
        //Vector3 movement = transform.forward * currentSpeed * Time.deltaTime;
        //rb.MovePosition(transform.position + movement);


        // Rotación si el personaje se mueve
        /*if (movement.magnitude > 0)
        {
            transform.Rotate(0, horizontalInput * speedRotation * Time.deltaTime, 0);
        }*/

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
