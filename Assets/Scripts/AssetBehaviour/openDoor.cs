using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour
{
    public GameObject closedDoor;
    public GameObject doorOpen;
   public GameObject doorPopup;
   public GameObject needKeyPopup;
   public GameObject player;
   private bool playerInRange = false;

    private void Start()
    {
        doorPopup.SetActive(false);
        needKeyPopup.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && player.GetComponent<gridMovement>().keyPickedUp)
        {
            needKeyPopup.SetActive(false);
            playerInRange = true;
            doorPopup.SetActive(true);
        }

        else if(!player.GetComponent<gridMovement>().keyPickedUp && collision.CompareTag("Player"))
        {
            playerInRange = true;
            needKeyPopup.SetActive(true);
        }
        //Debug.Log("Triggered by: " + collision.gameObject.name); the walls were causing the trigger to fire
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
         if(collision.CompareTag("Player") && player.GetComponent<gridMovement>().keyPickedUp)
         {
            needKeyPopup.SetActive(false);
            playerInRange = false;
            doorPopup.SetActive(false);
         }

         else
         {
            playerInRange = false;
            needKeyPopup.SetActive(false);
         }
    }

    private void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E) && player.GetComponent<gridMovement>().keyPickedUp)
        {
            closedDoor.SetActive(false);
            doorOpen.SetActive(true);
        }
    }
}
