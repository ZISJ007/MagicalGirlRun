using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAttackObject : MonoBehaviour
{
    [SerializeField] private float moveDistance = 3f;
    [SerializeField] private float moveSpeed = 5f;

    public void StartMove()
    {
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        
        Vector3 startPos = transform.position;;
        Vector3 tartgetPos = startPos+transform.right * moveDistance;

        while (Vector3.Distance(transform.position, tartgetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, tartgetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        while (Vector3.Distance(transform.position, startPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
