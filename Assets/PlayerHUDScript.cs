using TMPro;
using UnityEngine;

public class PlayerHUDScript : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI PosText;

    [SerializeField]
    TextMeshProUGUI SpeedText;

    [SerializeField]
    TextMeshProUGUI LapText;
    

    [SerializeField]
    Canvas canvas; 

    public void SetLapText(string TextToSet)
    {
        if(LapText == null)
        {
            Debug.Log("No LapText on object");
            return; 
        }

        LapText.text = TextToSet;
    }

    public void SetSpeedText(string TextToSet)
    {
        if (SpeedText == null)
        {
            Debug.Log("No SpeedText on object");
            return;
        }

        SpeedText.text = TextToSet;
    }

    public void SetPosText(string TextToSet)
    {
        if (PosText == null)
        {
            Debug.Log("No PosText on object");
            return;
        }

        PosText.text = TextToSet;
    }

    public void SetOwner(Camera camera)
    {
        if (canvas == null)
        {
            canvas = GetComponent<Canvas>();
            if (canvas == null)
            {
                Debug.Log("No canvas attatched to the canvas script");
                return;
            }
        }

        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = camera;
        canvas.planeDistance = 1; 

    }

}
