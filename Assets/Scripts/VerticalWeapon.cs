using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalWeapon : Weapon
{
    [SerializeField] private GameObject BalleType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Shoot()
    {
        GameObject Balle;
        Balle = Instantiate(BalleType, null);
        Balle.transform.position = gameObject.transform.position;
        Debug.Log("Arme Bleu");
    }
}
