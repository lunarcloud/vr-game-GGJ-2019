﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortManager : MonoBehaviour
{
    private GameDataManager DataManager;
    
    public PanelFade BlackoutCover;

    public Light Sun;

    public Material sky;

    public Material PortGroundMaterial;

    public Material PortOceanMaterial;

    public GameObject[] vendorLights;

    public AudioSource[] music;

    public enum MusicTypes {
        Temperate,
        Hot,
        Cold
    }

    private void Awake()
    {
        DataManager = FindObjectOfType<GameDataManager>();
        vendorLights = GameObject.FindGameObjectsWithTag("VendorLights");
        foreach (var light in vendorLights)
        {
            light.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Sun.color = DataManager.Game.Player.Location.StarColor;

        PortGroundMaterial.color = DataManager.Game.Player.Location.PortGroundColor;
        PortOceanMaterial.color = DataManager.Game.Player.Location.PortOceanColor;

        // Increment the number of visits - 1 means we're on the first visit
        DataManager.Game.Player.Location.VisitCount++;

        var temp = DataManager.Game.Player.Location.PlanetTemperature;
        if (temp > 20) {
            music[(int) MusicTypes.Hot].Play();
        }
        else if (temp < 9)
        {
            music[(int)MusicTypes.Cold].Play();
        }
        else
        {
            music[(int)MusicTypes.Temperate].Play();
        }
        BlackoutCover.FadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableVendorLight(int index)
    {
        vendorLights[index].SetActive(true);
    }

    public void DisableVendorLight(int index)
    {
        vendorLights[index].SetActive(false);
    }

    public void BackToShip()
    {
        FadeToByIndex(1);
    }

    public void ToVendor(int type)
    {
        FadeToByIndex(3);
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
