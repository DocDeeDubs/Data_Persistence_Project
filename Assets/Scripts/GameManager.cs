using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;
    public string userName;
    public string highScoreUserName;
    public int currentScore;
    public int highScore;
    public List<KeyValuePair<string, int>> highScoreList = new List<KeyValuePair<string, int>>();
    
    //public TMP_Text MenuBestScoreText;
    //public TMP_InputField menuUserNameInput;
    public GameObject EnterNameText;
    public GameObject devZonePanel;
    private bool devMode = false;

    [System.Serializable]
    class SaveData
    {
        public string userName;
        //public int currentScore;
        public int highScore;
        //public string highScoreUserName;
        //public List<KeyValuePair<string, int>> highScoreList = new List<KeyValuePair<string,int>>();
    }

    private void Awake()
    {
        if (gameManagerInstance != null)
        {
            Destroy(gameObject);
            return;
        }
        gameManagerInstance = this;
        DontDestroyOnLoad(gameObject);
        if(devMode) devZonePanel.SetActive(true);

        Scene curScene = SceneManager.GetActiveScene();
        string sceneName = curScene.name;
        Debug.Log("Im in the scene.. " + sceneName);
        //was doing stuff here on scene loaded, but only ran once due to awake only happening once.

    }

    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);
        if (scene.name == "menu")
        {
            TMP_Text menuBestScoreText = GameObject.Find("Best Score Text").GetComponent<TMP_Text>();
            LoadHighScore();
            //update the high score display
            int highScore = GameManager.gameManagerInstance.highScore;
            if (highScore > 0)
            {
                Debug.Log("I hve a high score to display." + highScore + " name " + highScoreUserName);
                menuBestScoreText.text = "Best Score: \n" + GameManager.gameManagerInstance.highScoreUserName + " - " + GameManager.gameManagerInstance.highScore;
            }

            //TMP_InputField menuUserNameInput = GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();

            //GameManager.gameManagerInstance.LoadUserName(); -- not saving the username, but the high score and name
            //if (GameManager.gameManagerInstance.userName.Trim() != "")
           // {
               // menuUserNameInput.text = GameManager.gameManagerInstance.userName;
           // }

        }
    }

  

    // ***********private methods to by called internally by this gamemanager *****************

    public void SaveUserName(string savedUserName) 
    {
        //SaveData data = new SaveData();  = data.userName
        userName = savedUserName; //this sets the "save data" and the gamemanager public var
         // this saves the username to persistant storage
        //string json = JsonUtility.ToJson(data);

        //File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        //Debug.Log("I have saved the username: " + userName + " to the file " + Application.persistentDataPath + "/savefile.json");

    }

    public void LoadUserName() // which this shouldnt work anymore, saving username and high score, and loading usrname/hgih score. not loading a username anymore
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            userName = data.userName;
            
            Debug.Log("I have found the saved the username: " + userName + " and loaded it from the file " + path);

        } else
        {
            Debug.Log("I cant find a save file in the path " + path);
        }
    }



    public bool SaveHighScore(string userName, int curScore)
    {
        bool didTheygetNewHighScore = false;
        Debug.Log("I'll check to see if the score is greater than the high score: " + userName + " cur score: " + curScore.ToString() + " high score: " + highScore);
        if (curScore > highScore)
        {
            Debug.Log("We have a new high score! ");
            didTheygetNewHighScore = true;
            highScore = curScore;
            highScoreUserName = userName;
            SaveData data = new SaveData();
            data.userName = userName;
            data.highScore = highScore;
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
            Debug.Log("I have saved the username: " + userName + " and high score " + highScore + " to the file " + Application.persistentDataPath + "/savefile.json");
        }

        return didTheygetNewHighScore;
 
    }

    public void LoadHighScore()
    {
        Debug.Log("I'll load the username and  high score from  persistant storage");
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScoreUserName = data.userName;
            highScore = data.highScore;
            Debug.Log("I have found the saved the username: " + highScoreUserName + " and high score " + highScore + " and loaded them from the file " + path);

        }
        else
        {
            Debug.Log("I cant find a save file in the path " + path);
        }
    }

    public void ShowEnterNameText()
    {
        EnterNameText.SetActive(true);
    }

    //--------------------------------------------------------------------

}
