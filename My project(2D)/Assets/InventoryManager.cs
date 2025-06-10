using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [Header("UI Settings")]
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public Transform slotParent;

    [Header("Inventory Settings")]
    public int inventorySize = 9;
    public List<Item> items = new List<Item>();

    [Header("Test Item")]
    public HealthPotion healthPotion; // 인스펙터에서 할당할 회복 물약

    private List<InventorySlot> slots = new List<InventorySlot>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeInventory();
        inventoryPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryPanel != null)
            {
                ToggleInventory();
            }
        }

        // Z 키로 회복 물약 추가
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AddItem(healthPotion);
        }
    }

    public void ToggleInventory()
    {
        if (inventoryPanel != null)
        {
            bool isActive = !inventoryPanel.activeSelf;
            inventoryPanel.SetActive(!isActive);
        }
    }

    void InitializeInventory()
    {
        // 기존 슬롯 정리
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }
        slots.Clear();

        // 새 슬롯 생성
        for (int i = 0; i < inventorySize; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotParent);
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            Image slotImage = slot.GetComponent<Image>();
            
            if (slotImage != null)
            {
                slotImage.enabled = true;
            }

            slot.Initialize(i);
            slots.Add(slot);
        }
        Debug.Log("InitializeInventory() 호출됨, 슬롯 개수: " + slots.Count); // 디버그 로그 추가
    }

    public bool AddItem(Item item)
    {
        // 1. 기존 슬롯에 추가 (스택 가능한 경우)
        foreach (InventorySlot slot in slots)
        {
            if (!slot.IsEmpty && slot.ItemData == item && item.isStackable)
            {
                slot.AddToStack();
                return true;
            }
        }

        // 2. 빈 슬롯에 추가
        foreach (InventorySlot slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.SetItem(item);
                return true;
            }
        }

        Debug.Log("인벤토리가 가득 찼습니다!");
        return false;
    }

    public void RemoveItem(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < slots.Count)
        {
            slots[slotIndex].RemoveItem();
        }
    }

}
