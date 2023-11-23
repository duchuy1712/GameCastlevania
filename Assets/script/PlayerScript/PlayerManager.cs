using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public PlayerMove ConTrol;
    public PlayerAnim Animation;
    public PlayerAttackControl AttackControl;
    public PlayerData Stat;
}
