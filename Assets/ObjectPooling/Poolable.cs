using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class Poolable : MonoBehaviour
    {
        [SerializeField] private bool autoRelease;
        [SerializeField] private float releaseTime;

        private ObjectPool pool;
        public ObjectPool Pool { get { return pool; } set { pool = value; } }

        private void OnEnable()
        {
            if (autoRelease)
                releaseRoutine = StartCoroutine(ReleaseRoutine());
        }

        public void Release()
        {
            if (releaseRoutine != null)
                StopCoroutine(releaseRoutine);
            if (pool != null)
                pool.Release(this);
        }

        Coroutine releaseRoutine;
        IEnumerator ReleaseRoutine()
        {
            yield return new WaitForSeconds(releaseTime);
            if (pool != null)
                pool.Release(this);
        }
    }
}

