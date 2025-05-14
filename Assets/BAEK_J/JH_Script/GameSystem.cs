using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollBar;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameObject claerPanel;
    [SerializeField] private GameObject failPanel;

    [Header("???? ????????")]
    public int isStage = 0;
    [SerializeField] private float beforeSpeed; // ???? ?? ??? ????
    private Coroutine speedChange; // ????©£? ????

    [Header("?? ????? ?????? ????")]
    public static float speed = 5;
    [SerializeField] private float moveDistance;
    [SerializeField] private float destination = 100;
    
    [SerializeField]private bool isBoss = false;

    public static bool hasFinished = false; // Finish ????? ??? ???? ????

    // ???? ???? ????
    public static bool[] key = new bool[3];


    private void Start()
    {
        hasFinished = false;
        Time.timeScale = 1f;
        moveDistance = 0;
    }

    private void Update()
    {
        if (hasFinished) return;

        moveDistance += speed * Time.deltaTime;

        if (moveDistance >= destination)
        {
            hasFinished = true;
            scoreManager.SetScore();
            Finish();
        }

        scrollBar.value = Mathf.Clamp01(moveDistance / destination);
    }

    public void ChangeSpeed(float amount, float duration) // 1) ???? ?©£????? ??? ????
    {
        beforeSpeed = speed;
        speed += amount;
        Debug.Log($"??? ???? {speed} (???? ?©£?: {duration}??)");
        speedChange = StartCoroutine(RevertSpeed(duration));
    }
    private IEnumerator RevertSpeed(float duration) // 2) ????©£? ????? ????
    {
        yield return new WaitForSeconds(duration);
        speed = beforeSpeed;
        Debug.Log($"??? ???? {speed}");
    }

    private void Finish() // ??????? ????? ???? ? ????
    {
        if (QuestManager.isQuestClear == true)
        {
            GameSystem.key[isStage - 1] = true;
            Debug.Log($"{isStage}??¡Æ ? ???");
        }

        if (isBoss)
        {
            SceneManager.LoadScene("Scenes/TrueEndingScene");
        }
    }

    public void Fail()
    {
        failPanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            claerPanel.SetActive(true);
        }
    }
}
