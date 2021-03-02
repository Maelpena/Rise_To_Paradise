using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    public float speed = 7.0f;
    public float dir = 1;
    public Rigidbody2D rb;
    public Animator anim;
    public CharacterData ownerCharData;
    public int sizeBonus;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        rb.MovePosition(new Vector2(speed * dir * Time.deltaTime + transform.position.x , transform.position.y));
        transform.localScale = new Vector3(1 + sizeBonus, 1 + sizeBonus, 1 + sizeBonus);
    }
    public void SetWay(bool isLeft)
    {
        if (!isLeft)
        {
                GetComponent<SpriteRenderer>().flipX = true;
                dir = -1;
        }
        transform.position = new Vector2(transform.position.x + dir/2, transform.position.y);

    }
    public void Hit(Collider2D collision)
    {
        dir = 0;
        if (collision.gameObject.tag.Equals("Enemies"))
        {
            ownerCharData.score += collision.gameObject.GetComponent<CharacterData>().score;
            anim.Play("Hitting");
        } else if (!collision.gameObject.tag.Equals("Projectile"))
        {
            anim.Play("Splashing");
        }
    }
}
