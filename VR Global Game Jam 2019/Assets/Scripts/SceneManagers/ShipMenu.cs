using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

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
        DataManager = FindObjectOfType<GameDataManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        shipPlack.text = DataManager.Game.Player.ShipName;

        foreach (var planet in DataManager.Game.Planets.OrderBy(p => DataManager.Game.Player.DistanceTo(p)))
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
        BlackoutCover.FadeOut();
    }
    
    // Update is called once per frame
    void Update()
    {
        PlanetPreview.transform.Rotate(Vector3.up, GlobeSpeed * Time.deltaTime);
        
    }

    public void LoadPlanetData(PlanetData data) {
        selectedPlanet = data;

        DetailsText.text = $"Name: {data.PlanetName}"
            + $"\nTravel/Fuel Cost: {DataManager.Game.Player.DistanceTo(data)}"
            + $"\nDistance from Star: {data.PlanetDistance:F1} AU"
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

        var buyPrices = data.BuyPrices;
        var sellPrices = data.SellPrices;
        foreach (var resource in ResourceType.Types)
        {
            var buyPrice = buyPrices[resource];
            var sellPrice = sellPrices[resource];
            AddPriceItem($" {resource.Name} : $ {(buyPrice / 100)}.{(buyPrice % 100):00} / $ {(sellPrice / 100)}.{(sellPrice % 100):00}");
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
