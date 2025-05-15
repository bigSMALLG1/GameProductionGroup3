using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class pushBlock : MonoBehaviour
{
    public float distance = 1f;
    public LayerMask blockMask;
    GameObject block;
    void Start()
    {
        
    }

    
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, blockMask);

        if(hit.collider != null && hit.collider.gameObject.tag == "pushableBlock" && Input.GetKey(KeyCode.Space))
        {
            block = hit.collider.gameObject;

            block.GetComponent<FixedJoint2D> ().enabled = true;
            block.GetComponent<FixedJoint2D> ().connectedBody = this.GetComponent<Rigidbody2D> ();
        } 
        
        else if (Input.GetKeyUp (KeyCode.Space)) 
        {
            block.GetComponent<FixedJoint2D> ().enabled = false;
        }

         /*if(hit.collider != null)
         {
            Debug.Log("hit");
         }
        */

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * distance);
    }
}
