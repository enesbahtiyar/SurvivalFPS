using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : SingletonMonoBehaviour<InventorySystem>
{
    public GameObject Inventory;
    public bool isOpen;

    public Transform slotsParent;

    public List<GameObject> inventorySlots = new List<GameObject>();

    public List<string> itemList = new List<string>();

    GameObject itemToAdd;

    GameObject whatSlotToPut;


    protected override void Awake()
    {
        base.Awake();
        PopulateSlotList();
    }

    private void Start()
    {
        isOpen = false;
    }


    void PopulateSlotList()
    {
        foreach (Transform child in slotsParent)
        {
            if(child.CompareTag("Slot"))
            {
                inventorySlots.Add(child.gameObject);
            }
        }
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            isOpen = !isOpen;
            Inventory.SetActive(isOpen);

            if(isOpen || CraftingSystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void AddToInventory(string itemName)
    {
        whatSlotToPut = FindNextEmptySlot();
        itemToAdd = Instantiate(Resources.Load<GameObject>(itemName), whatSlotToPut.transform.position, whatSlotToPut.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToPut.transform);

        itemList.Add(itemName);   
    }

    public void RemoveFromInventoryToWorld(GameObject gameObject)
    {
        Destroy(gameObject);
        //TODO: yere item at 
    }

    public void RemoveItemFromInventory(string itemName, int count)
    {
        int counter = count;

        Debug.Log(itemName);
        Debug.Log(counter);
        for(int i = slotsParent.childCount -1; i >= 0; i--)
        {
            if(slotsParent.GetChild(i).childCount > 0)
            {
                if (slotsParent.GetChild(i).GetChild(0).name == itemName + "(Clone)" && counter != 0)
                {
                    Destroy(slotsParent.GetChild(i).GetChild(0).gameObject);
                    counter--;
                    itemList.Remove(itemName);
                    Debug.Log("item silindi");
                    Debug.Log(counter);
                }
            }
        }
    }





    private GameObject FindNextEmptySlot()
    {
        foreach(Transform child in slotsParent)
        {
            if(child.childCount == 0)
            {
                return child.gameObject;
            }
        }

        return null;
    }

    public bool CheckIfFull()
    {
        int counter = 0;

        foreach (Transform slot in slotsParent)
        {
            if (slot.childCount > 0)
            {
                counter++;
            }
        }

        if (counter == slotsParent.childCount)
        {
            return true;
        }
        else return false;
    }

}
