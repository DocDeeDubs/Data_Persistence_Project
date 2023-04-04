using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Handlers : MonoBehaviour
{


    //************** public classes to be called by button clicks*****************

    //******* ones for the secondscene ***********

    public void handleSecondSceneDisplayUsername(TMP_Text whatTextbox)
    {
        Debug.Log("I will display the username in the textbox.");
        whatTextbox.text = GameManager.gameManagerInstance.userName;
    }

    //------------------------------------------------

    //**********ones for the menu screen*********
    public void handleSaveUsernameButtonClick(TMP_InputField whatInputBox)
    {
        string theuserInput = whatInputBox.text;
        if (theuserInput.Trim() == "")
        {
            Debug.Log("Please enter a name");
        }
        else
        {
            Debug.Log("I handle the button click and call to save username..." + theuserInput);
            GameManager.gameManagerInstance.SaveUserName(theuserInput);
        }

    }

    public void handleLoadUsernameButtonClick(TMP_InputField whereToDisplay)
    {
        Debug.Log("I will handle the button click and call to load saved username.");
        GameManager.gameManagerInstance.LoadUserName();
        whereToDisplay.text = GameManager.gameManagerInstance.userName;
    }

    public void handleLoadSceneButtonClick(string whatScene)
    {
        Debug.Log("I will handle the button click and call to load scene: " + whatScene);
        SceneManager.LoadScene(whatScene);
    }

    public void handleStartGameButtonClick(TMP_InputField whatInputBox)
    {
        
        
        //i need to ensure there is a name entered, loaded or saved.
        Debug.Log("I will handle the button click, ensure name is entered then call to load scene.");
       
        //first check for a name entered or load one that is saved if they didnt enter one in the box
        string userName = whatInputBox.text; //== "") ? (GameManager.gameManagerInstance.userName == "")? "": 
        if (userName.Trim() != "")
        {
            GameManager.gameManagerInstance.userName = userName;
            //GameManager.gameManagerInstance.SaveUserName(userName);
        } else
        {
            //GameManager.gameManagerInstance.LoadUserName();
            userName = GameManager.gameManagerInstance.userName;
        }      
        Debug.Log("I looked for a name and found:" + userName + " .");
        // if there is no username saved and they didnt enter one in the box, error and prompt for name.
        if(userName.Trim() == "")
        {
            GameManager.gameManagerInstance.ShowEnterNameText();

        } else
        {
            SceneManager.LoadScene("main");
        }
       
    }

    public void handleExitApplicationButtonClick()
    {
        Debug.Log("I will handle the button click and exit the application.");
        if (Application.isEditor)
        {
            Debug.Log("I will exit the game by exiting play mode because I'm running in editor.");
            UnityEditor.EditorApplication.isPlaying = false;

        }
        else
        {
            Debug.Log("I will exit the game by closing the application.");
            Application.Quit();
        }
    }


    public void handleSaveHighScoreButtonClick(TMP_InputField currentScore)
    {
        Debug.Log("I will save the current score in the textbox to the high score list.");
        string userName = GameManager.gameManagerInstance.userName;
        int curScore = 0;
        //if username data is empty give error
        if (userName.Trim() == "")
        {
            Debug.Log("Error- no username");
        } else
        {
            curScore = Convert.ToInt32(currentScore.text); //from text box instead for dev   GameManager.gameManagerInstance.currentScore;m
        }
        
        GameManager.gameManagerInstance.SaveHighScore(userName, curScore);
    }

    public void handleLoadHighScoreButtonClick(TMP_Text highScoreDisplay)
    {
        Debug.Log("I will load the current  high score list.");
    }


    //--------------------------------------

}
