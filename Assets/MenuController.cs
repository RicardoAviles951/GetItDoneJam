using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Music and Sounds")]
    public AK.Wwise.Event menuMusic;
    public AK.Wwise.Event confirmSound;

    public VisualElement EntireScreen;
    public Button playButton;
    public Button creditsButton;
    public Button quitButton;
    public ScreenFader screenFader;

    private bool buttonClicked = false;


    private void Start()
    {
        menuMusic.Post(gameObject);
    }

    private void OnEnable()
    {
        var root      = GetComponent<UIDocument>().rootVisualElement;
        playButton    = root.Q<Button>("playButton");
        creditsButton = root.Q<Button>("creditsButton");
        quitButton    = root.Q<Button>("quitButton");

        playButton.clicked += PlayButtonClicked;
        creditsButton.clicked += CreditsButtonClicked;
        quitButton.clicked += QuitButtonClicked;
    }

    private void OnDisable()
    {
        playButton.clicked -= PlayButtonClicked;
        creditsButton.clicked -= CreditsButtonClicked;
        quitButton.clicked -= QuitButtonClicked;
    }

    void EndGame()
    {
        Application.Quit();
    }

    void PlayButtonClicked()
    {
        if(!buttonClicked)
        {
            confirmSound.Post(gameObject);
            screenFader.FadeOut(screenFader.fadeDuration);
            buttonClicked = true;
            // Load your desired scene, replace "GameScene" with the actual scene name
            StartCoroutine(LoadScene("Scene_Ricardo"));
        }
        
    }

    void CreditsButtonClicked()
    {
        if(!buttonClicked)
        {
            confirmSound.Post(gameObject);
            screenFader.FadeOut(screenFader.fadeDuration);
            buttonClicked = true;
            // Load your desired scene, replace "CreditsScene" with the actual scene name
            StartCoroutine(LoadScene("Scene_Ricardo"));
        }
        
    }

    void QuitButtonClicked()
    {
        confirmSound.Post(gameObject);
        EndGame();
    }

    IEnumerator LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(screenFader.fadeDuration + .05f);
        SceneManager.LoadScene(sceneName);
    }

}
