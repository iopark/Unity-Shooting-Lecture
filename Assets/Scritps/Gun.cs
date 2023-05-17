using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private int damage;
    [SerializeField] private ParticleSystem bulletEffect;
    [SerializeField] private ParticleSystem muzzleEffect;

    public void Fire()
    {
        muzzleEffect.Play();

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
        {
            IHittable target = hit.transform.GetComponent<IHittable>();
            target?.Hit(hit, damage);

            ParticleSystem effect = Instantiate(bulletEffect, hit.point, Quaternion.LookRotation(hit.normal));
            effect.transform.parent = hit.transform.transform;
            Destroy(effect.gameObject, 3f);
        }
    }
}
