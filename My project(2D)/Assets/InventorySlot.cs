using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [Header("References")]
    public Image icon;
    public TextMeshProUGUI countText;

    public Item ItemData { get; private set; }
    public bool IsEmpty => ItemData == null;
    public int StackSize { get; private set; }
    public int SlotIndex { get; private set; }

    public void Initialize(int index)
    {
        SlotIndex = index;
        Clear();
    }

    public void SetItem(Item item)
    {
        ItemData = item;
        StackSize = 1;
        UpdateUI();
    }

    public void AddToStack()
    {
        StackSize++;
        UpdateUI();
    }

    public void RemoveItem()
    {
        if (StackSize > 1)
        {
            StackSize--;
            UpdateUI();
        }
        else
        {
            Clear();
        }
    }

    void UpdateUI()
    {
        icon.sprite = ItemData.icon;
        icon.enabled = true;
        countText.text = ItemData.isStackable && StackSize > 1 ? StackSize.ToString() : "";
    }

    public void Clear()
    {
        ItemData = null;
        StackSize = 0;
        icon.sprite = null;
        icon.enabled = false;
        countText.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsEmpty)
        {
            // 아이템 정보 표시 또는 사용
            Debug.Log($"사용: {ItemData.itemName}");
        }
    }
}