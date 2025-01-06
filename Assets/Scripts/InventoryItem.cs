using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool isTrashable;

    public string thisName, thisDescription, thisFunction;
    //burada çeşitli stringler tutup bunu muse ile üzerine geldiğimzde bi pop-up olarak oyuncuya gösterebiliriz

    private GameObject itemPendingConsumption;
    public bool isConsumable;
    public float healthEffect;
    public float foodEffect;
    public float hydrationEffect;

    public bool isEquiapple;
    private GameObject itemPendingEquipping;
    public bool isOnQuickSlot;
    public bool isSelected;


    private void Update()
    {
        if(isSelected)
        {
            gameObject.GetComponent<DragDrop>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<DragDrop>().enabled = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(thisName + "\n"
            + thisDescription + "\n"
            + thisFunction + "\n");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("selam");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log(EquipSystem.Instance.CheckIfFull());
            if(isEquiapple && !isOnQuickSlot && EquipSystem.Instance.CheckIfFull() == false)
            {
                EquipSystem.Instance.AddToQuickSlots(this.gameObject);
                isOnQuickSlot = true;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("mouse geri çekildi");
    }
}
