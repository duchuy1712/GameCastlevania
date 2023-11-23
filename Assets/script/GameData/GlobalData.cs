using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
    public int Level
    {
        get => PlayerPrefs.GetInt(GlobalDataKey.Level, 1);
        set => PlayerPrefs.SetInt(GlobalDataKey.Level, value);
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
    public const string Level = "Level";
    public const string Music = "Music";
    public const string Sfx = "Sfx";
}
