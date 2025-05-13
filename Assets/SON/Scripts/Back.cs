using UnityEngine;

public class BackRemover : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 2f;
    [SerializeField] private float destroyThresholdX = -30f;

    public float ScrollSpeed
    {
        get => scrollSpeed;
        set => scrollSpeed = Mathf.Max(0, value);
    }

    void Update()
    {
        // 왼쪽으로 이동
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        // x 좌표가 -30 이하가 되면 파괴
        if (transform.position.x <= destroyThresholdX)
        {
            Destroy(gameObject);
        }
    }
}
