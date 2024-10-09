using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public static Func<PauseMenu> GetInstance = () => null;

    public Button focusButton; 

    private void Awake()
    {
        GetInstance = () => this;
        SetVisabability(false); 

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnBecameVisible()
    {

    }
    private void OnEnable()
    {
        if (focusButton != null)
        {
            Debug.Log("Button selected");
            focusButton.Select();
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        SetVisabability(false); 

        if(AudioManager.instance != null)
        {
            AudioManager.instance.Resume();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SetVisabability(bool visability)
    {
        gameObject.SetActive(visability);
    }
   
}
