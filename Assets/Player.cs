using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController2D controller;
    float horizontalMove=0f;
    public float runSpeed=40f;

    public Rigidbody2D rb;
    private float DashTime;
    public float DashSpeed;
    public float StartDashTime;
    private int direction;

    private bool dashCoolDown = false;
    private bool Jump=false;
    private bool Crouch=false;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        DashTime=StartDashTime;
    }

    // Update is called once per frame
    void Update()
    //BASIC INPUT
    {
     horizontalMove=Input.GetAxisRaw("Horizontal")*runSpeed;

     if(Input.GetButtonDown("Jump"))
     {
      Jump=true;

     }else if(Input.GetButtonUp("Jump"))
     {
      Jump=false;
     }

     if(Input.GetButtonDown("Crouch"))
     {
      Crouch=true;
     }else if(Input.GetButtonUp("Crouch"))
     {
      Crouch=false;
     }
    //DASHING 
    if(direction==0){
        if(Input.GetKey(KeyCode.LeftShift )&& Input.GetKey(KeyCode.A) && dashCoolDown == false) {
            direction = 1;
            Invoke("DashCoolDown", 2);
            dashCoolDown = true;
        } else if(Input.GetKey(KeyCode.LeftShift)&&Input.GetKey(KeyCode.D) && dashCoolDown == false) {
            direction = 2;
            Invoke("DashCoolDown", 2);
            dashCoolDown = true;
        }
    } else {
        if(DashTime<=0)
        {
            direction = 0;
            DashTime=StartDashTime;
            rb.velocity=Vector2.zero;
        }else
        {
          DashTime -= Time.deltaTime;

          if(direction==1)  
          {
              rb.velocity = Vector2.left *DashSpeed;
          }else if (direction ==2)
          {
              rb.velocity = Vector2.right *DashSpeed;
          }
        }
    }
        
        
    }
    

    void FixedUpdate()
    {
     controller.Move(horizontalMove*Time.fixedDeltaTime,Crouch,Jump);
    }
    //POWER UP: RUN SPEED
    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag == "SpeedBoost")
        {
         Debug.Log("xD");
         Destroy(collision.gameObject);
         runSpeed=runSpeed+ 20f;
		 StartCoroutine(ResetPower());
         
        }
    }
		private IEnumerator ResetPower()
	{
		yield return new WaitForSeconds(10f);
		runSpeed=40f;
    }

    private void DashCoolDown(){
        dashCoolDown = false;
    }
}