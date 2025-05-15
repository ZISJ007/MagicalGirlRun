using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JI_ResourceController : MonoBehaviour
{
    private JI_PlayerController playerController;
    private JI_PlayerStats playerStats;
    private PlayerUIManager playerUIManager;
    private GameSystem gameSystem;
    [SerializeField] private bool BossStage;

    [Header("???? ??????")]
    public int obstacleDamage = 0;
    [Header("???? ????©£?(??)")]
    public float invincibilityDuration = 1.0f;
    [Header("?¡À???? ???? ????")]
    public bool isInvincible = false;
    private Coroutine invincibilityCoroutine;
    

    private void Awake()
    {
        gameSystem=FindObjectOfType<GameSystem>();
        playerUIManager= FindObjectOfType<PlayerUIManager>();
        playerController = GetComponent<JI_PlayerController>();
        playerStats = GetComponent<JI_PlayerStats>();
    }
    private void Update()
    {
        if (GameSystem.hasFinished)
        {
            isInvincible = true;
            transform.position += Vector3.right * GameSystem.speed * Time.deltaTime;
        }
    }

    public void Heal(int amount)
    {
        playerStats.Heal(amount);  // ?¡À???? ??? ???
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) return; // ???? ?????? ???? return
        playerController.HandleDamage(invincibilityDuration);  // ?¡À???? ???? ??????? ????? ???
        playerStats.TakeDamage(amount);  // ?¡À???? ??? ????
        StartInvincibility(invincibilityDuration); //?????? ???? ?? ???? ???? ????
        
        playerUIManager.TryShowRadomText();
        
        if (playerStats.CurrentHp <= 0)  // ????? 0 ?????? ?? ???
        {
            Death();
            if (BossStage)
            {
                SceneManager.LoadScene("BadEnding");
            }
        }
    }
    public void TakeHeal(int amount) // ??? ???
    {
        if (playerStats.CurrentHp >= playerStats.MaxHp) return; // ??? ??? ????? ???? return
        playerStats.Heal(amount);  // ?¡À???? ??? ???
    }
    public void StartInvincibility(float duration)
    {
        // ???? ?????? ???? ?????? ????
        if (invincibilityCoroutine != null)
        {
            StopCoroutine(invincibilityCoroutine); //???? ???? > ???? ???? ?? ??? ????
        }
        invincibilityCoroutine = StartCoroutine(InvincibilityCoroutine(duration)); //???? ????
    }

    private IEnumerator InvincibilityCoroutine(float duration) //???? ????
    {
        isInvincible = true; // ???? ????
        yield return new WaitForSeconds(duration); // ?????? ??(duration) ??? ??? (???? ????©£?)
        isInvincible = false; // ???? ????
        invincibilityCoroutine = null; // ???? ????
    }
    private bool Death()  // ?¡À???? ??? 
    {
        playerController.HandleDeath();  // ?¡À???? ??? ???
        isInvincible = true;
        enabled = false;  // JI_ResourceController ??????
        gameSystem.Fail();
        
        
        return true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Obstacle"))  // ?????? ?úô???? ??
        {
            TakeDamage(obstacleDamage);  // ??? ????
        }
    }
}