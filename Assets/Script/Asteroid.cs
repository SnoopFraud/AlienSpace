using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 1f;
    private float endpoint;
    public float RotationSpeed = 10f;

    private void Start()
    {

    }

    public void floating(float _speed, float endpointX)
    {
        speed = _speed;
        endpoint = endpointX;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime));

        if (transform.position.x > endpoint)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
        }
    }
}
