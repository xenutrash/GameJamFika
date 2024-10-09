using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class ScenLoader : MonoBehaviour
{
    public string[] SceneNames; 


    // Start is called before the first frame update
    void Awake()
    {
        foreach( string NameOfScene in SceneNames)
        {
            try
            {
                SceneManager.LoadScene(NameOfScene, LoadSceneMode.Additive);
            }
            catch
            {
                Debug.Log("Could not load the scene"); 
            } 
        }
        
    }


    private void Start()
    {
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
