using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Var
    [Header("Life")]
    public int Health;
    public UIManager ui;

    [Header("Movement")]
    public float MoveSpeed = 5f;
    public float DashSpeed = 7f;
    public Rigidbody2D rb;

    public Camera cam;

    Vector2 dir;
    public Vector2 Mousepos;

    private void Awake()
    {
        
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        Mousepos = cam.ScreenToWorldPoint(Input.mousePosition);

        //Health condition
        if(Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + dir * MoveSpeed * Time.deltaTime);

        //Set player mengarah kursor mouse
        Vector2 lookdir = Mousepos - rb.position;
        float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f; //Menghitung derajat
        rb.rotation = angle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Health--;
        }
    }
}
