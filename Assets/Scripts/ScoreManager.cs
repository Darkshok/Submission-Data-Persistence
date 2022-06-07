using System.IO;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private ScoreData[] _dataset;

    public static ScoreManager Instance;

    public string PlayerName;
    public ScoreData[] Dataset => _dataset;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitListOfRecords();
    }

    public void InitListOfRecords()
    {
        _dataset = new ScoreData[10];

        for(int i = 0; i < 10; i++)
        {
            Dataset[i].PlayerName = "Empty";
            Dataset[i].Points = 0;
        }
    }

    public void SaveScore()
    {
        SaveData saveData = new SaveData();
        saveData.Dataset = _dataset;
        saveData.LastName = PlayerName;

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            _dataset = saveData.Dataset;
            
            if(Dataset == null)
            {
                InitListOfRecords();
            }

            PlayerName = saveData.LastName;
        }
    }

    public void ClearScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if(File.Exists(path))
        {
            File.Delete(path);
        }

        InitListOfRecords();
    }

    [System.Serializable]
    public struct ScoreData
    {
        public string PlayerName;
        public int Points;

        public ScoreData(string name = "Name", int points = 0)
        {
            PlayerName = name;
            Points = points;
        }
    }

    [System.Serializable]
    public class SaveData
    {
        public ScoreData[] Dataset;
        public string LastName;
    }
}
