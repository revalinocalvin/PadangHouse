using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(AudioManager).Name;
                    instance = obj.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }

    public AudioSource audioSource;

    [Header("Audios not in object here")]
    [SerializeField] private AudioClip[] plates;
    [SerializeField] private AudioClip ding;
    [SerializeField] private AudioClip uiClick;

    private AudioClip audio;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void DoAudio(string setAudio)
    {
        if (setAudio == "plates")
        {
            int i = Random.Range(0, 3);
            audio = plates[i];

            audioSource.clip = audio;
            audioSource.Play();
        }
        else if (setAudio == "ding")
        {            
            audio = ding;

            audioSource.clip = audio;
            audioSource.Play();
        }
        else if (setAudio == "click")
        {
            audio = uiClick;

            audioSource.clip = audio;
            audioSource.Play();
        }
    }
}
