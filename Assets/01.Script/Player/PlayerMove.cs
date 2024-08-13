using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private FixedJoystick joystick;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private PlayerAnimation playerAnimation;

    private float inputDelay = 0.2f;
    private bool jumpInputCooldown;
    [Range(1f, 10f)]
    public float jumpPower;
    public int extraJumpCount = 1;

    [Range(1f,10f)]
    public float playerMoveSpeed;

    private Vector3 dashDirection;
    public float dashDistance;
    private float dashCurTime;
    public float dashTime;
    public float dashCoolTime;
    private float dashCurCoolTime;
    private bool canDash = true;

    public bool onGround;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void FixedUpdate()
    {
        float dirx = joystick.Direction.x * playerMoveSpeed * Time.deltaTime;
        transform.Translate(dirx, 0, 0);

        //Move �ִϸ��̼� ����
        if (dirx == 0)
            playerAnimation.currentState = States.Idle;
        else
            playerAnimation.currentState = States.Move;
        

        //�ٶ󺸴� ���� ����
        switch(joystick.Direction.x)
        {
            case 1:
                spriteRenderer.flipX = false;
                break;
            case -1:
                spriteRenderer.flipX = true;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            onGround = true;
            extraJumpCount = 1;
        }
    }
    public void Jump()
    {
        if (jumpInputCooldown)
            return;
        //2�� ���� ����
        switch(onGround)
        {
            case true:
                onGround = false;
                break;
            case false:
                if (extraJumpCount == 0)
                    return;
                else
                    extraJumpCount--;
                break;
        }
        rb.velocity = Vector2.up * jumpPower;
        StartCoroutine(JumpInputDelay());
    }
    //�������� ���� ��Ÿ��0.2��
    IEnumerator JumpInputDelay()
    {
        jumpInputCooldown = true;
        yield return new WaitForSeconds(inputDelay);
        jumpInputCooldown = false;

    }

    public void StartDash()
    {
        if(canDash)
        {
            StartCoroutine(Dash());
            StartCoroutine(DashCoolDown());
        } 
    }
    IEnumerator Dash()
    {
        dashCurTime = 0f;
        //�ٶ󺸴� �������� �뽬�̵�
        if(spriteRenderer.flipX)
            dashDirection = Vector2.left;
        else dashDirection = Vector2.right;

        //��� �ð��� ������� ��ðŸ� �����
        while (dashTime > dashCurTime)
        {
            transform.Translate(dashDirection * Time.deltaTime * dashDistance);
            dashCurTime += Time.deltaTime;
            yield return null;
        } 
    }
    //�뽬 ��Ÿ��
    IEnumerator DashCoolDown()
    {
        canDash = false;
        dashCurCoolTime = dashCoolTime;
        while(dashCurCoolTime > 0f)
        {
            dashCurCoolTime -= Time.deltaTime;
            yield return null;
        }
        canDash = true;
    }
}
