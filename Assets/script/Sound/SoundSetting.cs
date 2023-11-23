using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    [SerializeField] private Button MusicButton;
    [SerializeField] private Button SfxButton;
    [SerializeField] private GameObject MusicOff;
    [SerializeField] private GameObject SfxOff;
    private void Awake()
    {
        MusicOff.SetActive(!DataGame.Instance.globaldata.Music);
        SfxOff.SetActive(!DataGame.Instance.globaldata.Sfx);
        MusicButton.onClick.AddListener(Music);
        SfxButton.onClick.AddListener(Sfx);
    }
    private void Music()
    {
        AudioManager.Instance.PlayGlobalSFX("ButtonClick");
        if (DataGame.Instance.globaldata.Music == false)
            DataGame.Instance.globaldata.Music = true;
        else
            DataGame.Instance.globaldata.Music = false;
        MusicOff.SetActive(!DataGame.Instance.globaldata.Music);
        AudioManager.Instance.musicSource.mute = !DataGame.Instance.globaldata.Music;
        Debug.Log(DataGame.Instance.globaldata.Music.ToString());
    }
    private void Sfx()
    {
        AudioManager.Instance.PlayGlobalSFX("ButtonClick");
        if (DataGame.Instance.globaldata.Sfx == true)
            DataGame.Instance.globaldata.Sfx = false;
        else
            DataGame.Instance.globaldata.Sfx = true;
        SfxOff.SetActive(!DataGame.Instance.globaldata.Sfx);
        AudioManager.Instance.GlobalSfxSource.mute = !DataGame.Instance.globaldata.Sfx;
        AudioManager.Instance.UserSfxSource.mute = !DataGame.Instance.globaldata.Sfx;
    }
}
