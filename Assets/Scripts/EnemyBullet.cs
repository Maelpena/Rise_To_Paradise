using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    private int damage = 1;
    private Transform player;
    private Vector2 target;
    public float speed = 7.0f;
    //public Vector2 dir;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        SetWay(dir);
    }

    void Update()
    {
        rb.MovePosition(new Vector2(speed * dir.x * Time.deltaTime + transform.position.x, speed * dir.y * Time.deltaTime + transform.position.y));
    }

    public void SetWay(Vector2 direction)
    {
        dir = direction;
        if (dir.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;

        }
        if (dir.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 00, 90);

        }
        transform.position = new Vector2(transform.position.x + dir.x / 2, transform.position.y);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lMaskCollision == (lMaskCollision | (1 << collision.gameObject.layer)))
        {

            if (collision.gameObject.tag.Equals("Player"))
            {
                collision.gameObject.GetComponent<Player>().TakeDamage(damage);
                anim.Play("Hitting");
            }
            else
            {
                anim.Play("Splashing");
            }
            dir.x = 0;
            dir.y = 0;
        }
    }

    void DestroyEnemyBullet()
    {
        Destroy(gameObject);
    }
}
