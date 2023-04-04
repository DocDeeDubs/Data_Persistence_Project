//phasing this out to game manager instead;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    //public static MainManager Instance;

    
    public Brick BrickPrefab; // the prefab with the brick. 
    public int LineCount = 6;
    public Rigidbody Ball;

    public TMP_Text ScoreText;
    public GameObject GameOverText;
    public GameObject CongratsText;
    public TMP_Text BestScoreText;

    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;


   /* private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }*/

  

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {100,100,200,200,500,500};
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
        //update the high score display
        int highScore = GameManager.gameManagerInstance.highScore;
        if(highScore > 0)
        {
            BestScoreText.text = "Best Score: " + GameManager.gameManagerInstance.highScoreUserName + " - " + GameManager.gameManagerInstance.highScore;
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
                SceneManager.LoadScene("menu");
            }
           
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"{GameManager.gameManagerInstance.userName}'s Score : {m_Points}";
        GameManager.gameManagerInstance.currentScore = m_Points;
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        // //see if current score is good enough for high score list
        if(GameManager.gameManagerInstance.SaveHighScore(GameManager.gameManagerInstance.userName, GameManager.gameManagerInstance.currentScore))
        {
            CongratsText.SetActive(true);
            BestScoreText.text = "Best Score: " + GameManager.gameManagerInstance.userName + " - " + GameManager.gameManagerInstance.currentScore;

        }


    }

    public void SaveUserName()
    {
        
        Debug.Log("I will save the username: ");
    }
    public void LoadUserName()
    {
        Debug.Log("I will load the username: ");
    }
}
