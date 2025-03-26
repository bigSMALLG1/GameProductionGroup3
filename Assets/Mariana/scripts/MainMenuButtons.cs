using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuButtons : MonoBehaviour
{
    public void PlayGame()
    {
        //makes the game go on to the next scene on the scene manager won't work 
        Debug.Log("Game Started!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quitgame()
    {
        //makes the game quit 
        Debug.Log("Quit!");
        Application.Quit();
    }
}
