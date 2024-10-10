using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterButton : MonoBehaviour {
    private Animator animator;

    // Hämta Animator-komponenten när skriptet startar
    void Start() {
        // Försöker hitta Animator-komponenten på samma objekt som skriptet ligger på
        animator = GetComponent<Animator>();

        // Kontrollera om Animator hittades
        if (animator == null) {
            Debug.LogError("Ingen Animator hittades på detta GameObject.");
        }
    }

    // Denna funktion kopplas till OnClick-eventet på Button-komponenten
    public void OnButtonClick() {
        if (animator != null) {
            // Sätt triggern för att starta zoom-animationen
            animator.SetTrigger("Zoom");

            // Om du vill återställa Animatorn efter en viss tid
            StartCoroutine(ResetTriggerAfterAnimation());
        }
    }

    IEnumerator ResetTriggerAfterAnimation() {
        // Vänta tills animationen är färdig (anpassa tiden efter din animations längd)
        yield return new WaitForSeconds(0.1f); // Exempel: 1 sekunds väntetid

        // Återgå till Idle-läget
        animator.SetTrigger("Reset");
    }
}
