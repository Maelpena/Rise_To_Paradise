using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventEnemyDeath : UnityEvent<float,Vector2,GameObject> { }
public class CharacterData : MonoBehaviour
{
    public static EventEnemyDeath eventEnemyDeath = new EventEnemyDeath();
    public UnityEvent eventDeath;
    public Vector2 velocity;
    public int life;
    public int damage;
    public int score;
    public bool IsPlayer;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void takeDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            die();
        }
    }

    private void die()
    {
        if (IsPlayer)
        {
            GetComponent<PlayerMovement>().Die();
            FollowPlayer.shakeCoolDown = 1f;

        }
        else
        {
            GetComponent<EnemyMovement>().Die();
            FollowPlayer.shakeCoolDown = 0.2f;
            eventEnemyDeath.Invoke(score,gameObject.transform.position,transform.parent.gameObject);

        }
    }
}