using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject ListOfRecords;
    [SerializeField] private GameObject PrefabRecord;

    private void Start()
    {
        InitScreenScore();
    }

    private void InitScreenScore()
    {
        for(int i = 0; i < ScoreManager.Instance.Dataset.Length; i++)
        {
            ScoreManager.ScoreData data = ScoreManager.Instance.Dataset[i];

            if(data.PlayerName != "Empty")
            {
                var record = Instantiate(PrefabRecord, transform.position, Quaternion.identity);
                record.transform.SetParent(ListOfRecords.transform);

                var rank = record.transform.GetChild(0);
                rank.GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();

                var name = record.transform.GetChild(1);
                name.GetComponent<TextMeshProUGUI>().text = data.PlayerName;

                var score = record.transform.GetChild(2);
                score.GetComponent<TextMeshProUGUI>().text = data.Points.ToString();
            }
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
