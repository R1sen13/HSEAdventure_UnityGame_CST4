using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 12f;
    public float gravityScale = 3f;

	public Text score;
	private int sc = 0;
	private int jumps = 0;
	public GameObject over;
	public GameObject win;

	public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;
    public Transform groundCheckPoint;
    
    public float acceleration = 15f;
    public float deceleration = 20f;
    public float airControl = 0.8f;

	private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isGrounded;
    private bool isRunning;
    private bool isFacingRight = true;

    private float horizontalInput;
    private float currentSpeed;

	void Start(){
		score.text = $"{sc}";
		win.SetActive(false);
		over.SetActive(false);	

	    rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        SetupPhysics();
    }
    
    void SetupPhysics()
    {
        rb.gravityScale = gravityScale;
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

	void Update()
	{
	    GetInput();
	    HandleAnimations();
	    HandleJumpInput();
	}

	void FixedUpdate()
	{
	    MoveCharacter();
	    HandleJumpPhysics();
	}

	void GetInput()
	{
	    horizontalInput = Input.GetAxisRaw("Horizontal");
	    isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
	}

	void MoveCharacter()
	{
	    float targetSpeed = horizontalInput * (isRunning ? runSpeed : walkSpeed);
	    
	    // Плавное ускорение и торможение
	    float accelerate = Mathf.Abs(targetSpeed) > 0.1f ? acceleration : deceleration;
	    
	    // Меньший контроль в воздухе
	    if (!isGrounded)
	        accelerate *= airControl;
	    
	    // Плавное изменение скорости
	    currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, accelerate * Time.fixedDeltaTime);
	    
	    // Применяем скорость (только по X)
	    rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y);
	    if(Mathf.Abs(horizontalInput)>0 && isRunning){
		    	animator.SetBool("isRunning",true);
		}
		else{
			animator.SetBool("isRunning",false);
		}
		animator.SetFloat("movex", Mathf.Abs(horizontalInput));

	    // Поворот спрайта
	    if (Mathf.Abs(horizontalInput) > 0.1f)
	    {
	        isFacingRight = horizontalInput > 0;
	        spriteRenderer.flipX = !isFacingRight;
	    }
	}

	void HandleJumpInput()
	{
	    // Начало прыжка
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
	        if (jumps == 0)
	        {
	        	animator.SetBool("isJumping", true);
	            PerformJump(jumpForce);
	            jumps=1;
	        }
	        else if (jumps==1)
	        {
	        	animator.SetBool("isJumping", true);
	            PerformJump(jumpForce * 0.9f);
	            jumps = 2;
	        }
	    }
	    if (Input.GetKeyUp(KeyCode.Space)){
			animator.SetBool("isJumping", false);
	    }
	    // Переменная высота прыжка (отпускание кнопки)
	    if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0)
	    {
	        rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
	    }
	}

	private void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.CompareTag("ground")){
			jumps = 0;
		}
	}


	void HandleJumpPhysics()
	{
	    // Усиленная гравитация при падении
	    if (rb.linearVelocity.y < 0)
	    {
	        rb.gravityScale = gravityScale * 1.5f;
	    }
	    else
	    {
	        rb.gravityScale = gravityScale;
	    }
	}

	void PerformJump(float force)
	{
	    rb.linearVelocity = new Vector2(rb.linearVelocity.x, force);
	    
	    // Можно добавить звук прыжка
	    // AudioManager.Instance.PlaySound("jump");
	}

	void HandleAnimations()
	{
	    if (animator != null)
	    {
	        animator.SetFloat("Speed", Mathf.Abs(currentSpeed));
	        animator.SetBool("IsRunning", isRunning);
	        animator.SetBool("IsGrounded", isGrounded);
	        animator.SetFloat("VerticalVelocity", rb.linearVelocity.y);
	    }
	}


	void OnDrawGizmosSelected()
	{
	    if (groundCheckPoint != null)
	    {
	        Gizmos.color = isGrounded ? Color.green : Color.red;
	        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckDistance);
	    }
	}

	void HandleSkidAnimation()
	{
	    if (isGrounded && Mathf.Sign(horizontalInput) != Mathf.Sign(currentSpeed) && Mathf.Abs(currentSpeed) > 1f)
	    {
	        animator.SetBool("IsSkidding", true);
	    }
	    else
	    {
	        animator.SetBool("IsSkidding", false);
	    }
	}

	private void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.CompareTag("credit")){
			Destroy(col.gameObject);
			sc+=1;
			score.text = $"{sc}";
		}
	}

}
