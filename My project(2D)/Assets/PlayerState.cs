using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System;

public class PlayerState : MonoBehaviour
{
    #region 플레이어 스테이터스 관련 수치 변수 
    public float maxHP = 100f;
    public float currentHP = 100f;

    public float maxMP = 50f;
    public float currentMP = 50f;


    public int playerLevel = 1;
    public int currentEXP = 0;
    public int maxEXP = 100;
    #endregion

    #region 플레이어 MP 소비 
    public void UseMP(float amount)
    {
        currentMP -= amount;
        if (currentMP < 0)
        {
            currentMP = 0;
        }
    }
    #endregion

    #region 플레이어 레벨업

    public void GainExp(int amount)
    {
        currentEXP += amount;

        while (currentEXP >= maxEXP)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        playerLevel++;
        currentEXP -= maxEXP;
        Debug.Log("플레이어 레벨업!");

    }
    #endregion

    #region 플레이어 데미지 입음

    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0f, maxHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }
    #endregion


    #region 플레이어 회복
    public void Heal(int amount)
    {
        if (currentHP >= maxHP)
        {
            Debug.Log("현재 플레이어의 체력이 풀입니다.");
            return;
        }

        currentHP = Mathf.Min(currentHP + amount, maxHP);
        Debug.Log($"체력 회복! (+{amount}) 현재 체력: {currentHP}/{maxHP}");

    }

    #endregion
    #region 플레이어 죽음 
    void Die()
    {
        Debug.Log("플레이어가 사망했습니다.");
    }
    #endregion

}
