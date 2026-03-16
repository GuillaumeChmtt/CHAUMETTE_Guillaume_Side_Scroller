using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

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

    public Vector2 frontOffset = new Vector2(0.38f, -0.5f);
    public Vector2 backOffset = new Vector2(-0.38f, -0.5f);
    public float groundCheckDistanceFront = 0.5f;
    public float groundCheckDistanceBack = 0.5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        // Raycast suivent la rotation
        Vector3 frontOrigin = transform.TransformPoint(frontOffset);
        Vector3 backOrigin = transform.TransformPoint(backOffset);
        Vector3 downDirection = -transform.up; 

        Color color = Color.bisque;
        Debug.DrawRay(backOrigin, downDirection * groundCheckDistanceBack, color);
        Debug.DrawRay(frontOrigin, downDirection * groundCheckDistanceFront, color);

        RaycastHit2D hitFront = Physics2D.Raycast(frontOrigin, downDirection, groundCheckDistanceFront, groundLayer);
        RaycastHit2D hitBack = Physics2D.Raycast(backOrigin, downDirection, groundCheckDistanceBack, groundLayer);

        isGroundedFront = hitFront.collider != null;
        isGroundedBack = hitBack.collider != null;

        if (Input.GetButtonDown("Jump") && isGroundedBack) rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void LateUpdate()
    {
        var v = rb.linearVelocity;
        //v.x = inputX * moveSpeed;

        // Wheeling
        while (isGroundedBack || isGroundedFront)
        {
            inputX = Input.GetAxisRaw("Horizontal");
             break;
        }

        // Ne pas accélérer dans les airs
        if (!isGroundedBack && !isGroundedFront) inputX = 0f;

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
        if (!isGroundedFront)
        {
            if (Input.GetKey(KeyCode.A))
                rb.MoveRotation(rb.rotation + airRotationForce);

            if (Input.GetKey(KeyCode.D))
                rb.MoveRotation(rb.rotation - airRotationForce);
        }



        if (isGroundedBack || isGroundedFront) Debug.Log("La voiture touche le sol");
        
        if(!isGroundedBack && !isGroundedFront) Debug.Log("La voiture ne touche pas le sol");
    }



    //rb.linearVelocity = input * moveSpeed;

    
}
