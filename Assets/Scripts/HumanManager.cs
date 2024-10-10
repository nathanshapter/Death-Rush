using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : MonoBehaviour
{
    [SerializeField] Human humanPrefab;

    [SerializeField] Vector2 spawnAreaMin, spawnAreaMax;

    Human[] humans;

    [SerializeField] int humansToSpawn;


    private void Start()
    {
        SpawnHumans();
    }
    void SpawnHumans() 
    {
        for (int i = 0; i < humansToSpawn; i++) 
        {

            // Generate random position within the spawn area
            Vector2 randomPosition = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x), // Random X within bounds
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)  // Random Y within bounds
            );




            Instantiate(humanPrefab, randomPosition, Quaternion.identity);
        }
    }
}
