using ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class InstantiateTest : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int count;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                for (int i = 0; i < count; i++)
                {
                    GameObject gameObj = Instantiate(prefab);
                    gameObj.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
                }
            }
        }
    }
}
