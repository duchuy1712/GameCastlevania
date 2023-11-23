using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    [SerializeField] PlayerScriptAble BaseStat;
   public int Live
   {
        get => PlayerPrefs.GetInt(UserDataKey.Live, BaseStat.Live);
        set => PlayerPrefs.SetInt(UserDataKey.Live, value);
   }
    public int mana
    {
        get => PlayerPrefs.GetInt(UserDataKey.mana,BaseStat.StartUpMana);
        set => PlayerPrefs.SetInt(UserDataKey.mana,value);
    }
    public int mainWeaponLv
    {
        get => PlayerPrefs.GetInt(UserDataKey.mainWeapon,0);
        set => PlayerPrefs.SetInt(UserDataKey.mainWeapon, value);
    }
    public int subWeapon
    {
        get => PlayerPrefs.GetInt(UserDataKey.subWeapon, 0);
        set => PlayerPrefs.SetInt(UserDataKey.subWeapon, value);
    }
    public int score
    {
        get => PlayerPrefs.GetInt(UserDataKey.score, 0);
        set => PlayerPrefs.SetInt(UserDataKey.score, value);
    }
    public int highscore
    {
        get => PlayerPrefs.GetInt(UserDataKey.highScore, 0);
        set => PlayerPrefs.SetInt(UserDataKey.highScore, value);
    }
}
public struct UserDataKey
{
    public const string Live = "Live";
    public const string mana = "mana";
    public const string subWeapon = "subWeapon";
    public const string mainWeapon = "mainWeapon";
    public const string score = "score";
    public const string highScore = "highScore";
}