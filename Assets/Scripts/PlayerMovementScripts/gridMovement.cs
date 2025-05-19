using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridMovement : MonoBehaviour
{
    //Followed a tutorial on how to do grid movement but adapted it and changed a lot: https://youtu.be/mbzXIOKZurA?si=OhAjN5QOpBjz7Bo6
    private bool isMoving;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.4f;
    bool lookingRight = true;
    private BoxCollider2D coll;
    private Vector2 movementDirection;
    public Rigidbody2D rb;
    public bool keyPickedUp = false;
    public GameObject Key;
    public Transform MovePoint;
    public LayerMask whatStopsMovement;

 private void Start()
   {
        //getting the box collider component
        coll = GetComponent<BoxCollider2D>();
   }

    void Update()
    {
        //starts the moveplayer coroutine in whatever key corresponds to whatever vector
        if(Input.GetKey(KeyCode.W) && !isMoving)
            StartCoroutine(MovePlayer(Vector3.up));

        if(Input.GetKey(KeyCode.A) && !isMoving)
            StartCoroutine(MovePlayer(Vector3.left));

        if(Input.GetKey(KeyCode.S) && !isMoving)
            StartCoroutine(MovePlayer(Vector3.down));

        if(Input.GetKey(KeyCode.D) && !isMoving)
            StartCoroutine(MovePlayer(Vector3.right));
    }

    private void FixedUpdate()
    {
        //I almost lost my mind trying to get this to work
        //called this in FixedUpdate so that the players flip syncs with the WASD input
          if(Input.GetKey(KeyCode.D) && !lookingRight)
        {
            Flip();
        }
        else if(Input.GetKey(KeyCode.A) && lookingRight)
        {
            Flip();
        }
    }

    private IEnumerator MovePlayer(Vector3 direction)
{
    Vector3 rayOrigin = transform.position;
    float rayDistance = 1f;

    RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, rayDistance);
    
    //here I added the same gridmovement as the player but for the boxes that are moved in game so they move exactly one grid at a time by moving them using coroutines and lerp to ignore unity physics
    //I was stuck on this for a while but I found out that it only works if the rigidbody on the boxes are set to kinematic
    // If we hit a collider
    if (hit.collider != null)
    {
        if (hit.collider.CompareTag("Box"))
        {
            // Checks if box can move in the direction that we are moving, anything on the whatStopsMovement layer should stop movement but the raycast just hits any collider and stops the movement anyway
            RaycastHit2D boxHit = Physics2D.Raycast(hit.collider.transform.position, direction, rayDistance, whatStopsMovement);

            // If all is gucci then it does stuff
            if (boxHit.collider == null)
            {
                //Moves box
                Vector3 boxTarget = hit.collider.transform.position + direction * 1f;
                StartCoroutine(MoveBox(hit.collider.gameObject, boxTarget));
            }
            else
            {
                //the grid movement for the boxes was working but my player would just phase through every other game object but adding these yield breaks fixed it so that if 
                //the raycast hit the collider and it wasn't tagged as a box then the player wouldnt move
                yield break;
            }
        }
        
        else
        {
            yield break;
        }
    }
//player is moving, gets position of player and stores in origPos and then adds direction and how far I want it to travel (1f), stored in targetPos
//then while the elapsed time is less than the timeToMove, chnages transform.position (players position) by using lerp to bypass unity physics travelling from Vector3 a to Vector3 b in the timeToMove
    isMoving = true;

    float elapsedTime = 0f;
    origPos = transform.position;
    targetPos = origPos + direction * 1f;

    while (elapsedTime < timeToMove)
    {
        transform.position = Vector3.Lerp(origPos, targetPos, elapsedTime / timeToMove);
        elapsedTime += Time.deltaTime;
        yield return null;
    }

//makes sure the players position ends at the targetPos and then resets isMoving back to false
    transform.position = targetPos;
    isMoving = false;
}

//I added the MoveBox coroutine here, before this there was only the MovePlayer coroutine and I was just pushing the blocks manually using standard unity physics but adding this completed the basic mechanic for the game
private IEnumerator MoveBox(GameObject box, Vector3 targetPos)
{
    float elapsedTime = 0f;
    float tanktimeToMove = timeToMove;
    Vector3 boxOrigPos = box.transform.position;
  

    while (elapsedTime < tanktimeToMove)
    {
        box.transform.position = Vector3.Lerp(boxOrigPos, targetPos, elapsedTime / tanktimeToMove);
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    box.transform.position = targetPos;
}

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        lookingRight = !lookingRight;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Key")
        {
            keyPickedUp = true;
            Destroy(Key);
        }
    }
    

    /*public RaycastHit2D isGrounded()
    {
        //spawns boxcast on middle of player shooting downwards, returns value if it hits ground
       //return Physics2D.Raycast(transform.position, -Vector2.up, 1.35f);
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down);
    }
    */
}
