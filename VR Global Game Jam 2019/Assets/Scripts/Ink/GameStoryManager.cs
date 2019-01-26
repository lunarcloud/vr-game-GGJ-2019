﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStoryManager : MonoBehaviour {

    private GameDataManager DataManager;
    public InkStoryManager inkManager;
    public GameObject dialogCanvas;
    public GameObject activationCanvas;
    public UnityEngine.EventSystems.EventSystem eventSystem;

    private bool friendly;
    
    private bool pointingAtDialogBox = false;

    private bool moreToChatAbout = true;

    void Awake ()
    {
        DataManager = FindObjectOfType<GameDataManager>();

        inkManager.storyEndAction = delegate {
            dialogCanvas.SetActive(false);
            moreToChatAbout = false;

        };
		inkManager.AddTagProcessor ("talklater", delegate(string value) {
            moreToChatAbout = true;
            activationCanvas.SetActive(true);
            dialogCanvas.SetActive(false);
        });
    }

    public void StartStory()
    {
        dialogCanvas.SetActive(true);
        activationCanvas.SetActive(false);
        inkManager.story.ObserveVariable("friendly", (string varName, object varFriendly) =>
        {
            friendly = (bool) varFriendly;
        });
        inkManager.StartStory();
        moreToChatAbout = true;
    }
    
    private void Update()
    {
        if (GvrControllerInput.AppButton) dialogCanvas.SetActive(false);
    }

}
