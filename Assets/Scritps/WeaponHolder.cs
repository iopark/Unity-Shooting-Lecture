using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Gun gun;

    public void Fire()
    {
        gun.Fire();
    }
}
