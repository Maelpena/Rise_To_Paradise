using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public static float shakeCoolDown = 0;


    void Update()
    {
        if (Player != null)
        {
            transform.position = new Vector3(gameObject.transform.position.x, Player.transform.position.y, gameObject.transform.position.z);
        }

        if (shakeCoolDown > 0)
        {
            shakeScreen();
            shakeCoolDown -= Time.deltaTime;
        }
        if (shakeCoolDown < 0)
        {
            shakeCoolDown = 0;
            transform.position = new Vector3(1, Player.transform.position.y, transform.position.z);
        }

    }

    public void shakeScreen()
    {
        Vector3 randomPoint = new Vector3(1 + Random.Range(-0.1f, 0.1f), Player.transform.position.y + Random.Range(-0.1f, 0.1f), transform.position.z);
        transform.position = randomPoint;
    }
}
