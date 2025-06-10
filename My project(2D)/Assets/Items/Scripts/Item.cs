using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{
     public string itemName = "New Item";
    public Sprite icon = null;
    public bool isStackable = false;
    public int maxStack = 1;

    // 아이템 사용 효과 (오버라이드 가능)
    public virtual void Use()
    {
        Debug.Log("Using " + itemName);
    }
}
