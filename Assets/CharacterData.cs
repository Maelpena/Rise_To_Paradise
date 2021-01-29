using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
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
