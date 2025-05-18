using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pressKeyStart : MonoBehaviour
{
    public string menuScreen = "MainMenu";

    void Start()
    {

    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            StartCoroutine(LoadMenu());
        }
    }

    private IEnumerator LoadMenu()
    {
        SceneManager.LoadScene(menuScreen);
        yield return null;
    }
}
