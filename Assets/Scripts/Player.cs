using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public Weapon weapon;
    private int life = 1;
    public int score;
    public float powerUpCoolDown = 0;
    public bool isShield = false;
    public bool isInvincible = false;
    public UnityEvent playerDeath;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (powerUpCoolDown > 0)
        {
            powerUpCoolDown -= Time.deltaTime;
        }
        else if (powerUpCoolDown < 0)
        {
            powerUpCoolDown = 0;
            resetPowerUp();
        }
    }

    public void Shoot()
    {
        weapon.Shoot();
    }
    public void PickUpPowerUp(string type, int amount, float time)
    {
        switch (type)
        {
            case "bullet_speed":
                resetPowerUp();
                weapon.setSpeedBonus(amount);
                break;
            case "bullet_size":
                resetPowerUp();
                weapon.setSizeBonus(amount);
                break;
            case "shield":
                resetPowerUp();
                isShield = true;
                break;
            case "double_bullet":
                resetPowerUp();
                weapon.setDoubleBullet(true);
                break;
            case "invincible":
                resetPowerUp();
                isInvincible = true;
                break;
            case "bullet_crossing":
                resetPowerUp();
                weapon.setBulletCrossing(true);
                break;
            case "vertical_bullet":
                resetPowerUp();
                weapon.setVerticalBullet(true);
                break;
            default:
                break;
        }
        powerUpCoolDown = time;
    }

    public void resetPowerUp()
    {
        weapon.setSpeedBonus(0);
        weapon.setSizeBonus(0);
        isShield = false;
        weapon.setDoubleBullet(false);
        isInvincible = false;
        weapon.setBulletCrossing(false);
        weapon.setVerticalBullet(false);


    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Die();
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        CameraController.shakeCoolDown = 1f;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<PlayerMovement>().PlayDie();
        yield return new WaitForSeconds(1);
        playerDeath.Invoke();

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag.Equals("Enemies") && (isShield || isInvincible))
        {
            if (isShield == true)
            { 
                isShield = false;
                powerUpCoolDown = 0;
            }
            collision.gameObject.GetComponent<Enemy>().Die();

        } else if(collision.gameObject.tag.Equals("Enemies") || collision.gameObject.tag.Equals("Spike"))
        {
            TakeDamage(1);
        }
    }
}
