using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PoolableObject poolableObject = other.GetComponent<PoolableObject>();

        if (poolableObject != null)
        {
            poolableObject.returnPool();
        }
        else if (other.CompareTag("Item"))
        {
            Destroy(other.gameObject);
        }
    }
}
