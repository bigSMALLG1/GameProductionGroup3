using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holeBehaviour : MonoBehaviour
{
    public GameObject Fishtank; 
    public Transform tankSpawnPoint; 
    //collider without the trigger is the tankCollider, the one with the trigger is the trigger collider
    public Collider2D tankCollider;   
    public Collider2D triggerCollider; 
    public bool IsFilled {get; private set;}

    private void Start()
    {
        //adds holes to the list of holes on start
        holeManager.instance.detectHole(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(IsFilled)
        {
            return;
        }
        //if the collided object has the Box tag then hasTriggered is set to true and then if all variables are null it will spawn a fishtank, asset was originally a box before it was a fishtank and
        //I didn't want to go and change all the tags in case the art changed again
        if (collision.CompareTag("Box"))
        {
            IsFilled = true;

            //spawns fishtank at spawnpoint, there is a fishtank outside of the scene that is used to spawn in the new ones
            GameObject newFishtank = Instantiate(Fishtank, tankSpawnPoint.position, Quaternion.identity);

            //here I wanted the fishtank to spawn on a certain layer so the player could walk over it in renderview lol
            SpriteRenderer sr = newFishtank.GetComponent<SpriteRenderer>();
            if(sr != null)
            {
                sr.sortingOrder = -1;
            }
            //I set the rigidbody of the fishtank to static but I probably could have just destroyed the rigidbody instead or disabled it
            Rigidbody2D rb = newFishtank.GetComponent<Rigidbody2D>();
            
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Static;
            }

            Collider2D fishCollider = newFishtank.GetComponent<Collider2D>();
            //disables collider on the fishtank
            if (fishCollider != null)
            {
                fishCollider.enabled = false;
            }
            //destroys the original fishtank
            Destroy(collision.gameObject);
            //disables both colliders
            tankCollider.enabled = false;
            triggerCollider.enabled = false;

           holeManager.instance.HoleFilled();
        }
    }
}