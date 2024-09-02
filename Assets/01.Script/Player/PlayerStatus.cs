using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus instance;

    public int maxHP;
    public int curHP;

    public int maxMP;
    public int curMP;

    public int def;

    [Range(1f, 10f)]
    public float jumpPower;
    public int extraJumpCount = 1;

    public float dashDistance;
    public float dashTime;
    public float dashCoolTime;

}
