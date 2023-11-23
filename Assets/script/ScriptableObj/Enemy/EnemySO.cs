using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy Datas")]
public class EnemySO : ScriptableObject
{
    private void OnEnable()
    {
        Physics2D.IgnoreLayerCollision(9, 9, true);
    }
    public int hp;
    public int point;
    public int damge;
    public float StunTime;
}
