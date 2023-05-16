using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Rig aimRig;
    [SerializeField] private float reloadTime;

    private Animator animator;
    private bool reloading;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnFire(InputValue value)
    {
        if (reloading)
            return;

        animator.SetTrigger("Fire");
    }

    private void OnReload(InputValue value)
    {
        StartCoroutine(ReloadRoutine());
    }

    IEnumerator ReloadRoutine()
    {
        animator.SetTrigger("Reload");
        aimRig.weight = 0f;
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        aimRig.weight = 0.6f;
        reloading = false;
    }
}
