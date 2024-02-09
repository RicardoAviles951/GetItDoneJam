using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverDetector : MonoBehaviour
{
    private VisualElement EntireScreen;
    private Label GameOverText;
    private Button button;

    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        EntireScreen = root.Q<VisualElement>("EntireScreen");
        GameOverText = root.Q<Label>("Label_gameover");
        button = root.Q<Button>("Button");

    }

   

    private void OnEnable()
    {
        GameManager.GameOver += ToggleGameOver;
        button.clicked += () => GameManager.instance.Retry();

    }

    private void OnDisable()
    {
        GameManager.GameOver -= ToggleGameOver;
        button.clicked -= () => GameManager.instance.Retry();
    }


    void ToggleGameOver(bool won)
    {
        //Enable UI
        EntireScreen.style.display = DisplayStyle.Flex;
        if (won)
        {
            GameOverText.text = "You Won!";
        }
        else
        {
            GameOverText.text = "You Lose!";
        }
    }

   
}
