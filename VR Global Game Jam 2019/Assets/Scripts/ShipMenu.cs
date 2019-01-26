using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ShipMenu : MonoBehaviour
{
    private GameDataManager DataManager;

    public Image PlanetImage;

    public Text DetailsText;

    [SerializeField]
    private Button ButtonPrefab;

    [SerializeField]
    private GameObject ListContent;

    public EventSystem eventSystem;

    private bool FirstSelectedChosen = false;

    private float positionScale = 0.0025f;

    public float ButtonYStartPos = 125f;

    // Start is called before the first frame update
    void Start()
    {
        DataManager = FindObjectOfType<GameDataManager>();

        var ButtonYPos = ButtonYStartPos;
        foreach (var planet in DataManager.Game.Planets)
        {
            AddListItem(planet.PlanetName, ButtonYPos, delegate {
                LoadPlanetData(planet);
            });
            ButtonYPos -= 45;
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadPlanetData(PlanetData data) {
        DetailsText.text = "Name: " + data.PlanetName 
            + "\n Gravity: " + data.PlanetGravity;
    }

    public void AddListItem(string text, float buttonYPos, UnityEngine.Events.UnityAction onClick)
    {
        Button choice = Instantiate(ButtonPrefab) as Button;
        Text choiceText = choice.GetComponentInChildren<Text>();

        choice.transform.SetParent(ListContent.transform, false);
        choice.onClick.AddListener(onClick);
        choiceText.text = text;

        if (!FirstSelectedChosen)
        {
            eventSystem.firstSelectedGameObject = choice.gameObject;
        }
    }
}
