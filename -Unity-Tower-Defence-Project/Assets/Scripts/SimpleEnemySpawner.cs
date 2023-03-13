using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemySpawner : MonoBehaviour
{
    public EnemyController enemyToSpawn;
    public Transform spawnPoint;
    public float timeBetweenSpawns;
    public int amountToSpawn = 15;

    public Castle theCastle;
    public Path thePath;

    private float spawnCounter;
    
    
    // Start is called before the first frame update
    void Start()
    {
        spawnCounter = timeBetweenSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        if (amountToSpawn > 0 && theCastle.currentHealth > 0)
        {
            spawnCounter -= Time.deltaTime;
            if (spawnCounter <= 0)
            {
                spawnCounter = timeBetweenSpawns;

                Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation).Setup(theCastle, thePath);
                amountToSpawn--;
            }
        }
    }
}
