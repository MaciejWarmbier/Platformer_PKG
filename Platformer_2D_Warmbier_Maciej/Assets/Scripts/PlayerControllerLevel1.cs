using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerLevel1 : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.5f; 
    [SerializeField] float jumpForce = 2f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Animator animator;
    int score = 0;
    bool isWalking = false;
    bool isGrounded = true;
    bool isFalling = false;
    bool isFacingRight = true;
    Rigidbody2D rb;
    
    // Start is called before the first frame update
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isWalking = false;
        isGrounded = IsGrounded();
        isFalling = IsFalling();

        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            if(!isFacingRight){
                Flip();
            }
            isWalking= true;
        }
        else if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            if(isFacingRight){
                Flip();
            }
            isWalking = true;
        }

        
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)){
           Jump();
        }
        animator.SetBool("isFalling", isFalling);
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isGrounded",isGrounded);
    }

    void Jump(){
        
        
        if(isGrounded){
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("Jumping");
        }
    }

    bool IsGrounded(){
        return Physics2D.Raycast(transform.position, Vector2.down, 0.2f, groundLayer.value);
    }

    bool IsFalling(){
        if(rb.velocity.y < 0 && !isGrounded){
            Debug.Log("Falling");
            return true;
            
        }
        return false;
    }

    void Flip(){
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Cristal")){
            score++;
            Debug.Log("Score = " +score);
            other.gameObject.SetActive(false);
        }

        if(other.CompareTag("End")){
            Debug.Log("YOU WON!!!!");
        }

        if(other.CompareTag("Ametyst")){
            score+=10;
            Debug.Log("Score = " +score);
            other.gameObject.SetActive(false);
        }
    }
}
