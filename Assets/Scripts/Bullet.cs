using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask lMaskCollision;
    private int damage = 1;
    public float speed = 7.0f;
    public float dir = 1;
    public Rigidbody2D rb;
    public Animator anim;
    public int sizeBonus;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(new Vector2(speed * dir * Time.deltaTime + transform.position.x, transform.position.y));
        transform.localScale = new Vector3(1 + sizeBonus, 1 + sizeBonus, 1 + sizeBonus);
    }
    public void SetWay(bool isLeft)
    {
        if (!isLeft)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            dir = -1;
        }
        transform.position = new Vector2(transform.position.x + dir / 2, transform.position.y);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lMaskCollision == (lMaskCollision | (1 << collision.gameObject.layer)))
        {
            dir = 0;
            if (collision.gameObject.tag.Equals("Enemies"))
            {
                anim.Play("Hitting");
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
            else 
            {
               anim.Play("Splashing");
            }
       }
    }
}
