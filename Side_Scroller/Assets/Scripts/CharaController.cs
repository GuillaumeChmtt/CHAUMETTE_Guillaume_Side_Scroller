using UnityEngine;

public class CharaController : MonoBehaviour
{
    [Header("Move variables")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float acceleration = 20f;
    [SerializeField] float deceleration = 2.0f;

    [Header("Rotation")]
    [SerializeField] float airRotationForce = 5f;

    [Header("Jump")]
    [SerializeField] float jumpForce = 1.5f;

    Rigidbody2D rb;
    float inputX;
    public LayerMask groundLayer;
    bool isGrounded = false;

     void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

     void Update()
    {
  
        inputX = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded) rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        
    }

     void LateUpdate()
    {
        var v = rb.linearVelocity;
        //v.x = inputX * moveSpeed;

        if (inputX != 0)
        {
            // Accélération 
            float maxspeed = inputX * moveSpeed;
            v.x = Mathf.MoveTowards(v.x, maxspeed, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            // Décélération 
            v.x = Mathf.MoveTowards(v.x, 0f, deceleration * Time.fixedDeltaTime);
        }

        rb.linearVelocity = v;


        // ROTATIONS
        if (!isGrounded)
        {
            if (Input.GetKey(KeyCode.A))
                rb.MoveRotation(rb.rotation + airRotationForce);

            if (Input.GetKey(KeyCode.D))
                rb.MoveRotation(rb.rotation - airRotationForce);
        }
        
    }

        //rb.linearVelocity = input * moveSpeed;
    
}
