using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDScript : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI PosText;

    [SerializeField]
    TextMeshProUGUI SpeedText;

    [SerializeField]
    TextMeshProUGUI LapText;

    [SerializeField]
    TextMeshProUGUI WinnerText;

    [SerializeField]
    TextMeshProUGUI WinnerPlayerText;

    [SerializeField]
    Image WinnerImage; 


    [SerializeField]
    Canvas canvas;

    [SerializeField]
    GameObject PosObject;


    [SerializeField]
    GameObject SpeedObject;

    [SerializeField]
    GameObject LapObject;

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

    public void WinnerPlayer(string TextToSet)
    {
        /*
        WinnerPlayerText.gameObject.SetActive(true);
        WinnerPlayerText.text = TextToSet;
        */

        WinnerImage.gameObject.SetActive(true);
        
        WinnerText.gameObject.SetActive(true);
        WinnerText.text = "Finished at pos: 1";

        LapObject.SetActive(false);
        SpeedObject.SetActive(false);
        PosObject.SetActive(false);
    }


    public void SetFinishedText(string pos)
    {
       
        WinnerText.gameObject.SetActive(true);
        WinnerText.text = "Finished at pos: " + pos;

        LapObject.SetActive(false);
        SpeedObject.SetActive(false);
        PosObject.SetActive(false);


    }



}
