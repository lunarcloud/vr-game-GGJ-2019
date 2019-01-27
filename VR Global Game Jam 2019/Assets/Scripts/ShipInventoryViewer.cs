using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipInventoryViewer : MonoBehaviour
{
    private GameDataManager DataManager;

    [SerializeField]
    private Text ItemPrefab;

    [SerializeField]
    private GameObject ListContent;

    public Text moneyText;

    // Start is called before the first frame update
    void Start()
    {
        DataManager = FindObjectOfType<GameDataManager>();
        Reset();
    }

    public void Reset()
    {
        foreach (Transform child in ListContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        moneyText.text = $" $ {(DataManager.Game.Player.Currency / 100)}.{(DataManager.Game.Player.Currency % 100):00}";
        foreach (var inventory in DataManager.Game.Player.Inventory)
        {
            AddListItem(inventory.Key + " : " + inventory.Value);
        }
    }

    private void AddListItem(string text)
    {
        Text item = Instantiate(ItemPrefab) as Text;
        item.text = text;
        item.transform.SetParent(ListContent.transform, false);
    }
}
