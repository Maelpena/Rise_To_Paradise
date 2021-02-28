using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PowerUpGui : MonoBehaviour
{

    public Image powerUpSprite;
    public GameObject BG_powerUpSprite;
    public GameObject player;
    private PowerUp powerUp;


// Start is called before the first frame update
    void Start()
    {
    }
    private void OnEnable()
    {
        PowerUp.eventPickUpCollectible.AddListener(SetPowerUp);
    }
    // Update is called once per frame
    void Update()
    {
        if(player!= null)
        {
            if (player.GetComponent<PlayerMovement>().powerUpCoolDown > 0)
            {
                float filledPercent = player.GetComponent<PlayerMovement>().powerUpCoolDown / powerUp.bonusTime;

                BG_powerUpSprite.GetComponent<Image>().fillAmount = 1 - filledPercent;
            }
            else
            {
                powerUpSprite.enabled = false;
                BG_powerUpSprite.GetComponent<Image>().enabled = false;

            }
        }
        
    }

    public void SetPowerUp(PowerUp newPowerUp)
    {
        powerUp = newPowerUp;

        powerUpSprite.sprite = powerUp.bonusSprite;
        BG_powerUpSprite.GetComponent<Image>().sprite = powerUp.bonusSprite;
        BG_powerUpSprite.GetComponent<Image>().sprite = powerUp.bonusSprite;
        BG_powerUpSprite.GetComponent<Image>().color = Color.black;

        powerUpSprite.enabled = true;
        BG_powerUpSprite.GetComponent<Image>().enabled = true;

    }

}
