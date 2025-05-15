using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class keyBehaviour : MonoBehaviour
{
    public GameObject Key;
    public string sceneName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(Key);
            changeScene();
        }
    }

   
    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
