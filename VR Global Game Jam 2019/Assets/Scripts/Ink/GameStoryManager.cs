using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;

public class GameStoryManager : MonoBehaviour {

    private GameDataManager DataManager;
    public InkStoryManager inkManager;
    public GameObject dialogCanvas;
    public GameObject activationCanvas;
    public UnityEngine.EventSystems.EventSystem eventSystem;

    public GameObject numpadCanvas;
    public GvrKeyboard numpad;

    private bool pointingAtDialogBox = false;

    void Awake ()
    {
        DataManager = FindObjectOfType<GameDataManager>();

        inkManager.storyEndAction = delegate {
            activationCanvas.SetActive(true);
            dialogCanvas.SetActive(false);

        };

        inkManager.AddTagProcessor("buy", delegate (string[] values) {
            var resourceName = values[0];
            var success = int.TryParse(values[1], out var quantity);
            if (!success) {
                Debug.LogError($"{resourceName} quantity ({values[1]}) can't be parsed as an int!");
                return;
            }
            var resource = ResourceType.Types.First(r => r.Name == resourceName);
            var resourceCost = DataManager.Game.Player.Location.ResourceCosts[resource];

            if (DataManager.Game.Player.Currency >= resourceCost * quantity)
            {
                DataManager.Game.Player.Currency -= resourceCost * quantity;
                if (DataManager.Game.Player.Inventory.ContainsKey(resource) == false)
                {
                    DataManager.Game.Player.Inventory.Add(resource, 0);
                }
                DataManager.Game.Player.Inventory[resource] += quantity;

                inkManager.story.variablesState["SuccessfulBuy"] = true;
            }
            else
            {
                inkManager.story.variablesState["SuccessfulBuy"] = false;
            }
        });

        inkManager.AddTagProcessor("sell", delegate (string[] values) {
            var resourceName = values[0];
            var success = int.TryParse(values[1], out var quantity);
            if (!success)
            {
                Debug.LogError($"{resourceName} quantity ({values[1]}) can't be parsed as an int!");
                return;
            }
            var resource = ResourceType.Types.First(r => r.Name == resourceName);
            var resourceCost = DataManager.Game.Player.Location.ResourceCosts[resource];

            if (DataManager.Game.Player.Inventory.ContainsKey(resource)
                && DataManager.Game.Player.Inventory[resource] >= quantity)
            {
                DataManager.Game.Player.Currency += resourceCost * quantity;
                DataManager.Game.Player.Inventory[resource] -= quantity;
                inkManager.story.variablesState["SuccessfulSell"] = true;
            }
            else
            {
                inkManager.story.variablesState["SuccessfulSell"] = false;
            }
        });

        inkManager.AddTagProcessor("numpadShow", delegate (string[] values) {
            NumpadShow();
        });
        numpad.GetComponent<KeyboardDelegateVendorMenu>().KeyboardEnterPressed += (s, e) => {
            NumpadHide();
            inkManager.Continue();
        };
        inkManager.AddTagProcessor("numpadRetreive", delegate (string[] values) {
            inkManager.story.variablesState["NumpadValue"] = 1;
        });
    }

    public void NumpadShow()
    {
        dialogCanvas.SetActive(false);
        numpadCanvas.SetActive(true);
        numpad.gameObject.SetActive(true);
    }

    public void NumpadHide()
    {
        dialogCanvas.SetActive(true);
        numpadCanvas.SetActive(false);
        numpad.gameObject.SetActive(false);
    }

    public void StartStory()
    {
        dialogCanvas.SetActive(true);
        activationCanvas.SetActive(false);
        inkManager.StartStory();
        inkManager.story.variablesState["PlayerName"] = DataManager.Game.Player.PlayerName;
        inkManager.Continue();
    }
    
    private void Update()
    {
        if (GvrControllerInput.AppButton) dialogCanvas.SetActive(false);
    }

}
