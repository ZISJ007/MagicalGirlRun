using UnityEngine;

public class Back : MonoBehaviour
{
    float scrollSpeed;

    public void Init(float _speed)
    {
        scrollSpeed = _speed;
    }

    void Update()
    {
        // 왼쪽으로 이동
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
    }
}