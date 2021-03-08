using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootingEnemy : Enemy
{
    [SerializeField] public GameObject BalleType;
    public float MinTimeBtwShot;
    public float MaxTimeBtwShot;
    public float startTimeBtwShots;
    public GameObject enemyBullet;
    private Transform player;
    public bool HaveToShoot;
    public Animator anim;
    public Vector2 CanonOffset;
    public int dirToShoot = 0;
    public float KnockBackForce;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        RestartTimer();
    }

    void Update()
    {
        if (HaveToShoot && GetComponent<EnemyMovement>().isAlive)
        {
            GetComponent<EnemyMovement>().myState = EnemyMovement.STATES.Attack;
            HaveToShoot = false;
            GetComponent<EnemyMovement>().speed = 0;
            if (player.position.x - gameObject.transform.position.x > 0)
            {
                dirToShoot = 1;
                anim.Play("Attack_r");
            }
            else
            {
                dirToShoot = -1;
                anim.Play("Attack_l");
            }

        }
    }

    IEnumerator TimerBeforeShoot(float time)
    {
        yield return new WaitForSeconds(time);
        HaveToShoot = true;
        yield return null;
    }

    public void RestartTimer()
    {
        StartCoroutine(TimerBeforeShoot(Random.Range(MinTimeBtwShot,MaxTimeBtwShot)));
    }

    public void SpawnAndSetABullet()
    {
        GameObject Bullet;
        Bullet = Instantiate(enemyBullet);
        Bullet.transform.position = gameObject.transform.position + new Vector3(CanonOffset.x * dirToShoot, CanonOffset.y, 0);
        Bullet.GetComponent<EnemyBullet>().SetWay(new Vector2(dirToShoot, 0));
    }

    public void AddKnockback()
    {
        GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(dirToShoot * -1,0).normalized * KnockBackForce);
    }

    public void StopKnockBack()
    {

    }
    public void EndOfAttack()
    {
        RestartTimer();
        BackToWalk();
    }

    public void BackToWalk()
    {
        GetComponent<EnemyMovement>().speed = GetComponent<EnemyMovement>().Normalspeed;
        GetComponent<EnemyMovement>().myState = EnemyMovement.STATES.Walk;
   
    }




}