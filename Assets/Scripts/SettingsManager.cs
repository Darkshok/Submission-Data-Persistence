using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private int _lineCount = 6;

    public static SettingsManager Instance;

    public int LineCount
    {
        get { return _lineCount; }
        set { _lineCount = value; }
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveSettings()
    {
        SaveData saveData = new SaveData();
        saveData.LineCount = _lineCount;

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(Application.persistentDataPath + "/settings.json", json);
    }

    public void LoadSettings()
    {
        string path = Application.persistentDataPath + "/settings.json";

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            _lineCount = saveData.LineCount;
        }
    }

    public class SaveData
    {
        public int LineCount;
    }
}
