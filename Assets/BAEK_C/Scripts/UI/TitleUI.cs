using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUI : BaseUI
{
    public AudioSource audioSource;
    public Button startButton;
    public Button exitButton;
    public Button fakeExitButton;
    public VideoPlayer videoPlayer;
    
    public override void Init()
    {

        startButton.onClick.AddListener(() => OnStart(startButton.GetComponent<AudioSource>().clip));
        exitButton.onClick.AddListener(() => OnExit(exitButton.GetComponent<AudioSource>().clip));
        fakeExitButton.onClick.AddListener(() => OnFakeExit(fakeExitButton.GetComponent<AudioSource>().clip));

        videoPlayer.Play();  
    }

    public void OnStart(AudioClip clip)
    {
        StartCoroutine(PlaySoundThen(() => LoadStartScene(), clip));
    }

    void LoadStartScene()
    {
        bool tutorialDone = PlayerPrefs.GetInt("TutorialCompleted", 0) == 1;

        if (tutorialDone)
        {
            SceneManager.LoadScene("StageSelectScene");
        }
        else
        {
            SceneManager.LoadScene("Tutorial");
        }
    }

   public void OnExit(AudioClip clip)
    {
        Debug.Log("°ÔÀÓ Á¾·á");
        StartCoroutine(PlaySoundThen(() => Application.Quit(), clip));
    }

    public void OnFakeExit(AudioClip clip)
    {
        Debug.Log("³Í ¸¶¹ý¼Ò³à´Ù ¾îµô µµ¸Á°¡");
        StartCoroutine(PlaySoundThen(() => LoadStartScene(), clip));
    }

    private IEnumerator PlaySoundThen(System.Action onComplete, AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
            yield return new WaitForSeconds(clip.length);
        }
        onComplete?.Invoke();
    }

}
