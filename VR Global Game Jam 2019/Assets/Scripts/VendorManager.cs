using System.Collections;
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
    
    private void Awake()
    {
        BlackoutCover.FadeOut();
    }
    // Start is called before the first frame update
    void Start()
    {
        DataManager = FindObjectOfType<GameDataManager>();
        Sun.color = DataManager.Game.Player.Location.StarColor;

        var worldTexture = DataManager.Game.Player.Location.CreateWorldTexture();
        Globe.GetComponent<Renderer>().material.mainTexture = worldTexture;
    }

    // Update is called once per frame
    void Update()
    {
        Globe.transform.Rotate(Vector3.up, GlobeSpeed * Time.deltaTime);
    }

    public void BackToPort()
    {
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
