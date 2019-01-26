using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameDataManager DataManager;

    public PanelFade BlackoutCover;

    public Button ContinueButton;

    private bool quitting = false;

    void Awake()
    {
        BlackoutCover.FadeOut();
        //if (DataManager.hasExistingData) {
        //    ContinueButton.interactable = true;
        //}
    }

    private void Update()
    {
        if (GvrControllerInput.AppButton)
        {
            Quit();
        }
    }

    public void NewGame() {
        DataManager.NewGame();
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
        GvrDaydreamApi.LaunchVrHomeAsync((success) => {
            //Application.Quit(); // This results in dropping to android home before going back into daydream home.
        });
#endif
    }
}
