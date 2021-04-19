using HordeSurvivalGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public GameObject enemy; //The prefab of the enemy that will be spawned in. TODO: the script will take multiple enemy types (When that is set up), and spawn each of them.
    const float TIMER_START_VALUE = 3.0f; //The value the timer is set to.
    float spawnTimer; //The timer that is used between enemy spawns. TODO: This won't be a static number, and will have an element of randomness to it, while keeping balance by being slower towards the start of the game, and getting more difficult later on.
    [SerializeField]
    private Sprite spawnableMap; //The image of the playable area. Used to ensure enemies don't spawn on inaccessible or invalid tiles.
    public Player player; //Used to make sure enemies don't spawn too close to the player.
    public int noOfEnemiesAtStart = 5;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = TIMER_START_VALUE; //Sets timer to start value.
        //for (int i = 0; i < noOfEnemiesAtStart; i++)
        //{
        //    SpawnEnemy();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (!CameraScript.gameOver)
        {
            if (spawnTimer > 0.0f) //Timer isn't finished.
            {
                spawnTimer -= Time.deltaTime; //Count down.
            }
            else
            {
                spawnTimer = TIMER_START_VALUE; //Reset timer.
                SpawnEnemy();
            }
        }
        
    }

    private void SpawnEnemy() //Spawns an enemy in the scene.
    {
        Vector3 spawnPos = ReturnPosition(); //Vector that is used as the position the enemy will be spawned in at.
        if (spawnPos != Vector3.zero) //Makes sure the returned value isn't the exception value, and is a valid position.
        {
            GameObject GO = Instantiate(enemy, spawnPos, Quaternion.identity); //Enemy spawn.
            GO.GetComponent<Enemy>().playerReference = player;
        }
    }

    private Vector3 ReturnPosition() //Finds a valid Tile on the map that an enemy can be spawned on.
    {
        const float MAP_MAX_X = -202.0f; //The size of the map. Negative values as the entire map is flipped on both axies.
        const float MAP_MAX_Z = -94.0f;
        const int MINIMUM_SPAWN_DISTANCE_FROM_PLAYER = 10;
        const int MAXIMUM_SPAWN_DISTANCE_FROM_PLAYER = 50;

        Vector3 playerPosition = player.transform.position;
        bool invlidSpawnLocation = false; //Used to check if the enemies spawn point is neither too close nor too far from the player's location.

        int xPos = 0; //The X and Z positions to be put in the returned vector.
        int zPos = 0;
        int infiniteLoopPrevention = 0; //A counter to prevent the game freezing in the case of an infinite loop.
        do
        {
            invlidSpawnLocation = false; //This needs to be set every loop, and is assumed to be false, as it is only set later if it is true.

            xPos = (int)Random.Range(0.0f, MAP_MAX_X); //Chooses a random point on both the X and Z axies.
            zPos = (int)Random.Range(0.0f, MAP_MAX_Z);

            int distanceToPlayerX = Mathf.Abs(xPos) - (int)Mathf.Abs(playerPosition.x); //Number of tiles the potential enemy spawn point is away from the player's current position.
            int distanceToPlayerZ = Mathf.Abs(zPos) - (int)Mathf.Abs(playerPosition.z);
            distanceToPlayerX = Mathf.Abs(distanceToPlayerX); //Makes sure values are positive.
            distanceToPlayerZ = Mathf.Abs(distanceToPlayerZ);
            if (distanceToPlayerX < MINIMUM_SPAWN_DISTANCE_FROM_PLAYER && distanceToPlayerZ < MINIMUM_SPAWN_DISTANCE_FROM_PLAYER) invlidSpawnLocation = true; //If the enemy position is within a radius of the player, it's too close and shouldn't spawn.
            if (distanceToPlayerX > MAXIMUM_SPAWN_DISTANCE_FROM_PLAYER && distanceToPlayerZ > MAXIMUM_SPAWN_DISTANCE_FROM_PLAYER) invlidSpawnLocation = true; 

            infiniteLoopPrevention++;
            if (infiniteLoopPrevention > 100) //Surpassed loop limit. Most likely indicates there would be an infinate loop.
            {
                Debug.Log("Enemy failed to spawn. Cancelling attempt");
                return Vector3.zero; //Returns 0, which is treated as an exception and ignored.
            }
        } while ((spawnableMap.texture.GetPixel(-xPos, -zPos) == Color.black) || (invlidSpawnLocation)); //Loop while the chosen tile is invalid.

        return new Vector3(xPos, 0.2f, zPos);
    }
}
