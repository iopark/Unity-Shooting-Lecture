using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private int damage;
    [SerializeField] private ParticleSystem bulletEffect;
    [SerializeField] private ParticleSystem muzzleEffect;
    [SerializeField] private TrailRenderer bulletTrail;

    public void Fire()
    {
        muzzleEffect.Play();

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
        {
            IHittable target = hit.transform.GetComponent<IHittable>();
            target?.Hit(hit, damage);

            //ParticleSystem effect = Instantiate(bulletEffect, hit.point, Quaternion.LookRotation(hit.normal));
            ParticleSystem effect = GameManager.Pool.Get(bulletEffect, hit.point, Quaternion.LookRotation(hit.normal));
            effect.transform.parent = hit.transform.transform;
            StartCoroutine(DelayReleaseRoutine(effect.gameObject, 3f));
            //Destroy(effect.gameObject, 3f);

            //TrailRenderer trail = Instantiate(bulletTrail, muzzleEffect.transform.position, Quaternion.identity);
            TrailRenderer trail = GameManager.Pool.Get(bulletTrail, muzzleEffect.transform.position, Quaternion.identity);
            StartCoroutine(TrailRoutine(trail, trail.transform.position, hit.point));
            StartCoroutine(DelayReleaseRoutine(trail.gameObject, 3f));
            //Destroy(trail.gameObject, 3f);
        }
        else
        {
            //TrailRenderer trail = Instantiate(bulletTrail, muzzleEffect.transform.position, Quaternion.identity);
            TrailRenderer trail = GameManager.Pool.Get(bulletTrail, muzzleEffect.transform.position, Quaternion.identity);
            StartCoroutine(TrailRoutine(trail, trail.transform.position, Camera.main.transform.forward * maxDistance));
            StartCoroutine(DelayReleaseRoutine(trail.gameObject, 3f));
            //Destroy(trail.gameObject, 3f);
        }
    }

    IEnumerator DelayReleaseRoutine(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.Pool.Release(obj);
    }

    IEnumerator TrailRoutine(TrailRenderer trail, Vector3 startPoint, Vector3 endPoint)
    {
        float totalTime = Vector2.Distance(startPoint, endPoint) / maxDistance;

        float time = 0;
        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPoint, endPoint, time);
            time += Time.deltaTime / totalTime;

            yield return null;
        }
    }
}
