using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ForestSlime_States
{
    Move,
    Chase,
    Attack,
    Hit,
    Dead
}
public class ForestSlime : Monster
{

    private SpriteRenderer SpriteRenderer;

    public ForestSlime_States currentState;

    private RaycastHit2D hit;

    private RaycastHit2D attack_range;

    private int Player_Layer = 1 << 6;

    private Vector2 lookAt = Vector2.right;

    private float chaseRange;

    private float attackRange;

    private Coroutine attack_Coroutine;

    private float attack_time;

    private Coroutine reversalCoroutine;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.flipX = true;
        StatsSetting(300, 150, 75, 1, 0, 25);

        chaseRange = range * 10;

        attackRange = range;
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, lookAt * chaseRange, Color.red);
        Debug.DrawRay(transform.position, lookAt * attackRange, Color.blue);

        switch (currentState)
        {
            case ForestSlime_States.Move:
                Movement();
                break;

            case ForestSlime_States.Chase:
                Chase();
                break;

            case ForestSlime_States.Attack:
                Attack();
                break;
        }
        
    }
    void Movement()
    {
        transform.Translate(lookAt * movement_speed  * 0.5f * Time.deltaTime);

        hit = Physics2D.Raycast(transform.position, lookAt, chaseRange, Player_Layer);

        if (hit) //감지 범위 안이면 플레이어 추격
        {
            if(reversalCoroutine != null) // 감지 시작시 회전하는 코루틴 멈춤
            {
                StopCoroutine(reversalCoroutine);
                reversalCoroutine = null;
            }
            currentState = ForestSlime_States.Chase;
        }

        if(reversalCoroutine == null) // 움직일때 5초마다 회전
            reversalCoroutine = StartCoroutine(Reversal());
    }

    void Chase()
    {
        hit = Physics2D.Raycast(transform.position, lookAt, chaseRange, Player_Layer);
        attack_range = Physics2D.Raycast(transform.position, lookAt, attackRange , Player_Layer);

        transform.Translate(lookAt * movement_speed * Time.deltaTime);

        if(attack_range)
        {
            currentState = ForestSlime_States.Attack;
        }
        else if (!hit)
        {
            currentState = ForestSlime_States.Move;
        }
    }

    void Attack()
    {
        if(attack_Coroutine == null)
            attack_Coroutine = StartCoroutine(SlimeAttack());
    }

    IEnumerator SlimeAttack()
    {
        yield return new WaitForSeconds(attack_speed * 0.5f);

        attack_time = 0f;

        while (attack_time < 0.25f)
        {
            attack_time += Time.deltaTime;

            float moveDistance = range * 3f * Time.deltaTime * movement_speed;

            if (lookAt == Vector2.left)
            {
                transform.Translate(-moveDistance, 0, 0);
            }
            else
            {
                transform.Translate(moveDistance, 0, 0);
            }

            yield return null;
        }

        yield return new WaitForSeconds(0.75f);
        currentState = ForestSlime_States.Move;
        attack_Coroutine = null;

    }

    void turn()
    {       
        if(SpriteRenderer.flipX)
        {
            SpriteRenderer.flipX = false;
            lookAt = Vector2.left;
        }
        
        else
        {
            SpriteRenderer.flipX = true;
            lookAt = Vector2.right;
        }
            

    }
    IEnumerator Reversal()
    {
        while (currentState == ForestSlime_States.Move)
        {
            yield return new WaitForSeconds(5f);
            turn();
        }
    }
}
