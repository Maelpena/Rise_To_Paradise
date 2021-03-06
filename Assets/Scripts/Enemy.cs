using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventEnemyDeath : UnityEvent<float, Vector2, GameObject> { }

public class Enemy : MonoBehaviour
{
    public static EventEnemyDeath eventEnemyDeath = new EventEnemyDeath();
    public int life = 1;
    public int damage = 1;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Die();
        }
    }


    public void Die()
    {

        GetComponent<EnemyMovement>().PlayDie();

        eventEnemyDeath.Invoke(score, gameObject.transform.position, transform.parent.gameObject);
        
    }

    public int GetDamage()
    {
        return damage;
    }
   
    
}
