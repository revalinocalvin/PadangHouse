using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject resultUI;
    public GameObject loseUI;

    private void Awake()
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            SetUIReferences();
        }
    }

    private void SetUIReferences()
    {
        resultUI = FindInactiveObjectByName("Day Result Overview");
        loseUI = FindInactiveObjectByName("Lose Overview");

        if (resultUI == null || loseUI == null)
        {
            Debug.LogError("UI elements not found!");
        }
    }

    private GameObject FindInactiveObjectByName(string name)
    {
        Transform[] allTransforms = GetComponentsInChildren<Transform>(true);
        foreach (Transform t in allTransforms)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }
}
