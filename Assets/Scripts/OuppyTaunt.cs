using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OuppyTaunt : MonoBehaviour
{
    [SerializeField]
    Player player;
    Gamepad controller;
    [SerializeField]
    GameObject ouppyObject;
    [SerializeField]
    float RotastionSpeed;

    bool Taunting;

    float TargetZRotastion;
    bool rotastionSet;

    public float rotationRate = 0.4f;
    float CurrentRotastion = 0;


    // Start is called before the first frame update
    void Start()
    {
        controller = player.controller; 


    }

    // Update is called once per frame
    void Update()
    {
        if (controller != null)
        {
            if (controller.yButton.isPressed && Taunting == false)
            {
                Taunting = true;
              
            }
            
        }else

        if (Input.GetKeyDown(KeyCode.T) && Taunting == false)
        {
            Taunting = true;
            
        }

        if(Taunting)
        {
            PlayTaunt(); 
        }

    }




    void PlayTaunt()
    {


        CurrentRotastion += rotationRate * Time.deltaTime;
        Debug.Log(CurrentRotastion); 
        if (CurrentRotastion >= 1)
        {
            CurrentRotastion = 0;
            Taunting = false;
            Debug.Log("Reset "); 
        }
        ouppyObject.transform.Rotate(CurrentRotastion, 0, 0);






    }
}
