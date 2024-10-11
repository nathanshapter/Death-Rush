using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HumanManager : MonoBehaviour
{
    [SerializeField] Human humanPrefab;
    public List<Human> humanList;

    [SerializeField] Vector2 spawnAreaMin, spawnAreaMax;
    [SerializeField] Vector2 forbiddenAreaMin, forbiddenAreaMax;

    Human[] humans;

    [SerializeField] int humansToSpawn;

    [SerializeField] public int humansHit = 0;

 

    public int spawnWaveAmount = 5;
    int totalHumansToSpawnThisLevel;

    private void Start()
    {
       StartCoroutine( SpawnHumans(spawnWaveAmount));

       totalHumansToSpawnThisLevel = spawnWaveAmount * humansToSpawn;

       
    }


   

    IEnumerator SpawnHumans(int o)
    {
        for(int a = 0; a < o; a++)
        {
            print("wave number " + (a + 1) );

            for (int i = 0; i < humansToSpawn; i++)
            {
                yield return new WaitForSeconds(0.5f);
                SpawnInPlace();
            }
        }


       
   
    }

    private void SpawnInPlace()
    {
        Vector2 randomPosition = GetValidSpawnPosition();

        // Spawn a human at the valid random position
      Human newHuman =  Instantiate(humanPrefab, randomPosition, Quaternion.identity, this.gameObject.transform);
        humanList.Add(newHuman);
    }

    IEnumerator SpawnHumansSlowly()
    {
        yield return new WaitForSeconds(1);
        SpawnInPlace();
    }

    Vector2 GetValidSpawnPosition()
    {
        Vector2 randomPosition;

        do
        {
            // Generate a random position within the outer spawn area
            randomPosition = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x), // Random X within bounds
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)  // Random Y within bounds
            );

            // Keep generating a position until it falls outside the forbidden area
        } while (IsInsideForbiddenArea(randomPosition));

        return randomPosition;
    }

    bool IsInsideForbiddenArea(Vector2 position)
    {
        // Check if the position is inside the forbidden area
        return position.x > forbiddenAreaMin.x && position.x < forbiddenAreaMax.x &&
               position.y > forbiddenAreaMin.y && position.y < forbiddenAreaMax.y;
    }
}

