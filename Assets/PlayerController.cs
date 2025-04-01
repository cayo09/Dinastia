using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float speedRotation;
    [SerializeField] private float jumpForce;
    private bool canJump = false;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        Jumping();
    }

    private void HandleMovement()
    {


        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = transform.forward * speed * verticalInput * Time.deltaTime;

        rb.MovePosition(transform.position + movement);

        if (movement.magnitude > 0)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Rotate(0, horizontalInput * speedRotation * Time.deltaTime, 0);
        }



    }


    private void Jumping()
    {


        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }



    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ground");
            canJump = true;
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
