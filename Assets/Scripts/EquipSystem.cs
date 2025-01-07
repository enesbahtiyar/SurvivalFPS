using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSystem : SingletonMonoBehaviour<EquipSystem>
{
    public Transform slotsParent;

    public List<GameObject> quickSlots = new List<GameObject>();


    public GameObject numberHolder;

    public int selectedNumber = -1;
    public GameObject selectedItem = null;

    public GameObject toolHandler;
    public GameObject selecteItemModel;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        PopulateSlotList();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetSelectedItem(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetSelectedItem(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetSelectedItem(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetSelectedItem(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetSelectedItem(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SetSelectedItem(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SetSelectedItem(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SetSelectedItem(8);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SetSelectedItem(9);
        }
    }

    private void SetSelectedItem(int number)
    {

        if(CheckIfSlotIsFull(number))
        {
            if(selectedNumber != number)
            {
                selectedNumber = number;

                if (selectedItem != null)
                {
                    selectedItem.GetComponent<InventoryItem>().isSelected = false;
                }
                selectedItem = GetSelectedItem(number);
                selectedItem.GetComponent<InventoryItem>().isSelected = true;

                SetEquippedModel(selectedItem);


                //changing the color
                foreach (Transform child in numberHolder.transform)
                {
                    child.GetComponent<Image>().color = Color.white;
                }

                Image imageToBeReColored = numberHolder.transform.GetChild(number - 1).GetComponent<Image>();
                imageToBeReColored.color = Color.green;
            }
            else
            {
                if (selectedItem != null)
                {
                    selectedItem.GetComponent<InventoryItem>().isSelected = false;
                }

                if (selecteItemModel != null)
                {
                    DestroyImmediate(selecteItemModel.gameObject);
                    selecteItemModel = null;
                }

                //changing the color
                foreach (Transform child in numberHolder.transform)
                {
                    child.GetComponent<Image>().color = Color.white;
                }
                selectedItem = null;
                selectedNumber = -1;
            }
        }
    }

    private void SetEquippedModel(GameObject selectedItem)
    {

        if (selecteItemModel != null)
        {
            DestroyImmediate(selecteItemModel.gameObject);
            selecteItemModel = null;
        }

        string selectedItemName = selectedItem.name.Replace("(Clone)", "");
        selecteItemModel = Instantiate(Resources.Load<GameObject>(selectedItemName + "_Model"), new Vector3(-0.06f, 0.2f, 0.58f), Quaternion.Euler(0, -20f, -4f));
        selecteItemModel.transform.SetParent(toolHandler.transform, false);
    }

    private GameObject GetSelectedItem(int number)
    {
        
        GameObject toBeSelected = slotsParent.transform.GetChild(number - 1).GetChild(0).gameObject;
        return toBeSelected;
    }

    private bool CheckIfSlotIsFull(int number)
    {
        if(slotsParent.transform.GetChild(number - 1).childCount == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void AddToQuickSlots(GameObject itemToEquip)
    {
        GameObject avaliableSlots = FindNextEmptySlot();

        itemToEquip.transform.SetParent(avaliableSlots.transform, false);

        InventorySystem.Instance.ReCalculateList();
    }



    void PopulateSlotList()
    {
        foreach (Transform child in slotsParent)
        {
            if (child.CompareTag("Slot"))
            {
                quickSlots.Add(child.gameObject);
            }
        }
    }

    private GameObject FindNextEmptySlot()
    {
        foreach (Transform child in slotsParent)
        {
            if (child.childCount == 0)
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
