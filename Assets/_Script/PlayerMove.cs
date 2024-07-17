using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private FixedJoystick joystick;

    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;

    [Range(0f, 10f)]
    public float jumppower;
    [Range(0f,10f)]
    public float speed;

    public bool onGround;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        float dirx = joystick.Direction.x * speed * Time.deltaTime;
        transform.Translate(dirx, 0, 0);
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
    void Jump()
    {
        rb.velocity = Vector2.up * jumppower;
    }
    
}
