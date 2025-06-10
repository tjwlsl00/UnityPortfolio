using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [System.Serializable]
    public class PortalConnection
    {
        public string targetSceneName;
        public string targetPortalName; // 필요하다면 유지하거나 삭제 가능
        public Vector3 spawnTargetPosition; // <--- 목표 좌표 필드 추가
    }

    public PortalConnection connection;

    //플레이거가 트리거 영역 안에 있는지 추적하는 변수
    private bool isPlayerInsideTrigger = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInsideTrigger = true;
            Debug.Log("플레이어가 씬 전환 영역에 진입했습니다. 위쪽 화살표 키를 눌러 이동하세요.");

            // 포탈 정보 저장: 목표 좌표를 PortalManager에 전달
            PortalManager.Instance.SetLastPortal(
                SceneManager.GetActiveScene().name,
                gameObject.name,
                connection.spawnTargetPosition // <--- 추가된 목표 좌표 필드 사용
            );
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInsideTrigger = false;
            Debug.Log("플레이어가 씬 전환 영역에서 벗어났습니다.");
        }
    }

    void Update()
    {
        if (isPlayerInsideTrigger && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("위쪽 화살표 감지됨, 씬 로드 시도");
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        // Inspector에서 설정된 다음 씬 이름이 유효한지 확인
        if (!string.IsNullOrEmpty(connection.targetSceneName))
        {
            // SceneManager를 직접 사용하여 씬 전환
            Debug.Log($"--- 씬 로드 요청: {connection.targetSceneName} (SceneManager 직접 호출) ---");
            SceneManager.LoadScene(connection.targetSceneName);
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} 오브젝트에 붙은 SceneTransitionTrigger: 로드할 다음 씬 이름(nextSceneName)이 설정되지 않았습니다!", this);
        }
    }
}
