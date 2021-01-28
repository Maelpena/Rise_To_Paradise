using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Weapon ActualWeapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void UseWeapon()
    {
        ActualWeapon.DoAction();
    }
    // Update is called once per frame
    void Update()
    {
    }
}
