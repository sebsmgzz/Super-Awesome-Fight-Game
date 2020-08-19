using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{

    [SerializeField]
    PoolItem poolItem;

    int collisionsCount = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collisionsCount == 3)
        {
            collisionsCount = 0;
            Pools.Return(poolItem, gameObject);
        }
        else
        {
            collisionsCount += 1;
        }
    }

}
