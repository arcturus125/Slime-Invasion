using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void ChangeSceneButtonPress(string scene)
    {
        try
        {
            SceneManager.LoadScene(scene);
        }
        catch
        {
            Debug.Log("Couldn't switch to scene " + scene);
        }
    }
}
