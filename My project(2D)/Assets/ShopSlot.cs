using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI itemPriceText;
    public TextMeshProUGUI itemNameText;

    public void SetSlot(ItemData item)
    {
        if (itemIcon != null)
        {
            itemIcon.sprite = item.icon;
            itemIcon.gameObject.SetActive(item.icon != null); // 아이콘이 없으면 이미지 비활성화
        }

        if (itemNameText != null)
        {
            itemNameText.text = item.itemName;
        }

        if (itemPriceText != null)
        {
            itemPriceText.text = item.price.ToString();
        }
    }
}
