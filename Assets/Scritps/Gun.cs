using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private int damage;

    public void Fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
        {
            IHittable target = hit.transform.GetComponent<IHittable>();
            target?.Hit(hit, damage);
        }
    }
}
