using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : Weapon
{
    [SerializeField] public GameObject BalleType;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Shoot()
    {
        GameObject Balle;
        Balle = Instantiate(BalleType, null);
        Balle.transform.position = gameObject.transform.position;

        if (gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().dir < 0)
        {
            if (!gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().isWallSlide)
            {
                Balle.GetComponent<Bullet>().SetWay(false);
            } else
            {
                Balle.GetComponent<Bullet>().SetWay(true);

            }
        }
        else
        {
            if (gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().isWallSlide)
            {
                Balle.GetComponent<Bullet>().SetWay(false);
            } else
            {
                Balle.GetComponent<Bullet>().SetWay(true);

            }
        }
        if (isDoubleBullet)
        {
            GameObject Balle2;
            Balle2 = Instantiate(BalleType, null);
            Balle2.transform.position = gameObject.transform.position;

            if (gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().dir < 0)
            {
                if (!gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().isWallSlide)
                {
                    Balle2.GetComponent<Bullet>().SetWay(true);
                }
                else
                {
                    Balle2.GetComponent<Bullet>().SetWay(false);

                }
            }
            else
            {
                if (gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().isWallSlide)
                {
                    Balle2.GetComponent<Bullet>().SetWay(true);
                }
                else
                {
                    Balle2.GetComponent<Bullet>().SetWay(false);

                }
            }
        }
        Balle.GetComponent<Bullet>().speed += ballSpeedBonus;
        Balle.GetComponent<Bullet>().sizeBonus += ballSizeBonus;

    }


}
