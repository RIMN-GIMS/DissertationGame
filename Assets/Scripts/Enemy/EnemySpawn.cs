
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Start is called before the first frame update

    [System.Serializable]

    public class Wave
    {
        public GameObject enemyprefab;
        public float spawnTimer;
        public float spawnInterval;
        public int eniemiesPerWave;
        public int eniemiesSpawnCount;

    }

    public List<Wave> waves = new List<Wave>();
    public int waveNumber;
    public Transform minPos;
    public Transform maxPos;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.Instance.gameObject.activeSelf)
        {
            //spawns enemy in intervals of real time
            waves[waveNumber].spawnTimer += Time.deltaTime;
            if (waves[waveNumber].spawnTimer >= waves[waveNumber].spawnInterval)
            {
                waves[waveNumber].spawnTimer = 0;
                spawnEnemy();
            }
            // if wave is complete moves on to next wave
            if (waves[waveNumber].eniemiesSpawnCount >= waves[waveNumber].eniemiesPerWave)
            {
                waves[waveNumber].eniemiesSpawnCount = 0;
                // increases speed waves spawn at with limit at .3 seconds
                if (waves[waveNumber].spawnInterval > 0.3f)
                {
                    waves[waveNumber].spawnInterval *= 0.9f;
                }

                waveNumber++;
            }
            if (waveNumber >= waves.Count)
            {
                waveNumber = 0;
            }
        }

    }
    void spawnEnemy()
    {
        Instantiate(waves[waveNumber].enemyprefab, RandomSpawnPoint(), transform.rotation);
        waves[waveNumber].eniemiesSpawnCount++;
    }

    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint;
        if (Random.Range(0f, 1f) > 0.5)
        {
            spawnPoint.x = Random.Range(minPos.position.x, maxPos.position.x);
            if (Random.Range(0f, 1f) > 0.5)
            {
                spawnPoint.y = minPos.position.y;
            }
            else
            {
                spawnPoint.y = maxPos.position.y;
            }
        }
        else
        {
            spawnPoint.y = Random.Range(minPos.position.y, maxPos.position.y);
            if (Random.Range(0f, 1f) > 0.5)
            {
                spawnPoint.x = minPos.position.x;
            }
            else
            {
                spawnPoint.x = maxPos.position.x;
            }
        }
        return spawnPoint;
    }
}
