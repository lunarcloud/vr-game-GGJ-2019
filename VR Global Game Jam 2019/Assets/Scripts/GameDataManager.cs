using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDataManager : MonoBehaviour
{
    public GameData Game { get; set; }

    public void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0) {
            NewGame();
        }
    }

    public void NewGame()
    {
        // Create new game
        var random = new System.Random();
        Game = new GameData(random.Next());
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
