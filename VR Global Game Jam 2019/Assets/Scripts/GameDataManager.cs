using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDataManager : MonoBehaviour
{
    public bool active { get { return _active; } }

    private bool _active = false;

    public GameData Game { get; set; }

    void Awake()
    {
        var gameDataManagers = GameObject.FindObjectsOfType<GameDataManager>();
        if (gameDataManagers.Length > 1) {
            Destroy(this.gameObject);
        }

        _active = true;

        DontDestroyOnLoad(this.gameObject);

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
    
}
