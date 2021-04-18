using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HordeSurvivalGame
{
    public class CameraScript : MonoBehaviour
    {
        public Camera mainCamera;

        public Canvas gameCanvas;
        public Canvas gameOverCanvas;

        public static bool gameOver = false;

        void Awake()
        {
            PlayerResources.ResetResources();
            gameOver = false;
            mainCamera.enabled = true;
            gameCanvas.enabled = true;
            gameOverCanvas.enabled = false;
        }

        private void Update()
        {
            if (PlayerResources.GetPlayerHealth() < 1)
            {
                mainCamera.enabled = true;
                mainCamera.transform.position = new Vector3(0.0f, 300.0f, 0.0f);
                mainCamera.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                gameCanvas.enabled = false;
                gameOverCanvas.enabled = true;
                gameOver = true;
            }
        }

    }
}

