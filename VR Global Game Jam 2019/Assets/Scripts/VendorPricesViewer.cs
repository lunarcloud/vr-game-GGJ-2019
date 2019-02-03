using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class VendorPricesViewer : MonoBehaviour
{
    private GameDataManager DataManager;

    [SerializeField]
    private Text PriceItemPrefab;

    [SerializeField]
    private GameObject PricesListParent;

    private PlanetData selectedPlanet;

    private void Start()
    {
        DataManager = FindObjectOfType<GameDataManager>();
        if (DataManager.Game.Player.Location != null)
            LoadPlanetData(DataManager.Game.Player.Location);
    }

    public void LoadPlanetData(PlanetData data) {
        selectedPlanet = data;
        ReloadPlanetData();
    }

    public void ReloadPlanetData()
    {
        if (selectedPlanet == null) return;

        // Clear List, then build
        foreach (Transform child in PricesListParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        var buyPrices = selectedPlanet.BuyPrices;
        var sellPrices = selectedPlanet.SellPrices;
        foreach (var resource in ResourceType.Types)
        {
            var buyPrice = buyPrices[resource];
            var sellPrice = sellPrices[resource];
            AddPriceItem($" {resource.Name} : $ {(buyPrice / 100)}.{(buyPrice % 100):00} / $ {(sellPrice / 100)}.{(sellPrice % 100):00}");
        }
    }
    
    private void AddPriceItem(string text)
    {
        Text item = Instantiate(PriceItemPrefab) as Text;
        item.text = text;
        item.transform.SetParent(PricesListParent.transform, false);
    }

}
