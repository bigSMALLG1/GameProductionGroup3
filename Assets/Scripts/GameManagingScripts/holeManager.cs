using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class holeManager : MonoBehaviour
{
    public static holeManager instance;
    //hole behaviour is attached to each hole game object
    public List<holeBehaviour> holes = new List<holeBehaviour>();
    public GameObject bookshelf;
    public string sceneToLoad;

    private void Awake()
    {
        //makes a singleton 
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void detectHole(holeBehaviour hole)
    {
        if(!holes.Contains(hole))
        {
            holes.Add(hole);
        }
    }
 public bool AllHolesFilled()
    {
        //checks each hole in the list, if one of them is not filled it returns, if all are filled it returns true
        foreach (var hole in holes)
        {
            if(!hole.IsFilled)
            {
                return false;
            }
        }
        return true;
    }
    public void HoleFilled()
    {
        if(AllHolesFilled())
        {
            //bookshelf collider turns on when all holes filled
            bookshelf.GetComponent<Collider2D>().enabled = true;
        }
    }

   
    public void ActivateBookshelf(Collider2D playerCollider)
    {
        if(AllHolesFilled() && playerCollider.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
