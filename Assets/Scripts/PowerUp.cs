using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventPickUpCollectible : UnityEvent<PowerUp> { }

public class PowerUp : MonoBehaviour
{
    public static EventPickUpCollectible eventPickUpCollectible = new EventPickUpCollectible();

    public int bonusAmount;
    public string bonusType;
    public float bonusTime;
    public Sprite bonusSprite;
    public GameObject powerUpGui;

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
            collision.gameObject.GetComponent<PlayerMovement>().PickUpPowerUp(bonusType, bonusAmount, bonusTime);
            eventPickUpCollectible.Invoke(this);

           //PowerUpGui.Draw(this);
            Destroy(gameObject);
        }
    }
   
}
