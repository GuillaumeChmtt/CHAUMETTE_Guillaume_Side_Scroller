using Unity.VisualScripting;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    [Header("Move variables")]
    [SerializeField] float moveSpeed = 25f;
    [SerializeField] float acceleration = 20f;
    [SerializeField] float deceleration = 2.0f;

    [Header("Rotation")]
    [SerializeField] float airRotationForce = 5f;

    [Header("Jump")]
    [SerializeField] float jumpForce = 1.5f;

    Rigidbody2D rb;
    float inputX;
    public LayerMask groundLayer;
    bool isGroundedFront = false;
    bool isGroundedBack = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

     void Update()
    {
  
        inputX = Input.GetAxisRaw("Horizontal");

        isGroundedFront = Physics2D.Raycast(new Vector3(0.5f, 0f, 0f), Vector2.down, 1f, groundLayer);

        isGroundedBack = Physics2D.Raycast(new Vector3(-0.5f,0f,0f), Vector2.down, 1f, groundLayer);



        if (Input.GetButtonDown("Jump") & isGroundedBack) rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        
    }

     void LateUpdate()
    {
        var v = rb.linearVelocity;
        //v.x = inputX * moveSpeed;

        // Ne pas accélérer dans les airs
        if (!isGroundedBack & !isGroundedFront) inputX = 0f;

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


        // Rotation vehicule
        if (!isGroundedBack & !isGroundedFront)
        {
            if (Input.GetKey(KeyCode.A))
                rb.MoveRotation(rb.rotation + airRotationForce);

            if (Input.GetKey(KeyCode.D))
                rb.MoveRotation(rb.rotation - airRotationForce);
        }



        if (isGroundedBack & isGroundedFront) Debug.Log("La voiture touche le sol");    
    }


        
        //rb.linearVelocity = input * moveSpeed;
    
}
