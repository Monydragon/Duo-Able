using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip dungeonBGM;
    public AudioSource SFX_Source;
    public AudioSource BGM_Source;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        BGM_Source.clip = dungeonBGM;
        BGM_Source.loop = true;
        BGM_Source.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFX_Source.clip = clip;
        SFX_Source.Play();
    }

    public void PlayBGM(AudioClip clip)
    {
        BGM_Source.clip = clip;
        BGM_Source.Play();
    }
}
