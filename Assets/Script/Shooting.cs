using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform point;
    public GameObject BulletPrefab;

    public float Bulletforce = 20f;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject Bullet = Instantiate(BulletPrefab, point.position, point.rotation);
        Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(point.up * Bulletforce, ForceMode2D.Impulse);
    }
}
