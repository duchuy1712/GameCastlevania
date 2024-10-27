using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalData : MonoBehaviour
{
    public int CurrentLevel
    {
        get => PlayerPrefs.GetInt(GlobalDataKey.CurrentLevel, SceneManager.GetSceneByName("Level1").buildIndex);
        set => PlayerPrefs.SetInt(GlobalDataKey.CurrentLevel, value);
    }
    public bool Music
    {
        get => PlayerPrefs.GetInt(GlobalDataKey.Music) == 0? true:false;
        set => PlayerPrefs.SetInt(GlobalDataKey.Music, Music ? 1:0);
    }
    public bool Sfx
    {
        get => PlayerPrefs.GetInt(GlobalDataKey.Sfx) == 0? true:false;
        set => PlayerPrefs.SetInt(GlobalDataKey.Sfx, Sfx ? 1:0);
    }
}
public struct GlobalDataKey
{
    public const string CurrentLevel = "CurrentLevel";
    public const string Music = "Music";
    public const string Sfx = "Sfx";
}
