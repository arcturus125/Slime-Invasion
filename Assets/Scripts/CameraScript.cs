using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HordeSurvivalGame
{
    public class CameraScript : MonoBehaviour
    {
        public Camera mainCamera; //The camera that is used.

        public Canvas gameCanvas; //Canvas for while the game is playing.
        public Canvas gameOverCanvas; //Canvas for when the game is over.
        public Text scoreText; 

        public static bool gameOver = false; //Used to prevent other classes from doing things when the game is over, i.e. spawning enemies.
        private float timeSurvived = 0.0f;

        void Awake()
        {
            PlayerResources.ResetResources(); //Resets all static variables.
            timeSurvived = 0.0f;
            gameOver = false;
            gameCanvas.enabled = true;
            gameOverCanvas.enabled = false;
        }

        private void Update()
        {
            if (PlayerResources.GetPlayerHealth() < 1)
            {
                mainCamera.transform.position = new Vector3(0.0f, 300.0f, 0.0f); //Sets the position and rotation of the camera to be away from the map.
                mainCamera.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                gameCanvas.enabled = false; //Changes canvas being used.
                gameOverCanvas.enabled = true;
                string minutes = ((int)(timeSurvived / 60)).ToString();
                string seconds = ((int)(timeSurvived % 60)).ToString();
                scoreText.text = "You lasted " + minutes + " minutes, " + seconds + " seconds";
                gameOver = true;
            }
            else timeSurvived += Time.deltaTime; //Game is not over, so update the timeSurvived timer.
        }

    }
}

