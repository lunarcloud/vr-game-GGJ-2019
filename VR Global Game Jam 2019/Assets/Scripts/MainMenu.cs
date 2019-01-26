﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameDataManager DataManager;

    public PanelFade BlackoutCover;

    public Button NewGameButton;

    public Button ContinueButton;

    public Button QuitButton;

    public Text PlayerName;

    public Text ShipName;

    public Canvas PlayerNameCanvas;

    public Canvas ShipNameCanvas;

    public Button PlayButton;

    public GvrKeyboard keyboard;

    private bool quitting = false;

    void Awake()
    {
        keyboard.gameObject.SetActive(false);

        NewGameButton.gameObject.SetActive(true);
        ContinueButton.gameObject.SetActive(true);
        QuitButton.gameObject.SetActive(true);
        PlayerNameCanvas.gameObject.SetActive(false);
        ShipNameCanvas.gameObject.SetActive(false);
        PlayButton.gameObject.SetActive(false);
        
        //if (DataManager.hasExistingData) {
        //    ContinueButton.interactable = true;
        //}

        BlackoutCover.FadeOut();
    }

    private void Update()
    {
        if (GvrControllerInput.AppButton)
        {
            Quit();
        }
    }
    
    public void NewGame()
    {
        NewGameButton.gameObject.SetActive(false);
        ContinueButton.gameObject.SetActive(false);
        QuitButton.gameObject.SetActive(false);

        DataManager.NewGame();

        PlayerNameCanvas.gameObject.SetActive(true);
        ShipNameCanvas.gameObject.SetActive(true);
        PlayButton.gameObject.SetActive(true);
    }

    public void ChangeInputDelegate(GvrKeyboardDelegateBase gvrKeyboard) {
        keyboard.gameObject.SetActive(false);
        keyboard.ClearText();
        keyboard.keyboardDelegate = gvrKeyboard;
        keyboard.gameObject.SetActive(true);
    }

    public void PlayGame() {

        // PlayerName.text;
        // ShipName.text;

        FadeToByIndex(1);
    }

    public void ContinueGame()
    {
        //FadeToByIndex(dataManager.scene);
    }

    public void FadeToByIndex(int sceneIndex)
    {
        StartCoroutine(FadeToByIndexImpl(sceneIndex));
    }

    private IEnumerator FadeToByIndexImpl(int sceneIndex)
    {
        BlackoutCover.FadeIn();
        yield return new WaitForSeconds(2);
        LoadByIndex(sceneIndex);
    }

    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void Quit()
    {

        if (!quitting)
        {
            quitting = true;
            StartCoroutine(QuitImpl());
        }
    }

    private IEnumerator QuitImpl()
    {
        BlackoutCover.FadeIn();
        yield return new WaitForSeconds(2);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        GvrDaydreamApi.LaunchVrHomeAsync((success) => {
            //Application.Quit(); // This results in dropping to android home before going back into daydream home.
        });
#endif
    }
}
