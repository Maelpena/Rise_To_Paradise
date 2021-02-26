using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Player != null)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, Player.transform.position.y, gameObject.transform.position.z);
        }
        //shakeScreen();
    }

    public void shakeScreen()
    {
        Vector3 randomPoint = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), transform.position.z);
        transform.position = randomPoint;
    }
}
