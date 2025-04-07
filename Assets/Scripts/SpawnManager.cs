using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;// array to hold different obstacles
    private Vector3 spawnPos = new Vector3(25, 1.5f, 0);
    private float startDelay = 2;
    private float repeatRate = 2;
    private PlayerController playerControllerScript; 

    // Start is called before the first frame update
    void Start()
    {
       InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
       playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle ()
    {   // stops the spawn manager when the game is over
       if (!playerControllerScript.gameOver)
        {
            int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            GameObject selectedObstacle = obstaclePrefabs[obstacleIndex];
            
            // Randomly decide whether to spawn a pile (group of obstacles)
            if (Random.value < 0.3f) // 30% chance to spawn a pile
            {
                SpawnPile();
            }
            else
            {
                Instantiate(selectedObstacle, spawnPos, selectedObstacle.transform.rotation);
            }
        }

    }

    void SpawnPile()
    {
        int numObstacles = Random.Range(2, 5); // Spawn 2 to 4 obstacles in a pile
        float spacing = 2.0f; // Space between obstacles in the pile
        
        for (int i = 0; i < numObstacles; i++)
        {
            int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            GameObject obstacle = obstaclePrefabs[obstacleIndex];
            Vector3 pileSpawnPos = spawnPos + new Vector3(i * spacing, 0, 0);
            Instantiate(obstacle, pileSpawnPos, obstacle.transform.rotation);
        }
    }


}
