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

            if(isOpen)
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

    public void RemoveFromInventory(GameObject gameObject)
    {
        Destroy(gameObject);
        //TODO: yere item at 
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
