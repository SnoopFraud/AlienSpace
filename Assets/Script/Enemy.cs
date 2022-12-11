using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Var
    public UIManager ui;
    public int score;

    public Transform destination;
    public float speed = 5f;
    public float minDistance = 1f;
    public float range;

    public Rigidbody2D rb;
    public Vector2 LookAt;

    private WaveSpawner wave;

    public void Awake()
    {
        destination = GameObject.FindGameObjectWithTag("Player").transform;
        wave = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveSpawner>();
        ui = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        if (wave.currentwave <= 10)
        {
            speed = 3;
        }
        else
        {
            speed = 5;
        }
    }

    private void Update()
    {
        range = Vector2.Distance(transform.position, destination.position);
        LookAt = destination.position;

        if(range > minDistance)
        {
            transform.position 
                = Vector2.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
        }


        //Set enemy mengarah player
        Vector2 lookdir = LookAt - rb.position;
        float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f; //Menghitung derajat
        rb.rotation = angle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
            wave.spawnedEnemies.RemoveAt(0);
            ui.GetComponent<UIManager>().txtupdate(score);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            wave.spawnedEnemies.RemoveAt(0);
        }
    }
}
