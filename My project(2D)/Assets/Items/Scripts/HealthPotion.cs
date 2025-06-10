using UnityEngine;

[CreateAssetMenu(fileName = "Health Potion", menuName = "Items/Health Potion")]
public class HealthPotion : Item
{
    public int healAmount = 30;
    public int manaCost = 10; // 필요시 마나 소모량 추가

    public override void Use()
    {
        PlayerState player = FindAnyObjectByType<PlayerState>();
        
        if (player == null)
        {
            Debug.LogError("플레이어 체력 시스템 없음!");
            return;
        }

        if (player.currentHP >= player.maxHP)
        {
            Debug.Log("체력이 이미 가득 찼습니다!");
            return;
        }

        // 마나 소모가 필요한 경우 (선택사항)
        // if (!player.SpendMana(manaCost)) return;

        player.Heal(healAmount);
        Debug.Log($"회복 물약 사용! (+{healAmount} HP)");
    }
}