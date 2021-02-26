using System.Collections;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    public static float shakeCoolDown = 0;
    
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
        

        if (shakeCoolDown > 0)
        {
            shakeScreen();
            shakeCoolDown -= Time.deltaTime;
        }
        if (shakeCoolDown < 0)
        {
            shakeCoolDown = 0;
            transform.position = new Vector3(1, transform.position.y, transform.position.z);
        }
    }

    public void shakeScreen()
    {
        Vector3 randomPoint = new Vector3(transform.position.x + Random.Range(-0.1f, 0.1f), transform.position.y + Random.Range(-0.1f, 0.1f), transform.position.z);
        transform.position = randomPoint;
    }
}
