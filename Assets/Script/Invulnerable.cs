using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invulnerable : MonoBehaviour
{
    bool invincible = false;
    public SpriteRenderer ren;
    Color c;

    public Player pl;
    private void Awake()
    {
        pl = GetComponent<Player>();
        ren = GameObject.Find("/Player/SpaceShip1").GetComponent<SpriteRenderer>();
        c = ren.material.color;
    }

    private void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && pl.Health > 0)
        {
            StartCoroutine("Invul");
        }
    }

    IEnumerator Invul()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        invincible = true;
        c.a = 0.5f;
        ren.material.color = c;
        yield return new WaitForSeconds(2f);
        Physics2D.IgnoreLayerCollision(6, 7, false);
        c.a = 1f;
        invincible = false;
        ren.material.color = c;
    }
}
