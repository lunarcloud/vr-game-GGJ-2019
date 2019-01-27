using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameStoryManager : MonoBehaviour {

    private GameDataManager DataManager;
    public InkStoryManager inkManager;
    public GameObject dialogCanvas;
    public GameObject activationCanvas;
    public UnityEngine.EventSystems.EventSystem eventSystem;

    public GameObject numpadCanvas;
    public GvrKeyboard numpad;
    public Text numpadText;
    public KeyboardDelegateVendorMenu numpadDelegate;

    private int ValueOfLastNumpad = 0;

    private bool pointingAtDialogBox = false;

    void Awake ()
    {
        DataManager = FindObjectOfType<GameDataManager>();

        inkManager.storyEndAction = delegate {
            activationCanvas.SetActive(true);
            dialogCanvas.SetActive(false);

        };

        inkManager.AddTagProcessor("buy", delegate (string[] values) {
            var resourceName = inkManager.story.variablesState["TalkingAboutResource"].ToString();
            var quantity = ValueOfLastNumpad;
            Debug.Log($"Buying {quantity} of {resourceName}.");
            var resource = ResourceType.Types.First(r => r.Name == resourceName);
            var resourceCost = DataManager.Game.Player.Location.BuyPrices[resource];

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
            var resourceName = inkManager.story.variablesState["TalkingAboutResource"].ToString();
            var quantity = ValueOfLastNumpad;
            Debug.Log($"Selling {quantity} of {resourceName}.");
            var resource = ResourceType.Types.First(r => r.Name == resourceName);
            var resourceCost = DataManager.Game.Player.Location.SellPrices[resource];

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
        numpadDelegate.KeyboardEnterPressed += (s, e) => {
            DoneNumpad();
        };
    }

    public void NumpadShow()
    {
        numpad.ClearText();
        numpadText.text = "";
        dialogCanvas.SetActive(false);
        numpadCanvas.SetActive(true);
        numpad.gameObject.SetActive(true);
    }

    public void DoneNumpad() {
        var success = int.TryParse(numpad.EditorText, out ValueOfLastNumpad);
        if (!success)
        {
            Debug.LogError($"{numpad.EditorText} can't be parsed as an int!");
            ValueOfLastNumpad = 0;
        }
        NumpadHide();
        inkManager.Continue();
    }

    private void NumpadHide()
    {
        numpad.ClearText();
        numpadText.text = "";
        dialogCanvas.SetActive(true);
        numpadCanvas.SetActive(false);
        numpad.gameObject.SetActive(false);
    }

    public void StartStory()
    {
        dialogCanvas.SetActive(true);
        activationCanvas.SetActive(false);
        inkManager.StartStory();
        var location = DataManager.Game.Player.Location;
        inkManager.story.variablesState["PlayerName"] = DataManager.Game.Player.PlayerName;
        var absoluteFriendliness = location.Friendliness;
        inkManager.story.variablesState["Friendliness"] = absoluteFriendliness <= 0.3 ? "Low"
                                                        : absoluteFriendliness >= 0.7 ? "High" 
                                                        : "Normal";
        inkManager.story.variablesState["Religion"] = location.Religion.Name;
        inkManager.story.variablesState["Family"] = location.Family.Name;
        inkManager.story.variablesState["Bantering"] = location.Bantering.Name;


        inkManager.Continue();
    }
    
    private void Update()
    {
        if (GvrControllerInput.AppButton) dialogCanvas.SetActive(false);
    }

}
