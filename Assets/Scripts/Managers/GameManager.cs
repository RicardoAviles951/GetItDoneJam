using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //Scriptable object that holds data on locked and unlocked abilties. 
    public SO_AbilityData abilityData;

    //Added by Ricardo
    //Event for when the game is over
    public static event Action<bool> GameOver;

    public int winCount = 0;

    private void Awake()
    {
        instance = this;
    }

    public bool isGameOver = false;

    public bool lostGame;
    public bool wonGame;
    void Start()
    {
        lostGame = false;
        wonGame = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        PlayerHealth.OnDeath += LoseGame;
    }

    private void OnDisable()
    {
        PlayerHealth.OnDeath -= LoseGame;
    }

    // Update is called once per frame
    void Update()
    {
        if(winCount == 2 && !isGameOver) 
        {
            WinGame();
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            Retry();
        }

        ProcessGameState();
    }

    //This method fires when the player has died
    void LoseGame()
    {
        //Set flag for losing
        lostGame = true;
        //Send message to anything listening for when the game is over (mostly for UI)
        GameOver?.Invoke(wonGame);
    }
    public void Retry()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        //Reset unlocked abilities by locking fire again
        abilityData.LockAbility("Fire");
        SceneManager.LoadScene(currentSceneName);
    }

    

    void WinGame()
    {
        wonGame = true;
        //Send out a message to UI
        GameOver?.Invoke(wonGame);
    }

    void ResetCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void ProcessGameState()
    {
        if (!isGameOver)
        {
            if(lostGame)
            {
                ResetCursor();
                GameOver?.Invoke(wonGame);
                isGameOver = true;
            }

            if (wonGame)
            {
                ResetCursor();
                GameOver?.Invoke(wonGame);
                isGameOver = true;
            }

        }
    }
}
