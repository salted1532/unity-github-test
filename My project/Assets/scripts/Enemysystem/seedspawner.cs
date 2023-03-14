using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seedspawner : MonoBehaviour
{
    public List<Mobs> enemies = new List<Mobs>();
    public List<GameObject> spawnpoint = new List<GameObject>();
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    //Mobs aaa = new Mobs(0, 0);
    public List<List<Mobs>> enemyinfo = new List<List<Mobs>>();

    public int waverate;
    public int enemyrate;
    public float wavedelay;

    //int[,] seed = new int[,] { { 2, 4, 9 } , { 3, 7 }, { 5 } };
    // Start is called before the first frame update
    void Start()
    {
        adding();
        //aaa = new Mobs(1, 6);
        /*enemyinfo.Add(new List<Mobs> { new Mobs(1, 2), new Mobs(1, 4) ,new Mobs(1, 9) });
        enemyinfo.Add(new List<Mobs> { new Mobs(2, 3), new Mobs(2, 7) });
        enemyinfo.Add(new List<Mobs> { new Mobs(3, 5) });*/
    }

    // Update is called once per frame
    void Update()
    {
            wavedelay -= Time.deltaTime;
            if (enemyrate == 0)
            {
                wavedelay = 15;
                seeding();
            }
            else if (wavedelay <= 0)
            {
                wavedelay = 15;
                seeding();
            }
    }
    public void seeding()
    {
        waverate += 1;
        enemyinfo.Clear();
        int randseed = Random.Range(0, 2);
        switch (randseed) //리스트 첫번쨰 랜덤 위치로 적 스폰
        {
            case 0:
                enemyinfo.Add(new List<Mobs> { new Mobs(1, 2), new Mobs(1, 4), new Mobs(1, 9) });
                enemyinfo.Add(new List<Mobs> { new Mobs(2, 3), new Mobs(2, 7) });
                enemyinfo.Add(new List<Mobs> { new Mobs(3, 5) });
                break;
            case 1:
                enemyinfo.Add(new List<Mobs> { new Mobs(1, 3), new Mobs(1, 5) });
                enemyinfo.Add(new List<Mobs> { new Mobs(2, 2), new Mobs(2, 7)});
                enemyinfo.Add(new List<Mobs> { new Mobs(3, 1) });
                break;
            case 2:
                enemyinfo.Add(new List<Mobs> { new Mobs(1, 6), new Mobs(1, 8) });
                enemyinfo.Add(new List<Mobs> { new Mobs(2, 1), new Mobs(2, 3) });
                enemyinfo.Add(new List<Mobs> { new Mobs(3, 4), new Mobs(3, 7), new Mobs(3, 9) });
                break;

        }
        foreach (var m in enemyinfo)
        {
            enemyrate += m.Count;
        }
        spawning();
    }
    public void adding()
    {
        for (int i = enemies.Count - 1; i > -1; i--)
        {
            enemiesToSpawn.Add(enemies[i].enemyPrefab);
        }
    }
    public void spawning()
    {
        /*for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (enemyinfo[i, j] == 10)
                {
                    break;
                }
                Instantiate(enemiesToSpawn[enemyinfo[i, j]], spawnpoint[seed[i,j]-1], Quaternion.identity);
            }
        }*/
        foreach (var m in enemyinfo)
        {
            foreach (Mobs n in m)
                Instantiate(enemiesToSpawn[n.whatMobs-1], spawnpoint[n.spawnPos-1].transform.position, Quaternion.identity);
        }
        //Instantiate(enemiesToSpawn[enemyinfo[0].whatMobs], spawnpoint[enemyinfo[0].spawnPos].transform.position, Quaternion.identity);
    }
}

[System.Serializable]
public class Mobs
{
    public GameObject enemyPrefab;
    [HideInInspector]
    public int whatMobs;
    [HideInInspector]
    public int spawnPos;

    public Mobs (int w, int p)
    {
        whatMobs = w;
        spawnPos = p;
    }
}
