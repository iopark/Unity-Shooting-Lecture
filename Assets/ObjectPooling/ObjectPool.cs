using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPooling
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private Poolable poolablePrefab;

        [SerializeField] private int poolSize;
        [SerializeField] private int maxSize;

        private Stack<Poolable> objectPool = new Stack<Poolable>();

        private void Start()
        {
            CreatePool();
        }

        private void CreatePool()
        {
            for (int i = 0; i < poolSize; i++)
            {
                Poolable poolable = Instantiate(poolablePrefab);
                poolable.Pool = this;
                poolable.gameObject.SetActive(false);
                poolable.transform.parent = transform;
                objectPool.Push(poolable);
            }
        }

        public Poolable Get()
        {
            if (objectPool.Count > 0)
            {
                Poolable poolable = objectPool.Pop();
                poolable.gameObject.SetActive(true);
                poolable.transform.parent = null;
                return poolable;
            }
            else
            {
                Poolable poolable = Instantiate(poolablePrefab);
                poolable.Pool = this;
                return poolable;
            }
        }

        public void Release(Poolable poolable)
        {
            if (objectPool.Count < maxSize)
            {
                poolable.gameObject.SetActive(false);
                poolable.transform.parent = transform;
                objectPool.Push(poolable);
            }                
            else
                Destroy(poolable.gameObject);
        }
    }
}
