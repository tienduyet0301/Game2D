 using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
   public float speed = 5f;
   public float jumpSpeed = 8f;
   private float direction = 0f; // hướng di chuyển
   private Rigidbody2D player;

    public Animator animator;

   public Transform groundCheck; // check nhân vật tiếp đất hay không
   public float groundCheckRadius; // vùng kiểm tra
   public LayerMask groundLayer;
   private bool isTouchingGround;
   private bool facingRight=true;
   private int countCoin = 0;
   public TMP_Text txtCoin;
   public AudioSource soundCoin;
//Khai bao cac bien de ban
   
   public Transform gunTip;
   public GameObject bullet;
   float fireRate = 0.5f;
   float nextFire = 0;

    void Start()
   {
       player = GetComponent<Rigidbody2D>();
        
            
        
    }
   void Update()
   {
    // Lấy giá trị nhập từ bàn phím để xác định hướng di chuyển
       isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
       direction = Input.GetAxisRaw("Horizontal"); 

        animator.SetFloat("Speed", Mathf.Abs(direction)); // Cập nhật giá trị tốc độ cho Animator để thay đổi hoạt ảnh đi bộ.

       if (direction > 0f) 
       {
           player.velocity = new Vector2(direction * speed, player.velocity.y);
       }
       else if (direction < 0f)
       {
           player.velocity = new Vector2(direction * speed, player.velocity.y);
       }
       else
       {
           player.velocity = new Vector2(0, player.velocity.y);
       }

       if (Input.GetButtonDown("Jump") && isTouchingGround) // kiểm tra có đang thực hiện phím nhảy khi nhân vật đang tiếp đất hay không
       {
           player.velocity = new Vector2(player.velocity.x, jumpSpeed);
            animator.SetBool("isJumping", true);
       }
       //Chuc nang ban tu ban phim
       if(Input.GetAxisRaw("Fire1")>0)
            fireBullet ();
   }
    private void FixedUpdate()
   {
        if(!facingRight && direction > 0)
       {
           Flip();
       }else if(facingRight && direction < 0)
       {
           Flip();
       }
   }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false); // Cập nhật trạng thái nhảy trong Animator khi nhân vật tiếp đất.
    }

   void Flip()
   {
       facingRight = !facingRight;
       Vector2 currentScale = transform.localScale;
       currentScale.x *= -1;
       transform.localScale = currentScale;
   }
   private void OnTriggerEnter2D(Collider2D collision)
   {
    if(collision.gameObject.tag == "coin")
    {
        soundCoin.Play();
        countCoin += 1;
        txtCoin.text = countCoin + " x";
        Destroy(collision.gameObject);
    }
   }
   //Chuc nang ban
        void fireBullet(){
            if(Time.time > nextFire) {
                nextFire = Time.time + fireRate;
                if(facingRight) {
                    Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0,0,0)));
                } else if (!facingRight){
                  Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0,0,180)));

                }
            }
        }
}