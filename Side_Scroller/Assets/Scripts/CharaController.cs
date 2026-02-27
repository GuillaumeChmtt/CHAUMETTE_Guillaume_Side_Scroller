using UnityEngine;

public class CharaController : MonoBehaviour
{
    [Header("Move variables")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float acceleration = 20f;
    [SerializeField] float deceleration = 2.0f;

    [Header("Rotation")]
    [SerializeField] float rotationForce = 10f;

    [Header("Gravity/Jump")]
    [SerializeField] float gravity = -10f;
    [SerializeField] float jumpForce = 1.5f;

    Rigidbody2D rb;
    float inputX;
    public LayerMask groundLayer;
    float inputRotation;

     void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

     void Update()
    {
  
        inputX = Input.GetAxisRaw("Horizontal");

        inputRotation = Input.GetAxisRaw("Vertical");

        bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded) rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        


        /*
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input.Normalize();
        */
    }

     void FixedUpdate()
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

        bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);
        if (!isGrounded && inputRotation != 0)
        {
            rb.AddTorque(inputRotation * rotationForce);
        }

        //quand on est dans les airs on ne peut plus accélérer (physique réelle pour une voiture)
        if (isGrounded == false) acceleration = 0f;

        //rb.linearVelocity = input * moveSpeed;
    }
}
