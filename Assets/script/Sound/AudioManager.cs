using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class AudioManager : Singleton<AudioManager>
{
    private void Awake()
    {
        OnLevelWasLoaded(SceneManager.GetActiveScene().buildIndex);
        musicSource.mute = !DataGame.Instance.globaldata.Music;
        GlobalSfxSource.mute = !DataGame.Instance.globaldata.Sfx;
        UserSfxSource.mute = !DataGame.Instance.globaldata.Sfx;
    }
    public Sound[] musicBg, GlobalSfxSound, UserSfxSound;
    public AudioSource musicSource, GlobalSfxSource, UserSfxSource;

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicBg, x => x.AudioName == name);
        if ((s == null || musicSource.mute == true))
        {
            musicSource.Stop();
            return;
        }
        else
        {
            if (musicSource.enabled != false)
            {   musicSource.clip = s.clip;
            musicSource.Play(); }
            else
                return;
        }
    }
    public void PlayGlobalSFX(string name)
    {
        Sound s = Array.Find(GlobalSfxSound, x => x.AudioName == name);
        if (s == null || GlobalSfxSource.mute == true)
        {
            GlobalSfxSource.Stop();
            return;
        }
        else
        {
            if(!GlobalSfxSource.isPlaying)
                GlobalSfxSource.PlayOneShot(s.clip);
        }
    }
    public void PlayUserSFX(string name)
    {
        Sound s = Array.Find(UserSfxSound, x => x.AudioName == name);
        if (s == null || UserSfxSource.mute == true)
        {
            UserSfxSource.Stop();
            return;
        }
        else
        {
            if(!UserSfxSource.isPlaying)
                UserSfxSource.PlayOneShot(s.clip);
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "MenuScreen":
                PlayMusic("MenuScreen");
                break;
            case "lv1":
                PlayMusic("Lv1");
                break;
            case "GameComplete":
                PlayGlobalSFX("GameComplete");
                break;
            default:
                PlayMusic("MenuScreen");
                break;
        }
    } 
}
