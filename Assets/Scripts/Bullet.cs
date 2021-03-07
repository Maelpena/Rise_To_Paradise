using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask lMaskCollision;
    private int damage = 1;
    public float speed = 7.0f;
    //public float dir = 1;
    public Rigidbody2D rb;
    public Animator anim;
    public int sizeBonus;
    public bool bulletCrossing;
    public Vector2 dir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(new Vector2(speed * dir.x * Time.deltaTime + transform.position.x, speed * dir.y * Time.deltaTime + transform.position.y));
        transform.localScale = new Vector3(1 + sizeBonus, 1 + sizeBonus, 1 + sizeBonus);
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
            transform.rotation = Quaternion.Euler(0, 00, 90 );

        }
        transform.position = new Vector2(transform.position.x + dir.x / 2, transform.position.y);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (lMaskCollision == (lMaskCollision | (1 << collision.gameObject.layer)))
        {
            
            if (collision.gameObject.tag.Equals("Enemies") )
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
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
}
