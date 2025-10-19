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

    public GameObject barrier;

	public Text score;
	private int sc = 0;
	private int jumps = 0;

	public float groundCheckDistance = 0.1f;
    
    public float acceleration = 15f;
    public float deceleration = 20f;
    public float airControl = 0.8f;

	private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isGrounded;
    private bool isRunning;
    private bool isFacingRight = true;

    private bool isDeath = false;

    private float horizontalInput;
    private float currentSpeed;

    private bool end = false;

    public static bool isPaused;

    public AudioSource death,jump,coin,crow, background, enemy, alert, music, fanfars;

    IEnumerator ReloadAfterWait() {
    	yield return new WaitForSeconds(3.5f);
    
    	SceneManager.LoadScene("Game");
	}

	IEnumerator NextLevelAfterWait() {
    	yield return new WaitForSeconds(6.5f);
    
    	SceneManager.LoadScene("Game");
	}

	void Start(){
		score.text = $"{sc}";

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
		if(!isPaused){
			death.UnPause();
			background.UnPause();
			music.UnPause();
			fanfars.UnPause();
			if(!isDeath && !end){
			    GetInput();
			    HandleAnimations();
			    HandleJumpInput();
			}
			if(end && gameObject.transform.position.x <= 26378){
				animator.SetFloat("movex", 100);
				animator.SetBool("isRunning",false);
				animator.SetFloat("movey",0);
				rb.transform.position += new Vector3(1,0,0) * (200*Time.deltaTime);
			}
	   	}
	   	else{
	   		background.Pause();
			death.Pause();
			music.Pause();
			fanfars.Pause();
		}
	}

	void FixedUpdate()
	{
		if(!isPaused){
			death.UnPause();
			background.UnPause();
			music.UnPause();
			fanfars.UnPause();
			if(!isDeath && !end){
			    MoveCharacter();
			    HandleJumpPhysics();
			}
		}
		else{
			background.Pause();
			death.Pause();
			music.Pause();
			fanfars.Pause();
		}
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
	            PerformJump(jumpForce);
	            jumps=1;
	        }
	        else if (jumps==1)
	        {
	            PerformJump(jumpForce * 0.9f);
	            jumps = 2;
	        }
	    }
	    // Переменная высота прыжка (отпускание кнопки)
	    if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0)
	    {
	        rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
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
	    if(!isDeath){
		    jump.Play();
			crow.Play();
		}
	}

	void HandleAnimations()
	{
	    if (animator != null)
	    {
	        animator.SetFloat("movex", (int)Mathf.Abs(currentSpeed));
		    if(Mathf.Abs(horizontalInput)>0 && isRunning){
				animator.SetBool("isRunning",true);
			}
			else{
				animator.SetBool("isRunning",false);
			}
	        animator.SetFloat("movey", Mathf.Abs(rb.linearVelocity.y));
	    }
	}

	private void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.CompareTag("end")){
			end = true;
			walkSpeed = 0;
			music.Stop();
			fanfars.Play();
			StartCoroutine(NextLevelAfterWait());
		}

		if(col.gameObject.CompareTag("enemytrigger") && !isDeath){
			rb.linearVelocity = new Vector2(rb.linearVelocity.x, 775);
			Destroy(col.transform.parent.gameObject);
			enemy.Play();
		}
		if(col.gameObject.CompareTag("ground")){
			jumps = 0;
		}
		if(col.gameObject.CompareTag("credit")){
			Destroy(col.gameObject);
			coin.Play();
			sc+=1;
			score.text = $"{sc}";
			if(sc == 52){
		    	alert.Play();

		    	barrier.SetActive(false);
		    	Destroy(barrier);
		    	background.Stop();
		    	music.Play();
		    }
		}
	}
	private void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.CompareTag("ground")){
			jumps = 0;
		}
		if(col.gameObject.CompareTag("death_platform") || col.gameObject.CompareTag("enemy")){
			if(!isDeath){
				background.Stop();
				music.Stop();
				death.Play();

				animator.SetBool("isDeath",true);
				if(col.gameObject.CompareTag("enemy")){
					Collider2D first = gameObject.GetComponent<Collider2D> ();
					Collider2D second = col.gameObject.GetComponent<Collider2D> ();
				    Physics2D.IgnoreCollision(first, second, true);
				    Physics2D.queriesHitTriggers = false;
				}
				runSpeed = 0;
				walkSpeed = 0;
				StartCoroutine(ReloadAfterWait());
			}
			isDeath = true;
		}
	}
}
