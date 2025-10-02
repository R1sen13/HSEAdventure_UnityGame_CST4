using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public Rigidbody2D rb;
	public float ja = 35;
	public float gs = 9.8f;
	public float fgs = 40;
	private bool isGround = true;
	public float speed;
	public Text score;

	private int sc = 0;
	public GameObject over;
	public GameObject win;

	void Start(){
		score.text = $"{sc}";
		win.SetActive(false);
		over.SetActive(false);	

	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			Jump();		
		}
		else if (Input.GetKey(KeyCode.D)){
			transform.position += new Vector3(1, 0, 0)*(speed*Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.A)){
			transform.position += new Vector3(-1, 0, 0)*(speed*Time.deltaTime);
		}
	}

	private void Jump(){
		if(isGround){
			rb.AddForce(Vector2.up * ja, ForceMode2D.Impulse);
			if(rb.linearVelocity.y >= 0){
				rb.gravityScale = gs;
			}
			else if(rb.linearVelocity.y < 0){
				rb.gravityScale = fgs;
			}
		}
	}
    
}
