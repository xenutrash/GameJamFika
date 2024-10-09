using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro2DManager : MonoBehaviour {
    [SerializeField] private GameObject[] countdownObjects; // Array för varje GameObject (1, 2, 3, GO)
    //[SerializeField] private AudioSource audioS;
    //[SerializeField] private AudioClip[] numberSounds; // Ljudklipp för "1", "2", "3", "GO!"
    [SerializeField] private string triggerName = "Play"; // Namnet på samma trigger för alla objekten

    [SerializeField] private float countdownInterval = 1f;  // Tidsintervall mellan varje steg i nedräkningen
   // [SerializeField] private float soundCountdown = 2f;

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        // Trigga samma animation och spela ljud för varje objekt
        for (int i = 0; i < countdownObjects.Length; i++)
        {
            GameObject currentObject = countdownObjects[i];
            Animator animator = currentObject.GetComponent<Animator>();

            // Spela animationen med samma trigger
            if (animator != null)
            {
                animator.SetTrigger(triggerName);
            }

            // yield return new WaitForSeconds(soundCountdown);

            // // Spela upp motsvarande ljud för siffran eller "GO!"
            // if (i < numberSounds.Length)
            // {
            //     audioS.PlayOneShot(numberSounds[i]);
            // }

            // Vänta innan nästa animation triggas
            yield return new WaitForSeconds(countdownInterval);
        }

        // Här kan du lägga till logik för att starta spelet när nedräkningen är klar
    }
}
