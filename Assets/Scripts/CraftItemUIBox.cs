using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CraftItemUIBox : MonoBehaviour
{
    [SerializeField] TMP_Text reqText1;
    [SerializeField] TMP_Text reqText2;
    [SerializeField] TMP_Text reqText3 = null;
    [SerializeField] TMP_Text reqText4 = null;

    public int requiredItemQuantity1;
    public int requiredItemQuantity2;
    public int requiredItemQuantity3;
    public int requiredItemQuantity4;
    public string requiredItemName1;
    public string requiredItemName2;
    public string requiredItemName3 = null;
    public string requiredItemName4 = null;


    [HideInInspector] public bool requiredItemQuantity1Meet;
    [HideInInspector] public bool requiredItemQuantity2Meet;
    [HideInInspector] public bool requiredItemQuantity3Meet;
    [HideInInspector] public bool requiredItemQuantity4Meet;

    [SerializeField] Button craftButton;

    public string whatToCraft;
    private void Start()
    {
        craftButton.onClick.AddListener(delegate { CraftItem(); });
    }

    public void CraftItem()
    {
        CraftingSystem.Instance.CraftItem(requiredItemQuantity1, requiredItemQuantity2, requiredItemQuantity3, requiredItemQuantity4, requiredItemName1, requiredItemName2, requiredItemName3, requiredItemName4, whatToCraft);
    }

    private void Update()
    {
        ChangeColorOfTexts();

        UpdateTexts();
    }

    private void UpdateTexts()
    {
        int stoneCount, woodCount;
        CraftingSystem.Instance.CheckInventory(out stoneCount, out woodCount);

        reqText1.text = requiredItemQuantity1 + " " + requiredItemName1 + " [" + woodCount + "]";
        reqText2.text = requiredItemQuantity2 + " " + requiredItemName2 + " [" + stoneCount + "]";
    }

    private void ChangeColorOfTexts()
    {
        if(requiredItemQuantity1Meet)
        {
            reqText1.color = Color.green;
        }
        else
        {
            reqText1.color = Color.red;
        }

        if(requiredItemQuantity2Meet)
        {
            reqText2.color = Color.green;
        }
        else
        {
            reqText2.color = Color.red;
        }
    }
}
