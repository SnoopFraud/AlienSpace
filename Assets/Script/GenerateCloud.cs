using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCloud : MonoBehaviour
{
    [SerializeField]
    GameObject[] objek;

    [SerializeField]
    float spawnInterval;

    [SerializeField]
    GameObject endPoint;
    Vector3 startPos;

    private void Start()
    {
        startPos = this.transform.position;
        premade();
        Invoke("trySpawn", spawnInterval);
    }

    void SpawnObj(Vector3 startPos)
    {
        int RandomIndex = Random.Range(0, objek.Length);
        GameObject obj = Instantiate(objek[RandomIndex]);

        float startY = Random.Range(startPos.y - 4f, startPos.y + 4f); ;
        obj.transform.position = new Vector3(startPos.x, startY, startPos.z);

        obj.GetComponent<Asteroid>().floating(Random.Range(0.5f, 1.5f), endPoint.transform.position.x);
    }

    void trySpawn()
    {
        //Check kriteria
        SpawnObj(startPos);
        Invoke("trySpawn", spawnInterval);
    }

    void premade()
    {
        for (int i = 0; i < 10 ; i++)
        {
            Vector3 spawnpos = startPos + Vector3.right * (i * 2);
            SpawnObj(spawnpos);
        }
    }

}
