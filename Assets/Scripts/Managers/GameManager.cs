using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int winCount = 0;

    private void Awake()
    {
        instance = this;
    }

    public bool lostGame;
    public bool wonGame;
    void Start()
    {
        lostGame = false;
        wonGame = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerHealth.instance.currentHealth <=0)
        {
            UIManager.instance.Lose();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (winCount == 2)
        {
            UIManager.instance.Win();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Retry()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }


}
