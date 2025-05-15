using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wakeCat : MonoBehaviour
{
    public GameObject catPopup;
    public GameObject reginaldDialogueObj;
    private bool playerInRange = false;


    void Start()
    {
        //TMP popup turned off on start and the game obj holding the dialogue script
        catPopup.SetActive(false);
        reginaldDialogueObj.SetActive(false);
    }

   
    void Update()
    {
        //when player goes in to trigger and presses E it gets rid of the popup and dialogue begins
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            catPopup.SetActive(false);
            reginaldDialogueObj.SetActive(true);
            GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //when player collides with trigger the popup is turned on 
        if(collision.CompareTag("Player"))
        {
            //Debug.Log("Player in range");
            catPopup.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //leaving the trigger just turns the popup off again
        if(collision.CompareTag("Player"))
        {
            catPopup.SetActive(false);
            playerInRange = false;
        }
    }
}
