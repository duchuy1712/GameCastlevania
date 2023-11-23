using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataGame : Singleton<DataGame>
{
    public GlobalData globaldata;
    public UserData userdata;
    public void resetLevel()
    {
        PlayerPrefs.DeleteKey(GlobalDataKey.Level);
        PlayerPrefs.DeleteKey(UserDataKey.Live);
        PlayerPrefs.DeleteKey(UserDataKey.mana);
        PlayerPrefs.DeleteKey(UserDataKey.mainWeapon);
        PlayerPrefs.DeleteKey(UserDataKey.subWeapon);
        PlayerPrefs.DeleteKey(UserDataKey.score);
    }
}
