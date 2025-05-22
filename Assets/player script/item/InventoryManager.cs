using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public List<ItemData> items = new List<ItemData>();
    public InventoryUI inventoryUI;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddItem(ItemData newItem)
    {
        items.Add(newItem);
        inventoryUI.RefreshUI(); // UI °»½Å
    }
}
