using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static Func<GameManager> GetInstance;  
    private void Awake()
    {
        GetInstance = () => this;
        enabled = false;
    }

    [SerializeField]
    private string[] Bg_Tracks;
    [SerializeField]
    float TimeToWaitBeforeStartOfGame = 3;


    private SplitScreenManager splitManager {  get;  set; }

    private float acumulator = 0; 
    readonly System.Random rand = new System.Random();

    private void Start()
    {
        if(AudioManager.instance != null)
        {
            AudioManager.instance.Pause(); 
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        acumulator += Time.deltaTime;

        if (acumulator > TimeToWaitBeforeStartOfGame)
        {
            Debug.Log(acumulator);
            splitManager.EnablePlayerMovement();
            enabled = false;

            if (Bg_Tracks.Length < 1)
            {
                Debug.Log("No bg tracks");
                return;
            }
            
            string TrackToPlay = Bg_Tracks[rand.Next(0,Bg_Tracks.Length)];
            Debug.Log("Selected track " + TrackToPlay);
            PlayAudio(TrackToPlay);
            acumulator = 0; 
        }
    }



    public void StartGame(SplitScreenManager manager)
    {
        splitManager = manager;
        enabled = true;

    }

   private void PlayAudio(string audioToPlay)
    {
        if (AudioManager.instance == null)
        {
            Debug.Log("No AudioManager in Scene");
            return; 
        }
        AudioManager.instance.Play(audioToPlay);
    }

}
