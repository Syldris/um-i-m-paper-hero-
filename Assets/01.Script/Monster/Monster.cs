using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float current_hp;
    public float hp;
    public float attack;
    public float movement_speed;
    public float attack_speed;
    public float defense;
    public float range;

    protected float value = 0.05f; //이동속도,사거리 보정값

    public static GameObject Player;

    private void Awake()
    {
        Player = GameObject.Find("Player");
    }

    public void StatsSetting(float Hp,float Atk, float Move_Speed, float Atk_Speed, float def, float Range)
    {
        hp = Hp;
        attack = Atk;
        movement_speed = Move_Speed * value;
        attack_speed = Atk_Speed;
        defense = def;
        range = Range * value;
    }
}
