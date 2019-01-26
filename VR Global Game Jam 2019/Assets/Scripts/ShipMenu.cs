using System.Collections;
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
    private GameObject ListContent;

    public EventSystem eventSystem;

    private float positionScale = 0.0025f;

    public float ButtonYStartPos = 125f;

    private PlanetData selectedPlanet;
    
    public PanelFade BlackoutCover;

    public float GlobeSpeed = 30f;
    
    private void Awake()
    {
        BlackoutCover.FadeOut();
    }

    // Start is called before the first frame update
    void Start()
    {
        DataManager = FindObjectOfType<GameDataManager>();

        var ButtonYPos = ButtonYStartPos;
        foreach (var planet in DataManager.Game.Planets)
        {
            AddListItem(
                planet.PlanetName, 
                ButtonYPos,
                DataManager.Game.Player.Location.Equals(planet), 
                delegate { LoadPlanetData(planet); });

            ButtonYPos -= 45;
        }

        LoadPlanetData(DataManager.Game.Player.Location);
    }
    
    // Update is called once per frame
    void Update()
    {
        PlanetPreview.transform.Rotate(Vector3.up, GlobeSpeed * Time.deltaTime);


    }

    public void LoadPlanetData(PlanetData data) {
        selectedPlanet = data;

        DetailsText.text = "Name: " + data.PlanetName
            + "\n Distance: " + data.PlanetDistance
            + "\n Composition: " + data.PlanetComposition
            + "\n Atmosphere: " + data.PlanetAtmosphere
            + "\n Temperature: " + data.PlanetTemperature
            + "\n -------------------------"
            + "\n Star: " + data.StarName
            + "\n Star Type: " + data.StarType.Name
            + "\n Star Temperature: " + data.StarTemperature
            + "\n Star Color: " + data.StarColor
            ;

        PlanetPreview.SetActive(true);

        var worldTexture = data.CreateWorldTexture();
        PlanetPreview.GetComponent<Renderer>().material.mainTexture = worldTexture;
    }

    public void AddListItem(string text, float buttonYPos, bool isHighlighted, UnityEngine.Events.UnityAction onClick)
    {
        Button choice = Instantiate(ButtonPrefab) as Button;
        Text choiceText = choice.GetComponentInChildren<Text>();

        choice.transform.SetParent(ListContent.transform, false);
        choice.onClick.AddListener(onClick);
        choiceText.text = text;

        if (isHighlighted)
        {
            eventSystem.firstSelectedGameObject = choice.gameObject;
        }
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
