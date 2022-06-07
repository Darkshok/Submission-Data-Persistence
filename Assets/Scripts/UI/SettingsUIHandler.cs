using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingsUIHandler : MonoBehaviour
{
    [SerializeField] private Slider _lineCountSlider;
    [SerializeField] private TextMeshProUGUI _lineCountText;

    private void Start()
    {
        SettingsManager.Instance.LoadSettings();
        _lineCountSlider.value = SettingsManager.Instance.LineCount;
        _lineCountText.text = _lineCountSlider.value.ToString();
    }

    public void ChangeLineCount()
    {
        SettingsManager.Instance.LineCount = (int)_lineCountSlider.value;
        _lineCountText.text = _lineCountSlider.value.ToString();
    }

    public void ClearHighScores()
    {
        ScoreManager.Instance.ClearScore();
    }

    public void BackToMenu()
    {
        SettingsManager.Instance.SaveSettings();
        SceneManager.LoadScene(0);
    }
}
