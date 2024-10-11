using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : MonoBehaviour
{
    [SerializeField] Human humanPrefab;
    public List<Human> humanList;

    [SerializeField] Vector2 spawnAreaMin, spawnAreaMax;
    [SerializeField] Vector2 forbiddenAreaMin, forbiddenAreaMax;

    [SerializeField] int humansToSpawn;
    [SerializeField] public int humansHit = 0;

    public int spawnWaveAmount = 5;
    int totalHumansToSpawnThisLevel;


    [SerializeField] float spawnDelay = 0.1f;
    private void Start()
    {
        totalHumansToSpawnThisLevel = spawnWaveAmount * humansToSpawn;

        // Instantiate all the humans at the start, but keep them inactive.
        for (int i = 0; i < totalHumansToSpawnThisLevel; i++)
        {
            Human newHuman = Instantiate(humanPrefab, GetValidSpawnPosition(), Quaternion.identity, this.gameObject.transform);
            newHuman.gameObject.SetActive(false); // Keep them inactive initially
            humanList.Add(newHuman); // Add them to the list
        }

        // Start spawning humans in waves
        StartCoroutine(SpawnHumans());
    }

    IEnumerator SpawnHumans()
    {
        // Loop over the number of waves
        for (int wave = 0; wave < spawnWaveAmount; wave++)
        {
            print("Wave number " + (wave + 1));

            // Calculate the starting and ending index for the current wave
            int startIndex = wave * humansToSpawn;
            int endIndex = Mathf.Min(startIndex + humansToSpawn, humanList.Count); // Ensure we don't exceed the list size

            // Spawn the required number of humans for this wave
            for (int i = startIndex; i < endIndex; i++)
            {
                // Set a new valid spawn position for each human
                humanList[i].transform.position = GetValidSpawnPosition();

                // Activate the human
                humanList[i].gameObject.SetActive(true);

                yield return new WaitForSeconds(spawnDelay); // Delay between spawns
            }

            // Wait before starting the next wave
            yield return new WaitForSeconds(2.0f);
        }
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
