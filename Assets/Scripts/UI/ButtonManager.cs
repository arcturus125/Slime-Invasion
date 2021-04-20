using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject controlsPanel;

    private void Start()
    {
        if(controlsPanel)
        controlsPanel.SetActive(false);
    }

    public void ChangeSceneButtonPress(string scene) //Currently used for the start button, but options could use this as well if it ends up changing to another scene.
    {
        try
        {
            SceneManager.LoadScene(scene); //Attempts to load the scene from the string in the inspector.
        }
        catch
        {
            Debug.Log("Couldn't switch to scene " + scene); //Failed to switch, outputs a log for debugging.
        }
    }

    public void QuitApplicationButtonPress() //Quits the game.
    {
        Application.Quit();
    }

    public void ToggleControls()
    {
        controlsPanel.SetActive(!controlsPanel.activeInHierarchy);
    }
}
