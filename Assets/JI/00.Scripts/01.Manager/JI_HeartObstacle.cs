using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JI_HeartObstacle : MonoBehaviour
{
    private JI_ResourceController resourceController;
    public GameObject heartEffectPrefab; // ��Ʈ ȿ�� ������Ʈ
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
                resourceController.TakeHeal(1); // ��Ʈ �浹 �� ü�� ȸ��
                GameObject heartEffect = Instantiate(heartEffectPrefab, transform.position, Quaternion.Euler(-90f, 0f, 0f));
                Destroy(gameObject); // ��Ʈ ������Ʈ �ı�
                Destroy(heartEffect, 1f);
            }
        }
    }
}
