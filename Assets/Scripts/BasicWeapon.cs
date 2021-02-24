using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : Weapon
{
    [SerializeField] public GameObject BalleType;
    [SerializeField] private CharacterData charData;
    // Start is called before the first frame update
    void Start()
    {
        charData = gameObject.transform.parent.gameObject.GetComponent<CharacterData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Action()
    {
        GameObject Balle;
        Balle = Instantiate(BalleType, null);
        Balle.transform.position = gameObject.transform.position;
        Balle.transform.position += new Vector3(0, 0, 0);

        Balle.GetComponent<MoveProjectile>().ownerCharData = charData;
        if (gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().dir < 0)
        {
            if (!gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().isWallSlide)
            {
                Balle.GetComponent<MoveProjectile>().SetWay(false);
            } else
            {
                Balle.GetComponent<MoveProjectile>().SetWay(true);

            }
        }
        else
        {
            if (gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().isWallSlide)
            {
                Balle.GetComponent<MoveProjectile>().SetWay(false);
            } else
            {
                Balle.GetComponent<MoveProjectile>().SetWay(true);

            }
        }
        if (isDoubleBullet)
        {
            GameObject Balle2;
            Balle2 = Instantiate(BalleType, null);
            Balle2.transform.position = gameObject.transform.position;
            Balle2.GetComponent<MoveProjectile>().ownerCharData = charData;

            if (gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().dir < 0)
            {
                if (!gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().isWallSlide)
                {
                    Balle2.GetComponent<MoveProjectile>().SetWay(true);
                }
                else
                {
                    Balle2.GetComponent<MoveProjectile>().SetWay(false);

                }
            }
            else
            {
                if (gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().isWallSlide)
                {
                    Balle2.GetComponent<MoveProjectile>().SetWay(true);
                }
                else
                {
                    Balle2.GetComponent<MoveProjectile>().SetWay(false);

                }
            }
        }
        Balle.GetComponent<MoveProjectile>().speed += ballSpeedBonus;
        Balle.GetComponent<MoveProjectile>().sizeBonus += ballSizeBonus;

    }


}
