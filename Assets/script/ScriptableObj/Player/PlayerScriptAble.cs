using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character Datas")]
public class PlayerScriptAble : ScriptableObject
{
    public int StartUpMana;
    public int maxHp;
    public int maxMana;
    public int maxWeaponLv;
    public int Live;
}
