using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tomePopup : MonoBehaviour
{
    public AudioSource pageFlip;
   public string sceneToLoad;
   public GameObject popupUI;
   private bool playerInRange = false;
    //Used same popup system from the wakeCat script
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(holeManager.instance.AllHolesFilled() && collision.CompareTag("Player"))
        {
            popupUI.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            popupUI.SetActive(false);
            playerInRange = false;
        }
    }

    private void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(sceneChange());
        }
    }
    private IEnumerator sceneChange()
    {
        //originally I tried playing the sound effect in update before understanding coroutines properly
        //then I got stuck with a "not all code paths return a value" because I didn't include yield return to pause the coroutine
        pageFlip.Play();
        yield return new WaitForSeconds(pageFlip.clip.length);
        SceneManager.LoadScene(sceneToLoad);
    }
}
