﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VendorManager : MonoBehaviour
{
    private GameDataManager DataManager;

    public PanelFade BlackoutCover;

    public Light Sun;

    public Material sky;

    public float GlobeSpeed = 5f;

    public GameObject Globe;

    public GameObject[] Vendors;

    private Animator currentAnimator;
    
    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        DataManager = FindObjectOfType<GameDataManager>();
        Sun.color = DataManager.Game.Player.Location.StarColor;

        var worldTexture = DataManager.Game.Player.Location.CreateWorldTexture();
        Globe.GetComponent<Renderer>().material.mainTexture = worldTexture;

        foreach (var vendor in Vendors)
        {
            var isActive = vendor.name == DataManager.Game.Player.Location.VendorModel.Name;
            vendor.SetActive(isActive);
            if (isActive)
            {
                currentAnimator = vendor.GetComponent<Animator>();
                currentAnimator.SetBool("FriendlyGreet", DataManager.Game.Player.Location.Friendliness > 0.4f);
                currentAnimator.SetBool("ShakingHand", false);
            }
        }

        BlackoutCover.FadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        Globe.transform.Rotate(Vector3.up, GlobeSpeed * Time.deltaTime);
    }

    public void ShakeHands()
    {
        StartCoroutine(ShakeHandsImpl());
    }
    
    private IEnumerator ShakeHandsImpl() {

        currentAnimator.SetBool("ShakingHand", true);
        yield return new WaitForSeconds(1);
        currentAnimator.SetBool("ShakingHand", false);
    }

    public void BackToPort()
    {
        DataManager.Game.Player.Location.Friendliness += 0.05f;
        FadeToByIndex(2);
    }

    private void FadeToByIndex(int sceneIndex)
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
}
