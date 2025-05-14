using UnityEngine;

public class JI_PlayerStats : MonoBehaviour
{
    [Header("플레이어 체력")]
    [SerializeField] private int maxHp = 3; // 최대 체력 (내부 전용)
    [SerializeField] private int currentHp; // 현재 체력 (내부 전용)
    HeartsUI heartsUI; // 하트 UI를 참조하기 위한 변수
    public int MaxHp // 최대 체력 외부에서  접근 가능
    {
        get => maxHp;
        set
        {

            maxHp = Mathf.Max(1, value); // 최소 1 이상으로 제한
            // currentHp 최소값이 0, 최대값이 maxHp로 제한
            currentHp = Mathf.Clamp(currentHp, 0, maxHp);
            // heartsUI가 null이 아니면 최대 체력 변경 시 UI 갱신
            heartsUI?.UpdateHearts();  
        }
    }
    public int CurrentHp // 현재 체력 외부에서 접근 가능
    {
        get => currentHp;
        private set
        {
            currentHp = Mathf.Clamp(value, 0, maxHp);
            // heartsUI가 null이 아니면 체력 변경될 때 UI 갱신
            heartsUI?.UpdateHearts(); 

        }
    }

    private void Awake()
    {
        currentHp = maxHp;
        heartsUI = FindObjectOfType<HeartsUI>();
      
       
    }
    private void Start()
    {
        // heartsUI가 null이 아니면 시작할 때 UI 초기화
        heartsUI?.UpdateHearts();
    }
    public void Heal(int amount) // 체력 회복 
    {
        CurrentHp += amount;
    }

    public void TakeDamage(int amount) // 체력 감소
    {
        CurrentHp -= amount;
    }

    public void SetMaxHp(int amount) // 최대 체력 설정
    {
        MaxHp += amount;
        CurrentHp = maxHp; // 최대 체력 증가 시 현재 체력도 최대 체력으로 설정
    }
}
