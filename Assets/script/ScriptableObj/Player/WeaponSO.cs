using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Character", menuName = "Weapon Datas")]
public class WeaponSO : ScriptableObject
{
    public float speed;
    public int damage;
    public int ManaCost;
}
