using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class BGMData
{
    public string sceneName;
    public AudioClip clip;
}

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance{get; private set;}
    
    [SerializeField]private AudioSource audioSource;
     [SerializeField]private List<BGMData> bgmData;

    private Dictionary<string, AudioClip> bgmDict = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (var data in bgmData)
        {
            bgmDict[data.sceneName] = data.clip;
        }
    }

    public void PlayBgm(string sceneName)
    {
        if (bgmDict.TryGetValue(sceneName, out var clip))
        {
            if(audioSource.clip == clip)return;
            
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}
