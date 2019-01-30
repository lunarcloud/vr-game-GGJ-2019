using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameDataManager DataManager;

    public PanelFade BlackoutCover;

    public Button NewGameButton;

    public Button ContinueButton;

    public Button QuitButton;

    public Text InputTitle;

    public Text InputText;

    public Text VRModeText;

    public Canvas InputCanvas;

    public Button NextButton;

    public Button PlayButton;

    public GvrKeyboard daydreamKeyboard;

    public bool randomIsPlayer = false;

    private bool quitting = false;

    private GvrModeManager GvrMode;

    void Awake()
    {
        GvrMode = FindObjectOfType<GvrModeManager>();
        Input.backButtonLeavesApp = true;

        daydreamKeyboard.gameObject.SetActive(false);
        NewGameButton.gameObject.SetActive(true);
        ContinueButton.gameObject.SetActive(true);
        QuitButton.gameObject.SetActive(true);
        InputCanvas.gameObject.SetActive(false);
        NextButton.gameObject.SetActive(false);
        PlayButton.gameObject.SetActive(false);

        //if (DataManager.hasExistingData) {
        //    ContinueButton.interactable = true;
        //}
    }

    private void Start()
    {
        VRModeText.text = "Active VR Mode: " + (GvrMode.IsDaydream ? "Daydream" : "Cardboard");
        BlackoutCover.FadeOut();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || GvrControllerInput.AppButton)
        {
            Quit();
        }
    }
    
    public void NewGame()
    {
        NewGameButton.gameObject.SetActive(false);
        ContinueButton.gameObject.SetActive(false);
        QuitButton.gameObject.SetActive(false);

        DataManager.NewGame();

        InputTitle.text = "Input Captain Name";
        InputText.text = DataManager.Game.Player.PlayerName;
        if (GvrMode.IsDaydream)
            daydreamKeyboard.gameObject.SetActive(true);
        StartCoroutine(SetEditorText(DataManager.Game.Player.PlayerName));

        randomIsPlayer = true;
        InputCanvas.gameObject.SetActive(true);
        NextButton.gameObject.SetActive(true);
    }
 
    public void ShowKeyboard()
    {
        if (GvrMode.IsDaydream)
            daydreamKeyboard.gameObject.SetActive(true);
    }

    public void PlayerNameGood()
    {
        if (GvrMode.IsDaydream)
            daydreamKeyboard.gameObject.SetActive(false);
        DataManager.Game.Player.PlayerName = InputText.text;
        
        InputTitle.text = "Input Ship Name";
        InputText.text = DataManager.Game.Player.ShipName;
        if (GvrMode.IsDaydream)
            daydreamKeyboard.gameObject.SetActive(true);
        StartCoroutine(SetEditorText(DataManager.Game.Player.ShipName));

        randomIsPlayer = false;
        NextButton.gameObject.SetActive(false);
        PlayButton.gameObject.SetActive(true);
    }
    

    public void RandomizeInput() {
        var newName = randomIsPlayer ? CaptainNameGenerator.Create() : ShipNameGenerator.Create();
        StartCoroutine(SetEditorText(newName));
    }

    private IEnumerator SetEditorText(string text)
    {
        yield return new WaitForSeconds(1.0f);
        InputText.text = text;
        if (GvrMode.IsDaydream)
            daydreamKeyboard.EditorText = text;
    }

    public void PlayGame()
    {
        if (GvrMode.IsDaydream)
            daydreamKeyboard.gameObject.SetActive(false);
        NextButton.gameObject.SetActive(false);
        PlayButton.gameObject.SetActive(false);

        DataManager.Game.Player.ShipName = InputText.text;
    
        FadeToByIndex(1);
    }

    public void ContinueGame()
    {
        //FadeToByIndex(dataManager.scene);
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

    public void Quit()
    {

        if (!quitting)
        {
            quitting = true;
            StartCoroutine(QuitImpl());
        }
    }

    private IEnumerator QuitImpl()
    {
        BlackoutCover.FadeIn();
        yield return new WaitForSeconds(2);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        if (GvrIntent.IsLaunchedFromVr()) {
            GvrDaydreamApi.LaunchVrHomeAsync((success) => {
                if (!success) {
                    Application.Quit();
                }
            });
        } else {
            Application.Quit();
        }
#endif
    }
}
