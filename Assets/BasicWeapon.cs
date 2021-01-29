using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : Weapon
{
    [SerializeField] private GameObject BalleType;
    [SerializeField] private CharacterData charData;
    // Start is called before the first frame update
    void Start()
    {
        charData = gameObject.transform.parent.gameObject.GetComponent<CharacterData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Action()
    {
        GameObject Balle;
        Balle = Instantiate(BalleType, null);
        Balle.transform.position = gameObject.transform.position;
        Balle.GetComponent<MoveProjectile>().ownerCharData = charData;
        if (gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().dir < 0)
        {
            if (!gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().isWallSlide)
            {
                Balle.GetComponent<MoveProjectile>().SetWay(false);
            }

        }
        else
        {
            if (gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().isWallSlide)
            {
                Balle.GetComponent<MoveProjectile>().SetWay(false);
            }
        }
    }
}
