using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : SingletonMonoBehaviour<CraftingSystem>
{
    [SerializeField] GameObject CraftingSystemUI;
    [SerializeField] GameObject CraftingMainUI;
    [SerializeField] GameObject CraftingToolUI;

    [SerializeField] Button toolButton;
    [SerializeField] Button backButton;


    public bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        CraftingSystemUI.SetActive(false);
        CraftingMainUI.SetActive(true);
        CraftingToolUI.SetActive(false);
        toolButton.onClick.AddListener(delegate { OpenTools(); });
        backButton.onClick.AddListener(delegate { BackToMainUI(); });
    }

    public void CraftItem(int requiredItemQuantity1, int requiredItemQuantity2, int requiredItemQuantity3, int requiredItemQuantity4)
    {
        //inventoryi kontrol et gerekli item var mı bi bak bakalım
        //gerekli item varsa itemi craft et
        //yoksa devam et
        //item craf edildikten sonra yeni olan itemi inventory'e ekle
        //kullanılan itemleri inventoryden kaldır
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
}
