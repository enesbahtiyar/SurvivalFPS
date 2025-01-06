using System.Collections.Generic;
using UnityEngine;

public class QuickSlotSystem : SingletonMonoBehaviour<QuickSlotSystem>
{
    public Transform slotsParent;

    public List<GameObject> quickSlots = new List<GameObject>();
    public List<string> itemList = new List<string>();
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        PopulateSlotList();
    }

    public void AddToQuickSlots(GameObject itemToEquip)
    {
        GameObject avaliableSlots = FindNextEmptySlot();

        itemToEquip.transform.SetParent(avaliableSlots.transform.parent);

        string cleanName = itemToEquip.name.Replace("(Clone)", "");

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
