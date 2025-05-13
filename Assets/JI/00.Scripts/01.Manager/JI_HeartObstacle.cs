using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JI_HeartObstacle : MonoBehaviour
{
    private JI_ResourceController resourceController;
    public GameObject heartEffectPrefab; // 하트 효과 오브젝트
    private void Awake()
    {
        resourceController = FindObjectOfType<JI_ResourceController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (resourceController != null)
            {
                resourceController.TakeHeal(1); // 하트 충돌 시 체력 회복
                GameObject heartEffect = Instantiate(heartEffectPrefab, transform.position, Quaternion.Euler(-90f, 0f, 0f));
                Destroy(gameObject); // 하트 오브젝트 파괴
                Destroy(heartEffect, 1f);
            }
        }
    }
}
