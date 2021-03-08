using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    private int damage = 1;

    private Transform player;
    private Vector2 target;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
            DestroyEnemyBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lMaskCollision == (lMaskCollision | (1 << collision.gameObject.layer)))
        {

            if (collision.gameObject.tag.Equals("Player"))
            {
                collision.gameObject.GetComponent<Player>().TakeDamage(damage);
                if (bulletCrossing)
                    return;
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
