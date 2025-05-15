using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //this script includes a option menu feature and I used a tutorial to help: https://www.youtube.com/watch?v=MNUYe0PWNNs
    public static GameManager instance;
    public string sceneName;
    public string mainMenu;
    [SerializeField] GameObject optionsMenu;
    private void Awake()

    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void changeScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void Pause()
    {
        optionsMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        optionsMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Settings()
    {

    }

    public void Quit()
    {
        SceneManager.LoadScene(mainMenu);
    }

}
