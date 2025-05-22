using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData; // 이 아이템이 어떤 아이템인지 연결

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 닿으면
        {
            InventoryManager.instance.AddItem(itemData); // 인벤토리에 추가
            Destroy(gameObject); // 아이템 오브젝트 제거
        }
    }
}
