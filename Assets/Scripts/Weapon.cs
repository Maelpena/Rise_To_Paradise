using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int ballSpeedBonus = 0;
    public int ballSizeBonus = 0;
    public bool isDoubleBullet = false;


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
        Action();
    }
    protected virtual void Action() { }
    public void setSpeedBonus(int speedBonus)
    {
        ballSpeedBonus = speedBonus;
    }
    public void setSizeBonus(int sizeBonus)
    {
        ballSizeBonus = sizeBonus;
        Debug.Log(sizeBonus + " size bonus");
    }
    public void setDoubleBullet(bool doubleBullet)
    {
        isDoubleBullet = doubleBullet;
    }
}
