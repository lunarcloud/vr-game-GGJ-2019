using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
