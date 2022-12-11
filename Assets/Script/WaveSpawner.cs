using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<Musuh> enemies = new List<Musuh>();
    public int currentwave;
    public int wavevalue;

    public List<GameObject> enemiestoSpawn = new List<GameObject>();

    public Transform[] loc;
    public int SpawnIndex;

    public int WaveDuration;
    private float WaveTimer;
    private float SpawnInterval;
    private float waverate;
    private float SpawnTimer;
    private int enemyperlukeluar;

    public List<GameObject> spawnedEnemies = new List<GameObject>();

    public bool CanSpawn;

    public UIManager UI;

    // Start is called before the first frame update
    void Start()
    {
        //GenerateWave();
        StartCoroutine(transisitrue(3f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UI.GetComponent<UIManager>().waveupdate(currentwave);
        if (SpawnTimer <= 0)
        {
            //Spawn Enemy
            if(enemiestoSpawn.Count > 0)
            {
                GameObject enemy 
                    = (GameObject)Instantiate(enemiestoSpawn[0], loc[SpawnIndex].position, Quaternion.identity);
                enemiestoSpawn.RemoveAt(0); //remove from list
                spawnedEnemies.Add(enemy); //add it to spawned enemies list
                SpawnTimer = SpawnInterval;

                if(SpawnIndex + 1 <= loc.Length - 1)
                {
                    SpawnIndex++;
                }
                else
                {
                    SpawnIndex = 0;
                }
            }
            else
            {
                WaveTimer = 0;
            }
        }
        else
        {
            SpawnTimer -= Time.fixedDeltaTime;
            WaveTimer -= Time.fixedDeltaTime;
        }

        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            currentwave = 0;
        }

        //Ketika wave timer sudah 0 maka
        if(WaveTimer <= 0 && spawnedEnemies.Count <= 0 && CanSpawn == true 
            && GameObject.FindGameObjectWithTag("Player") != null)
        {
            currentwave++;
            
            StartCoroutine(transisitrue(5f));
            GenerateWave();
        }
        
    }

    IEnumerator transisitrue(float timer)
    {
        yield return new WaitForSeconds(timer);
        CanSpawn = true;
    }

    public void GenerateWave()
    {
        enemyperlukeluar = currentwave * 5; //Rumus enemy bertambah apabila menambah wave
        wavevalue = enemyperlukeluar; //10 enemies

        if(currentwave <= 5)
        {
            WaveDuration = currentwave * 10;
        }
        else if(currentwave >= 6)
        {
            WaveDuration = currentwave * 5;
        }

        GenerateEnemies();
        SpawnInterval = WaveDuration/enemiestoSpawn.Count; // gives a fixed time between each enemies

        WaveTimer = WaveDuration;

        //Thing to Note
        /* Ketika wave berakhir, maka count untuk enemiestoSpawn akan meningkat sesuai current wave 
         * Misal wave 1, berarti wave 1 x 10 = 10 enemy untuk dispawn
         * Misal wave 2, berarti wave 2 x 10 = 20 enemy untuk dispawn dan begitu seterusnya
         * Karena interval hitungannya Durasi / enemiestoSpawn, jadinya semakin besar selisih durasi
         * semakin kecil intervalnya
         * apabila durasi < enemiestoSpawn, maka ini akan menyebabkan semua enemy spawn dalam waktu yg sama
         * Misal wave 3, berarti Wave 3 x 10 = 30, sementara Interval berarti 20/30 = 2/3
         * Solusi sementara kemungkinan menambah durasi sesuai peningkatan wave
         * Apabila wave berakhir maka durasinya akan bertambah
         */
    }

    public void GenerateEnemies()
    {
        List<GameObject> generateEnemy = new List<GameObject>();
        while (wavevalue > 0 || generateEnemy.Count < 50)
        {
            int randEnemy_ID = Random.Range(0, enemies.Count);
            int randEnemycost = enemies[randEnemy_ID].cost;

            if(wavevalue-randEnemycost >= 0)
            {
                generateEnemy.Add(enemies[randEnemy_ID].enemyPrefab);
                wavevalue -= randEnemycost;
            } else if (wavevalue <= 0)
            {
                break;
            }
        }

        enemiestoSpawn.Clear();
        enemiestoSpawn = generateEnemy;
    }
}

[System.Serializable]
public class Musuh
{
    public GameObject enemyPrefab;
    public int cost;
}
