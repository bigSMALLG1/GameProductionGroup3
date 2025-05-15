using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bookshelfBehaviour : MonoBehaviour
{
    //couldn't deactivate the script without the start method
    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //bookshelf active when all fishtanks have filled the holes
        holeManager.instance.ActivateBookshelf(collision);
    }
}
