using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    void Start()
    {
        SpawnPlayerAtPortal();
    }

    void SpawnPlayerAtPortal()
    {
        // PortalManager 인스턴스가 있는지 확인
        if (PortalManager.Instance == null)
        {
            Debug.LogWarning("PortalManager.Instance가 null입니다. 포탈 위치에 플레이어를 스폰할 수 없습니다.");
            return;
        }

        var (lastScene, lastPortalName, lastPosition) = PortalManager.Instance.GetLastPortal();


        if (lastPosition != Vector3.zero || !string.IsNullOrEmpty(lastScene) || !string.IsNullOrEmpty(lastPortalName))
        {
            transform.position = lastPosition;
            Debug.Log($"플레이어를 저장된 스폰 위치로 이동했습니다: {lastPosition}");

        }
        else
        {
            Debug.Log("PortalManager에 저장된 유효한 스폰 위치 정보가 없습니다. 현재 위치에 스폰합니다.");
        }
    }
}
