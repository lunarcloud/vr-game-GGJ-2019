using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortManager : MonoBehaviour
{
    private GameDataManager DataManager;
    
    public PanelFade BlackoutCover;

    public Light Sun;

    public Material sky;

    private void Awake()
    {
        BlackoutCover.FadeOut();

    }

    // Start is called before the first frame update
    void Start()
    {

        DataManager = FindObjectOfType<GameDataManager>();
        Sun.color = DataManager.Game.Player.Location.StarColor;
    }

    // Update is called once per frame
    void Update()
    {
        
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
