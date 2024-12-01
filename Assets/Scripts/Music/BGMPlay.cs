using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BGMPlay : MonoBehaviour
{
    public static BGMPlay instance;
    AudioSource bgmAudioSource;
    List<AudioSource> audioSourceList;

    private void Awake()
    {
        bgmAudioSource = GetComponent<AudioSource>();
        audioSourceList = new List<AudioSource>();
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        bgmAudioSource.Play();
    }
    private void Update()
    {
        //Remove audioSource Components that finished;
        for (int i = audioSourceList.Count - 1; i >= 0; i--)
        {
            if (!audioSourceList[i].isPlaying)
            {
                Destroy(audioSourceList[i]);
                audioSourceList.RemoveAt(i);
            }
        }
    }
    public void PlayMusic(AudioClip audioClip)
    {
        AudioSource _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.clip = audioClip;
        _audioSource.Play();
        audioSourceList.Add(_audioSource);
    }
}
