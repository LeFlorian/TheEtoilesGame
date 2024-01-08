using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource walkSource;
    /*
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource sfxSource;*/

    [Header("Audio Clip")]
    public AudioClip death;
    public AudioClip walk;
    public AudioClip jump;
    public AudioClip interaction;
    public AudioClip success;
    public AudioClip tempete;
    public AudioClip music_lobby;
    public AudioClip music_geo;
    public AudioClip music_enigme;
    //public AudioClip music_musique; // pu besoin
    public AudioClip music_smash;
    public AudioClip music_boss_smash;
    public AudioClip sfx_entre_boss_smash;
    public AudioClip music_fanart;



    // à modifier, continuer

    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = music_lobby;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            if (musicSource.clip != music_lobby)
            {
                musicSource.Stop();
                musicSource.clip = music_lobby;
                musicSource.loop = true;
                musicSource.volume = 0.1f;

            }
            sfxSource.clip = null;
            if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }

        }
        else if (SceneManager.GetActiveScene().name == "01 - Geographie")
        {

            if (musicSource.clip != music_geo)
            {
                musicSource.Stop();
                musicSource.clip = music_geo;
                musicSource.loop = true;
            }
            sfxSource.clip = null;
            if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }

        }
        else if (SceneManager.GetActiveScene().name == "02 - Enigme")
        {
            if (musicSource.clip != music_enigme)
            {
                musicSource.Stop();
                musicSource.clip = music_enigme;
                musicSource.loop = true;
                musicSource.volume = 0.1f;

            }
            sfxSource.clip = null;
            if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }

        }
        else if (SceneManager.GetActiveScene().name == "03 - Musique")
        {
            sfxSource.Stop();
            musicSource.Stop();

        }
        else if (SceneManager.GetActiveScene().name == "04 - Versus")
        {
            Debug.Log(SceneManager.GetActiveScene().name);
            if (musicSource.clip != music_smash)
            {
                musicSource.Stop();
                musicSource.clip = music_smash;
                musicSource.loop = true;
            }
            sfxSource.clip = null;
            if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }

            music_boss_smash = null;
            sfx_entre_boss_smash = null;


}
        else if (SceneManager.GetActiveScene().name == "05 - Amour")
        {
            if (musicSource.clip != music_fanart)
            {
                musicSource.Stop();
                musicSource.clip = music_fanart;
                musicSource.loop = true;
            }
            sfxSource.clip = null;
            if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }

        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
