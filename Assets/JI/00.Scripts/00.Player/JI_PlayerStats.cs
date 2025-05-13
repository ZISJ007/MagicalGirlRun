using UnityEngine;

public class JI_PlayerStats : MonoBehaviour
{
    [Header("플레이어 체력")]
    [SerializeField] private float maxHp = 5f;
    [SerializeField] private float currentHp; // 현재 체력(내부 전용)
    public float MaxHp
    {
        get => maxHp;
        set
        {
            maxHp = Mathf.Max(0f, value);// 최대 체력은 0 이상으로만 설정          
            currentHp = Mathf.Clamp(currentHp, 0f, maxHp);// currentHp가 새 maxHp를 넘지 않도록 제한
        }
    }
    public float CurrentHp
    {
        get => currentHp;
        private set
        {
            currentHp = Mathf.Clamp(value, 0f, maxHp);
        }
    }
    public void Heal(float amount) => currentHp += amount;
    public void TakeDamage(float amount) => currentHp -= amount;

    private void Awake()
    {
        currentHp = maxHp; // 초기 체력 설정
    }

}