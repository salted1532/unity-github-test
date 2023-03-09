using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wavespawner : MonoBehaviour
{
    public List<Monster> enemies = new List<Monster>();
    public int currWave;
    public int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    public List<Vector3> spawnpoint = new List<Vector3>();
    public Transform spawnLocation;
    public Transform spawnLocation1;
    public Transform spawnLocation2;
    public Transform spawnLocation3;
    public Transform spawnLocation4;
    public Transform spawnLocation5;
    public int waveDuration;
    public float waveTimer;
    public float spawnInterval;
    public float spawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        GenerateWave();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawnTimer <= 0)
        {
            int randlocation = Random.Range(0, 5);
            //적 스폰
            if (enemiesToSpawn.Count > 0)
            {
                Instantiate(enemiesToSpawn[0], spawnpoint[randlocation], Quaternion.identity);
                /*switch(randlocation) //리스트 첫번쨰 랜덤 위치로 적 스폰
                {
                    case 0:
                        Instantiate(enemiesToSpawn[0], spawnLocation.position, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(enemiesToSpawn[0], spawnLocation1.position, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(enemiesToSpawn[0], spawnLocation2.position, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(enemiesToSpawn[0], spawnLocation3.position, Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(enemiesToSpawn[0], spawnLocation4.position, Quaternion.identity);
                        break;
                    case 5:
                        Instantiate(enemiesToSpawn[0], spawnLocation5.position, Quaternion.identity);
                        break;

                }*/
                enemiesToSpawn.RemoveAt(0);// 그리고 지우기
                spawnTimer = spawnInterval;
            }
            else
            {
                waveTimer = 0;
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }
    }
    public void GenerateWave()
    {
        waveValue = currWave * 10;
        GenerateEnemies();

        spawnInterval = waveDuration / enemiesToSpawn.Count;
        waveTimer = waveDuration;
    }
    public void GenerateEnemies()
    {
        List<GameObject> generateEnemies = new List<GameObject>();
        while (waveValue > 0)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;

            if (waveValue - randEnemyCost >= 0)
            {
                generateEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generateEnemies;
    }
}

[System.Serializable]
public class Monster
{
    public GameObject enemyPrefab;
    public int cost;
}

