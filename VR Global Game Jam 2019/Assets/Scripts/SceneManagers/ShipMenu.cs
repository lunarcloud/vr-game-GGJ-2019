using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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

    public VendorPricesViewer pricesViewer;

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

        var sizeDelta = PlanetsListParent.GetComponent<RectTransform>().sizeDelta;
        sizeDelta.y = (DataManager.Game.Planets.Length * (40 + 5)) + 10;
        PlanetsListParent.GetComponent<RectTransform>().sizeDelta = sizeDelta;

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

        pricesViewer.Load(selectedPlanet);

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
