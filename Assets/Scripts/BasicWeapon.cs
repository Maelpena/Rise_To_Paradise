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
        int direction = gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().lookAtLeft == true ? 1 : -1;
        Balle.GetComponent<Bullet>().SetWay(new Vector2(direction, 0));
        

        if (isDoubleBullet)
        {
            GameObject Balle2;
            Balle2 = Instantiate(BalleType, null);
            Balle2.transform.position = gameObject.transform.position;
            int direction2 = gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().lookAtLeft == false ? 1 : -1;
            Balle2.GetComponent<Bullet>().SetWay(new Vector2(direction2, 0));
        }

        if (isVerticalBullet)
        {
            GameObject Balle3;
            Balle3 = Instantiate(BalleType, null);
            Balle3.transform.position = gameObject.transform.position;
            Balle3.GetComponent<Bullet>().SetWay(new Vector2(0, 1));
        }

        Balle.GetComponent<Bullet>().speed += ballSpeedBonus;
        Balle.GetComponent<Bullet>().sizeBonus += ballSizeBonus;
        Balle.GetComponent<Bullet>().bulletCrossing = isBulletCrossing;

    }


}
