using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    private bool isPlayerInRange = false;
    private bool isDialogueActive = false;

    private void Start()
    {
        if (dialogueText == null && dialoguePanel != null)
        {
            dialogueText = dialoguePanel.GetComponentInChildren<TextMeshProUGUI>(true);
        }

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }

    private void Update()
    {
        // 스페이스바로 대화 시작 (근처에서만)
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            ShowDialogue("hello");
        }

        // 마우스 클릭 처리
        if(Input.GetMouseButtonDown(0))
        {
            // UI 요소 클릭시 무시 (버튼 등)
            if(EventSystem.current.IsPointerOverGameObject()) return;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if(hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                // NPC 클릭 시 대화창 열기/유지
                ShowDialogue("hello!");
            }
            else if(isDialogueActive)
            {
                // NPC 외부 빈 영역 클릭 시 대화창 닫기
                CloseDialogue();
            }
        }
    }

    private void ShowDialogue(string message)
    {
        if(dialoguePanel != null && dialogueText != null)
        {
            dialoguePanel.SetActive(true);
            dialogueText.text = message;
            isDialogueActive = true;
            Debug.Log("대화 시작:" + message);
        }
    }

    private void CloseDialogue()
    {
        if(dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
            isDialogueActive = false;
            Debug.Log("대화 종료: 외부 클릭");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("플레이어 접근 - 대화 가능");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            CloseDialogue();
            Debug.Log("플레이어 이탈 - 대화 종료");
        }
    }
}