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

    public Image PlanetImage;

    public GameObject PlanetPreview;

    public Text DetailsText;

    [SerializeField]
    private Button ButtonPrefab;

    [SerializeField]
    private GameObject ListContent;

    public EventSystem eventSystem;

    private bool FirstSelectedChosen = false;

    private float positionScale = 0.0025f;

    public float ButtonYStartPos = 125f;

    private PlanetData selectedPlanet;
    
    public PanelFade BlackoutCover;

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
        selectedPlanet = data;

        DetailsText.text = "Name: " + data.PlanetName
            + "\n Star: " + data.StarName
            + "\n Distance: " + data.PlanetDistance
            + "\n Composition: " + data.PlanetComposition
            + "\n Atmosphere: " + data.PlanetAtmosphere
            + "\n Temperature: " + data.PlanetTemperature
            ;

        PlanetPreview.SetActive(true);
        /*
        Texture runtimeTexture = (Texture)Resources.Load("earth");
        Material runtimeMaterial = new Material(Shader.Find("VertexLit"));
        runtimeMaterial.SetTexture("_MainTex", runtimeTexture);

        PlanetPreview.GetComponent<Renderer>().material = runtimeMaterial;
        */
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
