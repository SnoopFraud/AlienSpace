using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Dash : MonoBehaviour
{
    public float dashSpeed = 200000;

    public Player pl;

    public Rigidbody2D rb;

    bool canDash = true;
    int dashCooldown = 80;

    private void Start()
    {
        pl = GetComponent<Player>();
    }

    void FixedUpdate()
    {
        if (dashCooldown == 0)
        {
            canDash = true;
        }
        else
        {
            dashCooldown--;
        }

        rb.velocity = Vector2.zero;

        if (canDash && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(
            (pl.Mousepos / 4) * dashSpeed * Time.fixedDeltaTime
            );
            canDash = false;
            dashCooldown = 80;
        }
    }
}
