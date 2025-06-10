using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public static LoginManager Instance;

    [Header("Scene Settings")]
    public string sceneAfterLogin = "GameScene";

    [Header("Login UI")]
    public TMP_InputField nicknameInputField;
    public GameObject welcomePanel;
    public TMP_Text welcomeText;
    public Button startButton;
    public GameObject inputFieldGameObject;

    private string playerNickname;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "LoginScene")
        {
            InitializeLoginScene();
        }
    }

    void InitializeLoginScene()
    {
        welcomePanel?.SetActive(false);
        startButton?.gameObject.SetActive(false);
        inputFieldGameObject?.SetActive(true);

        if (startButton != null)
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(OnStartButtonClicked);
            nicknameInputField?.Select();
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "LoginScene")
        {
            if (nicknameInputField == null) return;

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                ProcessLogin();
            }
        }
    }

    void ProcessLogin()
    {
        string nickname = nicknameInputField?.text;

        if (!string.IsNullOrEmpty(nickname))
        {
            playerNickname = nickname;
            PlayerPrefs.SetString("PlayerNickname", nickname);
            PlayerPrefs.Save();

            if (welcomePanel != null)
            {
                welcomePanel.SetActive(true);
                if (welcomeText != null)
                {
                    welcomeText.text = "hello!" + nickname;
                }
            }

            inputFieldGameObject?.SetActive(false);
            startButton?.gameObject.SetActive(true);
        }
        else
        {
            nicknameInputField?.ActivateInputField();
        }
    }

    public void OnStartButtonClicked()
    {
        // GameManager에 닉네임 전달
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetPlayerNickname(playerNickname);
        }

        // 로그인 UI 정리
        nicknameInputField = null;
        welcomePanel = null;
        welcomeText = null;
        startButton = null;
        inputFieldGameObject = null;

        SceneManager.LoadScene(sceneAfterLogin);
    }

    public string GetPlayerNickname()
    {
        return playerNickname;
    }
}