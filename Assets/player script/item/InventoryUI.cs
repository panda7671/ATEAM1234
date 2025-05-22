using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Transform contentPanel;  // 슬롯들 들어갈 부모
    public GameObject slotPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            if (inventoryPanel.activeSelf)
                RefreshUI();
        }
    }

    public void RefreshUI()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in InventoryManager.instance.items)
        {
            GameObject slot = Instantiate(slotPrefab, contentPanel);
            slot.GetComponentInChildren<Image>().sprite = item.itemIcon;
        }
    }
}
