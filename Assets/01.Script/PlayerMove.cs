using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private FixedJoystick joystick;

    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;

    private float curtime;

    private Vector3 dashDirection;

    [Range(1f, 10f)]
    public float jumppower;
    [Range(1f,10f)]
    public float speed;

    public float dashDistance;

    public float dashtime;

    public bool onGround;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float dirx = joystick.Direction.x * speed * Time.deltaTime;
        transform.Translate(dirx, 0, 0);
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
        }
    }
    public void Jump()
    {
        if(onGround)
        {
            rb.velocity = Vector2.up * jumppower;
            onGround = false;
        }
    }

    public void StartDash()
    {
        StartCoroutine(Dash());
    }
    IEnumerator Dash()
    {
        curtime = 0f;
        //�ٶ󺸴� �������� �뽬�̵�
        if(spriteRenderer.flipX)
            dashDirection = Vector2.left;
        else dashDirection = Vector2.right;

        //��� �ð��� ������� ��ðŸ� �����
        while (dashtime > curtime)
        {
            transform.Translate(dashDirection * Time.deltaTime * dashDistance);
            curtime += Time.deltaTime;
            yield return null;
        } 
    }
}
