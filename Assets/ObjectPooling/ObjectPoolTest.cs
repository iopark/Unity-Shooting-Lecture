using ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class ObjectPoolTest : MonoBehaviour
    {
        [SerializeField] private ObjectPool objectPools;
        [SerializeField] private int count;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                for (int i = 0; i < count; i++)
                {
                    Poolable poolable = objectPools.Get();
                    poolable.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
                }
            }
        }
    }

}
