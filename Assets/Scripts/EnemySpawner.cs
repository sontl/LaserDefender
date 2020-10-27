using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] bool looping = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        if (looping)
        {
            do
            {
                yield return StartCoroutine(SpawnAllWaves());
            }
            while (looping);
        } else
        {
            StartCoroutine(SpawnAllWaves());
        }
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int i = 0; i < waveConfigs.Count; i++)
        {
            var currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
        
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        int enemyCount = 0;
        while (enemyCount <= waveConfig.GetNumberOfEnemies())
        {
            var newEnemy = 
                Instantiate(
                    waveConfig.GetEnemyPrefab(),
                    waveConfig.GetWayPoints()[0].transform.position,
                    Quaternion.identity
                    );
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            enemyCount++;
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawn());
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
