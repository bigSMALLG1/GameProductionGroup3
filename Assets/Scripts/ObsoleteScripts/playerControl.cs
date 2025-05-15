using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class playerControl : MonoBehaviour
{
    //This was the first player movement script that I made

    [Header("Movement Settings")]
    //settings for changing attributes like speed of player
    float horizontalMovement;
    float verticalMovement;
    //force of the players jump
    public float jumpForce;
    //movement speed of the player
    public float moveSpeed;
    //friction against ground
    public float groundFriction;
    [Header("Ground Check")]
    //Ground check settings and layermask for the ground
    public LayerMask whatIsGround;
    public Rigidbody2D rb;
    //readyToJump is true when raycast hit is in contact with ground and jumpcooldown is 0
    public bool readyToJump;
    //cooldown for jump
    public float jumpCoolDown;
    public bool keyPickedUp = false;
    public GameObject Key;
    bool lookingRight = true;
    private BoxCollider2D coll;
    private Vector2 movementDirection;

   private void Start()
   {
        //player should be able to jump on start
        readyToJump = true;
        coll = GetComponent<BoxCollider2D>();
   }
    private void Update()
    {
        //key input method called in update function
        KeyInput();
        //RaycastHit2D hit = isGrounded();
       // if (Input.GetButtonDown("Jump") && hit && readyToJump) 
      //  {
            //if you press spacebar and raycast hits ground and readyToJump is true, sets readyToJump to false and calls jump method, then invokes resetjump method along with cooldown.
       //     readyToJump = false;
      //      Jump();
       //     Invoke(nameof(ResetJump), jumpCoolDown);

       //     if (hit == true)
       //         rb.drag = groundFriction;
       //     else
        //        rb.drag = 0;
        
        //}
    }
    private void FixedUpdate()
    {
        //MovePlayer method called in FixedUpdate as it handles rigidbody physics
        MovePlayer();

        if(movementDirection.x > 0 && !lookingRight)
        {
            Flip();
        }
        else if(movementDirection.x < 0 && lookingRight)
        {
            Flip();
        }
    }
    private void KeyInput()
    {
        //allows movement with horizontal keys
       movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    private void MovePlayer()
    {
      rb.velocity = movementDirection * moveSpeed;
    }
        public RaycastHit2D isGrounded()
    {
        //spawns boxcast on middle of player shooting downwards, returns value if it hits ground
       //return Physics2D.Raycast(transform.position, -Vector2.up, 1.35f);
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down);
    }
    private void Jump()
    {  
        //jump method
       rb.velocity = new Vector2(rb.velocity.x, jumpForce);  
    }
    private void ResetJump()
    {
        //resets jump
        readyToJump = true;
    }      

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Key")
        {
            keyPickedUp = true;
            Destroy(Key);
        }
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        lookingRight = !lookingRight;
    }
}
