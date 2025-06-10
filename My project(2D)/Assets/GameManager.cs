using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Persistent Objects")]
    public GameObject player;          // 플레이어 오브젝트 (인스펙터 할당)
    public GameObject gameUI;          // 게임 UI (인스펙터 할당)
    public EventSystem gameEventSystem;// UI 이벤트 시스템 (인스펙터 할당)

    [Header("Settings")]
    public string defaultNickname = "Guest";
    private string playerNickname;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializePersistentObjects();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            DestroyDuplicateObjects();
        }

        // 게임 시작시 커서 설정(보이기)
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void InitializePersistentObjects()
    {
        DontDestroyOnLoad(gameObject);
        if (player) DontDestroyOnLoad(player);
        if (gameUI) DontDestroyOnLoad(gameUI);
        if (gameEventSystem) DontDestroyOnLoad(gameEventSystem);

        // 초기 닉네임 로드
        playerNickname = PlayerPrefs.GetString("PlayerNickname", defaultNickname);
    }

    void DestroyDuplicateObjects()
    {
        if (player) Destroy(player);
        if (gameUI) Destroy(gameUI);
        if (gameEventSystem) Destroy(gameEventSystem);
        Destroy(gameObject);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "LoginScene")
        {
            SetupNewScene();
        }
    }

    void SetupNewScene()
    {
        PositionPlayer();
        ConfigureUI();
        CleanDuplicateEventSystems();
    }

    void PositionPlayer()
    {
        if (!player) return;

        // 스폰 포인트 찾거나 기본 위치 설정
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        player.transform.position = spawnPoint ? spawnPoint.transform.position : Vector3.zero;

        // 플레이어 활성화 (씬 전환 시 비활성화 방지)
        player.SetActive(true);
    }

    void ConfigureUI()
    {
        if (!gameUI) return;

        gameUI.SetActive(true);

        // Canvas 설정
        Canvas canvas = gameUI.GetComponent<Canvas>();
        if (canvas)
        {
            if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            {
                canvas.worldCamera = Camera.main;
            }

            // UI 위치 초기화
            canvas.transform.SetAsLastSibling();
        }
    }

    void CleanDuplicateEventSystems()
    {
        // 중복 EventSystem 제거
        EventSystem[] eventSystems = FindObjectsByType<EventSystem>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (EventSystem es in eventSystems)
        {
            if (es != gameEventSystem)
            {
                Destroy(es.gameObject);
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        // 씬 전환 전 플레이어 임시 비활성화 (필요시)
        if (player) player.SetActive(false);
        SceneManager.LoadScene(sceneName);
    }

    // 닉네임 관리
    public void SetPlayerNickname(string nickname)
    {
        playerNickname = nickname;
        PlayerPrefs.SetString("PlayerNickname", nickname);
    }

    public string GetPlayerNickname() => playerNickname;
}