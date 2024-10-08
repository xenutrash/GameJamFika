using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    // [SerializeField] AudioClip[] soundClips;
    // [SerializeField] AudioSource audioSource;

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        Application.Quit();
        Debug.Log("Quit"); //Added this message bcs this wont work in unity
    }

    // public void PlayRandomSound() {
    //     if (soundClips.Length > 0) {
    //         // Slumpa fram ett index i soundClips-arrayen
    //         int randomIndex = Random.Range(0, soundClips.Length);

    //         // Hämta ett slumpat ljudklipp
    //         AudioClip clip = soundClips[randomIndex];

    //         // Spela ljudet
    //         audioSource.PlayOneShot(clip);
    //     }
    //     else {
    //         Debug.LogError("Inga ljudklipp är tillagda i soundClips-arrayen.");
    //     }
    // }
}