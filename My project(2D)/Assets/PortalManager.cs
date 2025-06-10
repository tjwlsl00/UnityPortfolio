using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public static PortalManager Instance;

    private string lastSceneName;
    private string lastPortName;
    private Vector3 lastPortalPosition;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLastPortal(string sceneName, string portalName, Vector3 position)
    {
        lastSceneName = sceneName;
        lastPortName = portalName;
        lastPortalPosition = position;

    }

    public (string, string, Vector3) GetLastPortal()
    {
        return (lastSceneName, lastPortName, lastPortalPosition);
    }
}
