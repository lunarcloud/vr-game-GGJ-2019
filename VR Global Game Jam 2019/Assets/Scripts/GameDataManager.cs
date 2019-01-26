using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public GameData Game { get; set; }

    public void NewGame()
    {
        // Create new game
        Game = new GameData(1234);
        Debug.Log("New Game Data!");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
