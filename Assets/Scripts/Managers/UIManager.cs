using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject loseScreem;
    public GameObject winScreen;
    public GameObject healthBar;
    public GameObject inventory;

    public void Win()
    {
        winScreen.SetActive(true);
        loseScreem.SetActive(false);
        healthBar.SetActive(false);
        inventory.SetActive(false);
    }
    public void Lose()
    {
        loseScreem.SetActive(true);
        winScreen.SetActive(false);
        healthBar.SetActive(false);
        inventory.SetActive(false);
    }
}
