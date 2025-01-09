using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : SingletonMonoBehaviour<CraftingSystem>
{
    [SerializeField] GameObject CraftingSystemUI;
    [SerializeField] GameObject CraftingMainUI;
    [SerializeField] GameObject CraftingToolUI;

    [SerializeField] Button toolButton;
    [SerializeField] Button backButton;

    List<string> inventoryItemList = new List<string>();
    List<CraftItemUIBox> craftableItems = new List<CraftItemUIBox>();
    public bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        CraftingSystemUI.SetActive(false);
        CraftingMainUI.SetActive(true);
        CraftingToolUI.SetActive(false);
        toolButton.onClick.AddListener(delegate { OpenTools(); });
        backButton.onClick.AddListener(delegate { BackToMainUI(); });

        PopulateCraftableItemsList();
    }

    public void CraftItem(int requiredItemQuantity1, int requiredItemQuantity2, int requiredItemQuantity3, int requiredItemQuantity4, 
        string requiredItemName1, string requiredItemName2, string requiredItemName3, string requiredItemName4, string whatToCraft)
    {
        if(!CheckIfRequiredItemsMet(requiredItemQuantity1,requiredItemQuantity2,requiredItemQuantity3,requiredItemQuantity4)) { return; }

        InventorySystem.Instance.AddToInventory(whatToCraft);

        InventorySystem.Instance.RemoveItemFromInventory(requiredItemName1, requiredItemQuantity1);
        InventorySystem.Instance.RemoveItemFromInventory(requiredItemName2, requiredItemQuantity2);
        InventorySystem.Instance.RemoveItemFromInventory(requiredItemName3, requiredItemQuantity3);
        InventorySystem.Instance.RemoveItemFromInventory(requiredItemName4, requiredItemQuantity4);

        RefreshRequiredItems();
    }

    private void BackToMainUI()
    {
        CraftingMainUI.SetActive(true);
        CraftingToolUI.SetActive(false);
    }

    void OpenTools()
    {
        CraftingMainUI.SetActive(false);
        CraftingToolUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        RefreshRequiredItems();

        if (Input.GetKeyDown(KeyCode.C))
        {
            isOpen = !isOpen;
            CraftingSystemUI.SetActive(isOpen);

            if (isOpen || InventorySystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                CraftingMainUI.SetActive(true);
                CraftingToolUI.SetActive(false);
            }
        }
    }

    private void RefreshRequiredItems()
    {
        int stoneCount, woodCount;
        CheckInventory(out stoneCount, out woodCount);

        foreach (CraftItemUIBox box in craftableItems)
        {
            if (stoneCount >= box.requiredItemQuantity2)
            {
                box.requiredItemQuantity2Meet = true;
            }
            else
            {
                box.requiredItemQuantity2Meet = false;
            }

            if (woodCount >= box.requiredItemQuantity1)
            {
                box.requiredItemQuantity1Meet = true;
            }
            else
            {
                box.requiredItemQuantity1Meet = false;
            }
        }
    }

    public void CheckInventory(out int stoneCount, out int woodCount)
    {
        stoneCount = 0;
        woodCount = 0;
        inventoryItemList = InventorySystem.Instance.itemList;

        foreach (string item in inventoryItemList)
        {
            switch (item)
            {
                case "Stone":
                    stoneCount++;
                    break;
                case "Wood":
                    woodCount++;
                    break;
            }
        }
    }

    private void PopulateCraftableItemsList()
    {
        craftableItems.Clear();

        for (int i = 0; i < CraftingToolUI.transform.childCount; i++)
        {
            CraftingToolUI.transform.GetChild(i).TryGetComponent<CraftItemUIBox>(out CraftItemUIBox craftItemUIBox);
            if (craftItemUIBox != null)
            {
                craftableItems.Add(craftItemUIBox);
            }
        }
    }
}
