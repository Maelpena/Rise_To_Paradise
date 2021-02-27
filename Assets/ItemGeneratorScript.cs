using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ItemGeneratorScript : MonoBehaviour
{
    public UnityEvent enemyDied;
    public List<GameObject> powerUps = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyDied()
    {
        Debug.Log("ENEMY Died");
    }

    public void eventDeath()
    {

    }
}
