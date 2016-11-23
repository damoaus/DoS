using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gobal : MonoBehaviour
{

    public GameObject[] Pathway;
    public static float Heath = 100;
    public static float Gold = 100;
    float seconds;
    float minutes;
    public Text health;
    public Text gold;
    public Text counter;
    public GameObject MiddleUI;
    public GameObject TopLeftUI;
    public string nextLevel;



    public GameObject pauseScreen;
    //Sounds
    public AudioClip Click;
    private AudioSource Source { get { return GetComponent<AudioSource>(); } }
    public ParticleSystem ExplosionParticles;
    bool loadlevel = true;
    public bool fromStart = true;

    public GameObject TowerDescription;

    public enum gameState
    {
        Start,
        Playing,
        NextLevel,
        GameOver,
        Pause
    }

    public static gameState myState = gameState.Start;

    void Start()
    {

        myState = gameState.Start;

    }

    void Update()
    {

        gState();

        TopUIupdate();


    }

    /// <summary>
    ///  Determines the gamestate
    /// </summary>
    void gState()
    {

        switch (myState)
        {

            case gameState.Start:
                // During the Start phase Time is pause and Health and gold are reset. Using Enter or right click will change gamestate to playing
                Time.timeScale = 1;
                // actives UI prompt
                TopLeftUI.SetActive(true);
                TopLeftUI.GetComponent<Text>().text = "Press Enter to Start First Wave";
                if (Input.GetKeyUp(KeyCode.Return) == true)
                {
                    TopLeftUI.SetActive(false);
                    Time.timeScale = 1;
                    myState = gameState.Playing;
                }

                if (Input.GetKeyUp(KeyCode.Escape) == true)
                {

                    myState = gameState.Pause;
                }
                break;
            case gameState.Playing:
                // Time is set to normal speed  If Health = 0 then gamestate changes to Gameover, If Esc is press gamestate changes to pause
                
                fromStart = false;
                setTime();

                if (Heath <= 0)
                {
                    Heath = 0;
                    MiddleUI.SetActive(true);
                    MiddleUI.GetComponent<Text>().text = "Game Over";
                    myState = gameState.GameOver;
                }
                int CurrentWave = gameObject.GetComponent<Spawner>().currentPoint;
                int wave = gameObject.GetComponent<Spawner>().waveNo.Length;

                if (CurrentWave == wave)
                {

                    if (gameObject.GetComponent<Spawner>().CompletedLevel)
                    {

                        myState = gameState.NextLevel;
                    }

                }
                if (Input.GetKeyUp(KeyCode.Escape) == true)
                {

                    myState = gameState.Pause;
                }
                // Temp WinState
                if (Input.GetKeyUp(KeyCode.Tab) == true)
                {
                    myState = gameState.NextLevel;
                }


                break;
            case gameState.GameOver:
                // Time is paused and pressing Esc will load Main Menu

                if (Input.GetKeyUp(KeyCode.Escape) == true)
                {
                    Time.timeScale = 1;
                    gameObject.GetComponent<Loadingscreen>().Nextscene = true;
                    gameObject.GetComponent<Loadingscreen>().loadlevel = "Main_menu";


                }


                break;
            case gameState.NextLevel:
                // Loads the Next Level. nextLevel is set by GameManager
                if (loadlevel)
                {
                    gameObject.GetComponent<Loadingscreen>().Nextscene = true;
                    gameObject.GetComponent<Loadingscreen>().loadlevel = nextLevel;
                    loadlevel = false;
                }


                break;
            case gameState.Pause:
                // Time is paused. Press Esc to set gamestate to playing
                pauseScreen.SetActive(true);

                Time.timeScale = 0;
                if (Input.GetKeyUp(KeyCode.Escape) == true)
                {
                    Time.timeScale = 1;
                    pauseScreen.SetActive(false);
                    if (fromStart)
                    {
                        myState = gameState.Start;
                    }
                    else
                    {
                        myState = gameState.Playing;
                    }
                }
                break;
        }
    }

    /// <summary>
    /// Updates the Health and Gold UI to current Value
    /// </summary>
    void TopUIupdate()
    {
        health.text = Heath.ToString();
        gold.text = Gold.ToString();

    }
    /// <summary>
    /// Updates Time UI
    /// </summary>
    void setTime()
    {
        minutes = (int)(Time.timeSinceLevelLoad / 60f);
        seconds = (int)(Time.timeSinceLevelLoad % 60f);
        counter.text = minutes.ToString("00") + " : " + seconds.ToString("00");
    }

    public void PlaySound(AudioClip sound)
    {
        Source.PlayOneShot(Click);
    }
}



