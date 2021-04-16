using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HordeSurvivalGame
{
    public class TeleportButton : MonoBehaviour
    {
        public Vector3 teleportDestinationPosition;

        public void Teleport()
        {
            Player.playerTransform.position = 
                new Vector3( teleportDestinationPosition.x,
                             Player.playerTransform.position.y,
                             teleportDestinationPosition.z);
        }
    }
}
