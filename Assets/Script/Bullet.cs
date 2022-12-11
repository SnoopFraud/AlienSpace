using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Update()
    {
        StartCoroutine("LimitBullet");
    }

    IEnumerator LimitBullet()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Bound"))
        {
            Destroy(this.gameObject);
        }
    }
}
