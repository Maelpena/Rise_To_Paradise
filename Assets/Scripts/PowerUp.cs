using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int bonusAmount;
    public string bonusType;
    public float bonusTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("mamamam");
            collision.gameObject.GetComponent<PlayerMovement>().PickUpPowerUp(bonusType, bonusAmount, bonusTime);
            Destroy(this);
        }
    }
   
}
