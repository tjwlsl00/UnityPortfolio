using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region [플레이어 참조 변수]
    public PlayerState playerState; // 플레이어의 상태 정보 (체력, 마나, 경험치 등)
    #endregion

    #region [체력 UI 요소]
    public Image hpFillImage;     // 체력 게이지 (Image 컴포넌트)
    public TextMeshProUGUI hpText; // 체력 수치 텍스트
    #endregion

    #region [마나 UI 요소]
    public Image mpFillImage;     // 마나 게이지
    public TextMeshProUGUI mpText; // 마나 수치 텍스트
    #endregion

    #region [경험치 UI 요소]
    public Image expFillImage;    // 경험치 게이지
    public TextMeshProUGUI expText; // 경험치 수치 텍스트
    #endregion

    #region [인벤토리 요소]
    // 인벤토리 UI 패널 연결
    public GameObject inventoryPanel;
    private bool isInventoryOpen = false;
    #endregion

    #region [초기 설정]
    void Start()
    {
        // 플레이어 참조 초기화
        InitializePlayerReference(); 
        // UI 전체 업데이트
        UpdateAllUI(); 
        // 시작 시 인벤토리 UI 비활성화
        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);
    }

    // 플레이어 오브젝트 찾기
    void InitializePlayerReference()
    {
        if (playerState == null)
        {
            playerState = FindAnyObjectByType<PlayerState>();
            if (playerState == null)
            {
                Debug.LogError("플레이어 상태 컴포넌트를 찾을 수 없습니다!");
                enabled = false; // 이 스크립트 비활성화
            }
        }
    }
    #endregion

    #region [UI 업데이트 로직]
    void Update()
    {
        UpdateAllUI(); // 간단한 구현을 위해 매 프레임 UI 갱신

        // i 키 입력 감지
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryPanel != null)
            {
                ToggleInventory();
            }
        }
    }

    // 모든 UI 요소 업데이트
    void UpdateAllUI()
    {
        UpdateHPUI();  // 체력 UI 업데이트
        UpdateMPUI();  // 마나 UI 업데이트
        UpdateEXPUI(); // 경험치 UI 업데이트
    }

    // 체력 UI 업데이트
    void UpdateHPUI()
    {
        if (hpFillImage != null && playerState.maxHP > 0)
            hpFillImage.fillAmount = playerState.currentHP / playerState.maxHP;

        if (hpText != null)
            hpText.text = $"{playerState.currentHP:F0}/{playerState.maxHP:F0}";
    }

    // 마나 UI 업데이트
    void UpdateMPUI()
    {
        if (mpFillImage != null && playerState.maxMP > 0)
            mpFillImage.fillAmount = playerState.currentMP / playerState.maxMP;

        if (mpText != null)
            mpText.text = $"{playerState.currentMP:F0}/{playerState.maxMP:F0}";
    }

    // 경험치 UI 업데이트
    void UpdateEXPUI()
    {
        if (expFillImage != null && playerState.maxEXP > 0)
            expFillImage.fillAmount = (float)playerState.currentEXP / playerState.maxEXP;

        if (expText != null)
            expText.text = $"{playerState.currentEXP}/{playerState.maxEXP}";
    }

    // 인벤토리 UI
    public void ToggleInventory()
    {
        if (inventoryPanel != null)
        {
            isInventoryOpen = !isInventoryOpen;
            inventoryPanel.SetActive(isInventoryOpen);

            // (선택 사항) 인벤토리가 열릴 때/닫힐 때 다른 동작을 수행할 수 있습니다.
            if (isInventoryOpen)
            {
                Debug.Log("인벤토리가 열렸습니다.");
            }
            else
            {
                Debug.Log("인벤토리가 닫혔습니다.");
            }
        }
    }
    #endregion
}