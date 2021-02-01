using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterData : MonoBehaviour
{
    public UnityEvent eventDeath;
    public Vector2 velocity;
    public int life;
    public int damage;
    public int score;


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
        if ( life <= 0)
        {
            die();
        }
    }

    private void die()
    {
        if(GetComponent<PlayerMovement>() != null) GetComponent<PlayerMovement>().Die();
        if(GetComponent<EnemyMovement>() != null) GetComponent<EnemyMovement>().Die();
        
    }
}
