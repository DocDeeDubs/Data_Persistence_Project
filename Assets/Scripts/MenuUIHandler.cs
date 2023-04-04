//not using this file from the default project
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]

public class MenuUIHandler : MonoBehaviour
{
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


}
