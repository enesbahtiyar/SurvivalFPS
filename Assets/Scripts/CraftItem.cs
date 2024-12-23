using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CraftItemUIBox : MonoBehaviour
{
    [SerializeField] TMP_Text reqText1;
    [SerializeField] TMP_Text reqText2;
    [SerializeField] TMP_Text reqText3 = null;
    [SerializeField] TMP_Text reqText4 = null;

    [SerializeField] int requiredItemQuantity1;
    [SerializeField] int requiredItemQuantity2;
    [SerializeField] int requiredItemQuantity3;
    [SerializeField] int requiredItemQuantity4;

    [SerializeField] Button craftButton;

    private void Start()
    {
        craftButton.onClick.AddListener(delegate { CraftItem(); });
    }

    public void CraftItem()
    {
        CraftingSystem.Instance.CraftItem(requiredItemQuantity1, requiredItemQuantity2, requiredItemQuantity3, requiredItemQuantity4);
    }
}
