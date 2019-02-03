using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class GameStoryManager : MonoBehaviour {

    private GameDataManager DataManager;
    public InkStoryManager inkManager;
    public GameObject dialogCanvas;
    public GameObject activationCanvas;
    public UnityEngine.EventSystems.EventSystem eventSystem;
    
    private GvrModeManager GvrMode;
    public GvrKeyboard daydreamNumpad;
    public KeyboardDelegateVendorMenu daydreamNumpadDelegate;

    public GameObject numpadCanvas;
    public Text numpadText;
    public GameObject inventoryCanvas;

    public VendorManager vendorManager;

    public ShipInventoryViewer inventoryViewer;

    private int ValueOfLastNumpad = 0;

    private bool pointingAtDialogBox = false;
    
    public Text FriendlinessText;

    void Awake ()
    {
        DataManager = FindObjectOfType<GameDataManager>();
        GvrMode = FindObjectOfType<GvrModeManager>();
        
        inkManager.storyEndAction = delegate {
            activationCanvas.SetActive(true);
            dialogCanvas.SetActive(false);
            vendorManager.BackToPort();
        };

        inkManager.AddTagProcessor("buy", delegate (string[] values) {
            var resourceName = inkManager.story.variablesState["TalkingAboutResource"].ToString();
            var quantity = ValueOfLastNumpad;
            Debug.Log($"Buying {quantity} of {resourceName}.");
            var resource = ResourceType.Types.First(r => r.Name == resourceName);
            var resourceCost = DataManager.Game.Player.Location.BuyPrices[resource];

            inkManager.story.variablesState["CancelTrade"] = quantity == 0;

            if (DataManager.Game.Player.Currency >= resourceCost * quantity
                && quantity > 0)
            {
                DataManager.Game.Player.Currency -= resourceCost * quantity;
                if (DataManager.Game.Player.Inventory.ContainsKey(resource) == false)
                {
                    DataManager.Game.Player.Inventory.Add(resource, 0);
                }
                DataManager.Game.Player.Inventory[resource] += quantity;

                inkManager.story.variablesState["SuccessfulBuy"] = true;
                vendorManager.ShakeHands();
                updateInventoryView();
            }
            else
            {
                inkManager.story.variablesState["SuccessfulBuy"] = false;
            }
            inkManager.Continue();
            dialogCanvas.SetActive(true);
        });

        inkManager.AddTagProcessor("sell", delegate (string[] values) {
            var resourceName = inkManager.story.variablesState["TalkingAboutResource"].ToString();
            var quantity = ValueOfLastNumpad;
            Debug.Log($"Selling {quantity} of {resourceName}.");
            var resource = ResourceType.Types.First(r => r.Name == resourceName);
            var resourceCost = DataManager.Game.Player.Location.SellPrices[resource];

            inkManager.story.variablesState["CancelTrade"] = quantity == 0;

            if (DataManager.Game.Player.Inventory.ContainsKey(resource)
                && DataManager.Game.Player.Inventory[resource] >= quantity
                && quantity > 0)
            {
                DataManager.Game.Player.Currency += resourceCost * quantity;
                DataManager.Game.Player.Inventory[resource] -= quantity;
                inkManager.story.variablesState["SuccessfulSell"] = true;
                vendorManager.ShakeHands();
                updateInventoryView();
            }
            else
            {
                inkManager.story.variablesState["SuccessfulSell"] = false;
            }
            inkManager.Continue();
            dialogCanvas.SetActive(true);
        });

        inkManager.AddTagProcessor("friendliness", delegate (string[] values) {
            var success = float.TryParse(values[0], out var updatedValue);
            if (!success)
            {
                Debug.LogError($"{values[0]} can't be parsed as an int!");
                ValueOfLastNumpad = 0;
            }
            updateFriendliness(DataManager.Game.Player.Location.Friendliness + updatedValue);
        });

        inkManager.AddTagProcessor("numpadShow", delegate (string[] values) { NumpadShow(); });
        inkManager.AddTagProcessor("numpadHide", delegate (string[] values) { NumpadHide(); });
        inkManager.AddTagProcessor("inventoryShow", delegate (string[] values) { InventoryShow(); });
        inkManager.AddTagProcessor("inventoryHide", delegate (string[] values) { InventoryHide(); });

        if (GvrMode.IsDaydream)
        {
            daydreamNumpadDelegate.KeyboardEnterPressed += (s, e) => { NumpadValueAccepted(); };
        }
    }

    private void InventoryHide()
    {
        inventoryCanvas.SetActive(false);
    }

    private void InventoryShow()
    {
        inventoryCanvas.SetActive(true);
    }

    private void Start()
    {
        NumpadHide();
        updateFriendlinessUI();
    }

    public void NumpadShow()
    {
        numpadText.text = "";
        dialogCanvas.SetActive(false);
        numpadCanvas.SetActive(true);
        if (GvrMode.IsDaydream)
        {
            daydreamNumpad.ClearText();
            daydreamNumpad.gameObject.SetActive(true);
        }
    }

    public void NumpadValueAccepted() {
        var success = int.TryParse(numpadText.text, out ValueOfLastNumpad);
        if (!success)
        {
            Debug.LogError($"{numpadText.text} can't be parsed as an int!");
            ValueOfLastNumpad = 0;
        }
        inkManager.Continue();
        NumpadHide();
    }

    private void NumpadHide()
    {
        numpadText.text = "";
        numpadCanvas.SetActive(false);

        if (GvrMode.IsDaydream)
        {
            daydreamNumpad.ClearText();
            daydreamNumpad.gameObject.SetActive(false);
        }
    }

    public void StartStory()
    {
        dialogCanvas.SetActive(true);
        activationCanvas.SetActive(false);
        inkManager.StartStory();
        var location = DataManager.Game.Player.Location;
        inkManager.story.variablesState["PlayerName"] = DataManager.Game.Player.PlayerName;
        updateFriendliness();
        inkManager.story.variablesState["Religion"] = location.Religion.Name;
        inkManager.story.variablesState["Family"] = location.Family.Name;
        inkManager.story.variablesState["Bantering"] = location.Bantering.Name;


        inkManager.Continue();
    }

    private void updateFriendliness()
    {
        var value = DataManager.Game.Player.Location.Friendliness;
        inkManager.story.variablesState["Friendliness"] = value <= 0.3 ? "Low"
                                                        : value >= 0.7 ? "High"
                                                        : "Normal";
        updateFriendlinessUI();
    }

    private void updateFriendlinessUI() {
        FriendlinessText.text = "";
        for (var i = 1; i <= DataManager.Game.Player.Location.Friendliness * 5; i++)
        {
            FriendlinessText.text += "❤️";
        }
    }

    private void updateFriendliness(float value)
    {
        DataManager.Game.Player.Location.Friendliness = value;
        updateFriendliness();
    }

    private void updateInventoryView() {
        inventoryViewer.Reset();
    }

    private void Update()
    {
        if (GvrControllerInput.AppButton) dialogCanvas.SetActive(false);
    }

}
