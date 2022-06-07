using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField NameInput;

    private void Start()
    {
        ScoreManager.Instance.LoadScore();
        NameInput.text = ScoreManager.Instance.PlayerName;
    }

    public void StartGame()
    {
        ScoreManager.Instance.PlayerName = NameInput.text;

        SceneManager.LoadScene(3);
    }

    public void OpenHighScore()
    {
        SceneManager.LoadScene(2);
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
