using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPanel;
    public Transform slotParent;
    public GameObject shopSlotPrefab;

    private List<ItemData> shopItems = new List<ItemData>();

    void Start()
    {
        shopPanel.SetActive(false);
        shopItems.Add(new ItemData("Potion(HP)", 5));
        shopItems.Add(new ItemData("Elixir(MP)", 10));
        shopItems.Add(new ItemData("Sword", 50));
        shopItems.Add(new ItemData("Shield", 40));

        CreateSlots();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleShop();
        }
        else
        {
            return;
        }
    }

    public void ToggleShop()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
    }

    void CreateSlots()
    {
        for (int i = 0; i < shopItems.Count; i++)
        {
            GameObject slotobj = Instantiate(shopSlotPrefab, slotParent);
            ShopSlot slot = slotobj.GetComponent<ShopSlot>();
            if (slot != null)
            {
                slot.SetSlot(shopItems[i]);
            }
        }
    }
}

[System.Serializable]
public class ItemData
{
    public Sprite icon;
    public int price;
    public string itemName;

    public ItemData(string v1, int v2)
    {
        V1 = v1;
        V2 = v2;
    }

    public ItemData(Sprite icon, int price, string name)
    {
        this.icon = icon;
        this.price = price;
        this.itemName = name;
    }

    public string V1 { get; }
    public int V2 { get; }
}
