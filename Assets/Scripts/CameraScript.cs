using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HordeSurvivalGame
{
    public class CameraScript : MonoBehaviour
    {
        public Camera mainCamera;
        public Camera endScreenCamera;

        public Canvas gameCanvas;
        public Canvas gameOverCanvas;

        public static bool gameOver = false;

        void Awake()
        {
            PlayerResources.ResetResources();
            gameOver = false;
            mainCamera.enabled = true;
            endScreenCamera.enabled = false;
            gameCanvas.enabled = true;
            gameOverCanvas.enabled = false;
        }

        private void Update()
        {
            if (PlayerResources.GetPlayerHealth() < 1)
            {
                mainCamera.enabled = false;
                endScreenCamera.enabled = true;
                gameCanvas.enabled = false;
                gameOverCanvas.enabled = true;
                gameOver = true;
            }
        }

    }
}

