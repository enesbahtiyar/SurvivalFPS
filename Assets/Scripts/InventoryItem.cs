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
    public bool isNowEquipped;

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
