using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootingEnemy : Enemy
{
    [SerializeField] public GameObject BalleType;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject enemyBullet;
    private Transform player;

    void Start()
    {
        timeBtwShots = startTimeBtwShots;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (timeBtwShots <= 0)
        {
            Instantiate(enemyBullet, null);
            enemyBullet.transform.position = gameObject.transform.position;
            timeBtwShots = startTimeBtwShots;
        }
        else
            timeBtwShots -= Time.deltaTime;
    }
}