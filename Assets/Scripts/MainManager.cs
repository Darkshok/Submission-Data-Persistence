using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField] private Brick _brickPrefab;
    [SerializeField] private int _lineCount = 6;
    [SerializeField] private Rigidbody _ball;

    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _bestScoreText;
    [SerializeField] private GameObject _gameOverText;
    
    private bool _started = false;
    private bool _gameOver = false;

    private int _points;

    private void Start()
    {
        //loading settings
        _lineCount = SettingsManager.Instance.LineCount;
        //---

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5, 9 };
        for (int i = 0; i < _lineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(_brickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        if(ScoreManager.Instance != null)
        {
            _bestScoreText.text = $"Best Score: {ScoreManager.Instance.Dataset[0].PlayerName} - {ScoreManager.Instance.Dataset[0].Points}";
        }
    }

    private void Update()
    {
        if (!_started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                _ball.transform.SetParent(null);
                _ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (_gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        _points += point;
        _scoreText.text = $"Score : {_points}";
    }

    private void ChangeHighScore()
    {
        if(ScoreManager.Instance != null)
        {
            DeletePreRecord();

            AddNewRecord();

            ScoreManager.Instance.SaveScore();
        }

        void DeletePreRecord()
        {
            for(int i = 0; i < ScoreManager.Instance.Dataset.Length; i++)
            {
                if(ScoreManager.Instance.PlayerName == ScoreManager.Instance.Dataset[i].PlayerName)
                {
                    ShiftLeft(ScoreManager.Instance.Dataset, i);

                    return;
                }
            }
        }

        void AddNewRecord()
        {
            for(int i = 0; i < ScoreManager.Instance.Dataset.Length; i++)
            {
                if(_points > ScoreManager.Instance.Dataset[i].Points)
                {
                    ShiftRight(ScoreManager.Instance.Dataset, i);
                    ScoreManager.Instance.Dataset[i].PlayerName = ScoreManager.Instance.PlayerName;
                    ScoreManager.Instance.Dataset[i].Points = _points;

                    return;
                }
            }
        }

        void ShiftRight<T>(T[] arr, int index)
        {
            for(int i = arr.Length - 1; i > index; i--)
            {
                arr[i] = arr[i - 1];
            }
        }

        void ShiftLeft<T>(T[] arr, int index)
        {
            for(int i = index; i < arr.Length - 1; i++)
            {
                arr[i] = arr[i + 1];
            }
        }
    }

    public void GameOver()
    {
        _gameOver = true;
        _gameOverText.SetActive(true);

        ChangeHighScore();

        _bestScoreText.text = $"Best Score: {ScoreManager.Instance.Dataset[0].PlayerName} - {ScoreManager.Instance.Dataset[0].Points}";
    }
}
