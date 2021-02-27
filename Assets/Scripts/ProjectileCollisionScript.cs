using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionScript : MonoBehaviour
{
    public LayerMask lMaskCollision;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lMaskCollision == (lMaskCollision | (1 << collision.gameObject.layer)))
        {
            if (!collision.gameObject.tag.Equals("Projectile"))
            {
                if (!((collision.gameObject.tag.Equals("Floor") && collision.gameObject.GetComponent<PlatformEffector2D>() != null) || (collision.gameObject.tag.Equals("Player"))))
                {
                    if (collision.gameObject.tag.Equals("Enemies") && !collision.gameObject.tag.Equals("Shield"))
                    {
                        collision.gameObject.GetComponent<CharacterData>().takeDamage(1);
                    }
                    GetComponent<MoveProjectile>().Hit(collision);
                }
            }
        }
            
    }
}
