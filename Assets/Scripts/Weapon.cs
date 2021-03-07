using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int ballSpeedBonus = 0;
    public int ballSizeBonus = 0;
    public bool isDoubleBullet = false;
    public bool isBulletCrossing;
    public bool isVerticalBullet;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoAction()
    {
        Shoot();
    }
    public virtual void Shoot() { }
    public void setSpeedBonus(int speedBonus)
    {
        ballSpeedBonus = speedBonus;
    }
    public void setSizeBonus(int sizeBonus)
    {
        ballSizeBonus = sizeBonus;
    }
    public void setDoubleBullet(bool doubleBullet)
    {
        isDoubleBullet = doubleBullet;
    }
    
    public void setBulletCrossing(bool bulletCrossing)
    {
        isBulletCrossing = bulletCrossing;
    }

    public void setVerticalBullet(bool verticalBullet)
    {
        isVerticalBullet = verticalBullet;
    }
}
