﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class ShipMenu : MonoBehaviour
{
    private GameDataManager DataManager;

    public GameObject PlanetPreview;

    public Text DetailsText;

    [SerializeField]
    private Button ButtonPrefab;

    [SerializeField]
    private GameObject PlanetsListParent;

    public EventSystem eventSystem;
  
    private PlanetData selectedPlanet;
    
    public PanelFade BlackoutCover;

    public Text shipPlack;

    public float GlobeSpeed = 30f;
    
    [SerializeField]
    private Text PriceItemPrefab;

    [SerializeField]
    private GameObject PricesListParent;

    public GameObject[] KidsDrawings;
    
    private void Awake()
    {
        BlackoutCover.FadeOut();
    }

    // Start is called before the first frame update
    void Start()
    {
        DataManager = FindObjectOfType<GameDataManager>();

        shipPlack.text = DataManager.Game.Player.ShipName;

        foreach (var planet in DataManager.Game.Planets)
        {
            AddPlanetItem(
                planet.PlanetName,
                DataManager.Game.Player.Location.Equals(planet),
                delegate { LoadPlanetData(planet); });
        }

        foreach (var drawing in KidsDrawings) {
            drawing.SetActive(false);
        }
        var random = new System.Random();
        KidsDrawings[random.Next(0, KidsDrawings.Length)].SetActive(true);

        LoadPlanetData(DataManager.Game.Player.Location);
    }
    
    // Update is called once per frame
    void Update()
    {
        PlanetPreview.transform.Rotate(Vector3.up, GlobeSpeed * Time.deltaTime);
        
    }

    public void LoadPlanetData(PlanetData data) {
        selectedPlanet = data;

        DetailsText.text = $"Name: {data.PlanetName}"
            + $"\nDistance: {data.PlanetDistance:F1} AU"
            + $"\nComposition: {data.PlanetComposition}"
            + $"\nAtmosphere: {data.PlanetAtmosphere}"
            + $"\nTerrain: {data.PlanetTerrain}"
            + $"\nTemperature: {data.PlanetTemperature:F1} °C"
            + "\n -------------------------"
            + $"\nStar: {data.StarName}"
            + $"\nStar Type: {data.StarType.Name}"
            + $"\nStar Temperature: {data.StarTemperature} °K"
            ;

        
        // Clear List, then build
        foreach (Transform child in PricesListParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (var resource in data.ResourceCosts)
        {
            AddPriceItem($" {resource.Key.Name} : $ {(resource.Value / 100)}.{(resource.Value % 100):00}");
        }

        PlanetPreview.SetActive(true);

        var worldTexture = data.CreateWorldTexture();
        PlanetPreview.GetComponent<Renderer>().material.mainTexture = worldTexture;
    }

    private void AddPlanetItem(string text, bool isHighlighted, UnityEngine.Events.UnityAction onClick)
    {
        Button choice = Instantiate(ButtonPrefab) as Button;
        Text choiceText = choice.GetComponentInChildren<Text>();

        choice.transform.SetParent(PlanetsListParent.transform, false);
        choice.onClick.AddListener(onClick);
        choiceText.text = text;

        if (isHighlighted)
        {
            eventSystem.firstSelectedGameObject = choice.gameObject;
        }
    }

    private void AddPriceItem(string text)
    {
        Text item = Instantiate(PriceItemPrefab) as Text;
        item.text = text;
        item.transform.SetParent(PricesListParent.transform, false);
    }

    public void Travel() {

        if (selectedPlanet == null) return;

        DataManager.Game.Player.Location = selectedPlanet;
        FadeToByIndex(2);
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
}
