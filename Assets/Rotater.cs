using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    float CurrentY = 0; 
    public float rotationRate = 0.2f;
    float CurrentRotastion = 0; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentRotastion += rotationRate * Time.deltaTime; 
        if(CurrentRotastion > 360)
        {
            CurrentRotastion = 0;
        }
        transform.Rotate(0,CurrentRotastion, 0);
    }
}
