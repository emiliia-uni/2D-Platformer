using System.Collections;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    Vector2 direction;
    public float dashForce = 10;
    public float DashTime = 0.5f;
    private bool _canJump = true;
    private Rigidbody2D rigidbody2D;
    public float speed;
    public float jumpForce = 10;
    public float maxSpeed = 10;
    public float stoppingForce = 10;
    private bool _isDashing = false;
    private Animator _animator;

    private float initXScale;

    
    public CoinComponent cm;
    
    //stworzyæ now¹ zmienna o nazwie jumpForce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        initXScale = transform.localScale.x;

       
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        MovePlayer();
        HandleMaxSpeed();
        PlayerStopping();
        if(direction.x != 0) 
        {
            _animator.SetBool("is moving", true);
        
        }
        else 
        {
            _animator.SetBool("is moving", false);

        }

        if(direction.x > 0) 
        {
            transform.localScale = new Vector3(initXScale, transform.localScale. y, transform.localScale.z);
        
        }

        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-initXScale, transform.localScale.y, transform.localScale.z);

        }

        if (rigidbody2D.linearVelocityY > 0)
        {
            _animator.SetBool("is falling", false);
            _animator.SetBool("is jumping", true);
        }
        else if(rigidbody2D.linearVelocityY < 0)
        {
            _animator.SetBool("is falling", true);
            _animator.SetBool("is jumping", false);
        }
        else
        {
            _animator.SetBool("is falling", false);
            _animator.SetBool("is jumping", false);
        }

       
    }

    private void MovePlayer()
    {
        rigidbody2D.AddForce(new Vector2(direction.x * speed, 0));
    }

    private void HandleMaxSpeed()
    {
        if (_isDashing) 
        {
            return;
        
        }

        if (rigidbody2D.linearVelocityX >= maxSpeed)
        {
            rigidbody2D.linearVelocityX = maxSpeed;
        }
        else if (rigidbody2D.linearVelocityX <= -maxSpeed)
        {
            rigidbody2D.linearVelocityX = -maxSpeed;
        }
    }

    private void PlayerStopping()
    {
        if (direction.x == 0 && rigidbody2D.linearVelocityX != 0)
        {
            rigidbody2D.AddForce(new Vector2(-rigidbody2D.linearVelocityX * stoppingForce, 0));
        }
    }

    private void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    private int _jumpCount = 0;
    public int maxJumpCount = 2;

    private void OnJump()
    {
        if (_canJump)
        {
            rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _jumpCount++;
            if(_jumpCount >= maxJumpCount) 
            {
                _canJump = false;

            }
            
        }

        
    }

    private void OnDash() 
    {
        if(_isDashing) 
        {
            return;
        
        }
        _isDashing = true;
        rigidbody2D.AddForce(new Vector2(direction.x * dashForce,0), ForceMode2D.Impulse);
        StartCoroutine(ResetDash(DashTime));

        if (direction.x != 0)
        {


        }
        else
        {
            _animator.SetBool("is jumping", false);

        }
    }
    IEnumerator ResetDash(float timeToReset) 
    {
        yield return new WaitForSeconds(timeToReset);
        _isDashing = false;
    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _canJump = true;
        _jumpCount = 0;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin")) 
        {
            cm.coinCount++;
            Destroy(collision.gameObject);
            Debug.Log("Coin");

        }

    }


    
    }

    






