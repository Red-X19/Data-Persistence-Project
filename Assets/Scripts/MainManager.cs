using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    public int highScore;
    public string championName;
    private string playerName;

    public Text ScoreText;
    public Text HighScore;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    private InformationHandler informationHandler;



    // Start is called before the first frame update
    void Start()
    {
        informationHandler = GameObject.Find("InformationHandler").GetComponent<InformationHandler>();
        playerName = informationHandler.playerName;

        informationHandler.LoadHighScore();

        highScore = informationHandler.highScore;
        championName = informationHandler.championName;

        ScoreText.text = $"Name:{playerName} Score :{m_Points}";
        HighScore.text = $"High Score:" + highScore + " by "+championName;
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Name:{playerName} Score :{m_Points}";
        if(m_Points>highScore)
        {
            HighScore.text = $"High Score:" + m_Points+ " by " + playerName;

        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        if (m_Points > highScore)
        {
            informationHandler.SaveHighScore(m_Points,playerName);

        }
        GameOverText.SetActive(true);
    }
}
