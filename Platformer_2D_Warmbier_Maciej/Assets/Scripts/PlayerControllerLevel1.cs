using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerLevel1 : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.5f; 
    [SerializeField] float jumpForce = 2f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Animator animator;
    [SerializeField] GameObject respawn;
    [SerializeField] float killOffset;
    [SerializeField] int maxKeyNumber;
    bool isWalking = false;
    bool isGrounded = true;
    bool isFalling = false;
    bool isFacingRight = true;
    
    Rigidbody2D rb;
    
        private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(GameManager.instance.currentGameState == GameState.GS_GAME)
        {
        isWalking = false;
        isGrounded = IsGrounded();
        isFalling = IsFalling();

        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
            MoveRight();
        }
        else if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
            MoveLeft();
        }

        
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)){
           Jump();
        }
        animator.SetBool("isFalling", isFalling);
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isGrounded",isGrounded);
        }
    }

    void MoveLeft()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        if (isFacingRight)
        {
            Flip();
        }
        isWalking = true;
    }

    void MoveRight()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        if (!isFacingRight)
        {
            Flip();
        }
        isWalking = true;
    }

    void Jump(){
        
        
        if(isGrounded){
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
        }
    }

    bool IsGrounded(){
        Debug.DrawRay(transform.position, Vector2.down*0.2f);
        return Physics2D.Raycast(transform.position, Vector2.down, 0.2f, groundLayer.value);
    }

    bool IsFalling(){
        if(rb.velocity.y < 0 && !isGrounded){
            
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
            GameManager.instance.AddCoins(1);
            other.gameObject.SetActive(false);
        }

        if(other.CompareTag("End")){
            //if(keys == maxKeyNumber)
           // {
            //    Debug.Log("YOU WON!!!!");
           // }
            //else
           // {
            //    Debug.Log("You need more keys");
            //}
            
        }

        if(other.CompareTag("Key")){
            GameManager.instance.AddKeys();
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Enemy"))
        {
            if (isOnEnemy(other)) {
                GameManager.instance.AddEnemies();
            }
            else
            {
                GameManager.instance.DepleteHearts();
                Respawn();
            }
        }

        if (other.CompareTag("Heart"))
        {
            GameManager.instance.AddHearts();
            other.gameObject.SetActive(false);

        }
    }

    bool isOnEnemy(Collider2D enemy)
    {
        
        if(enemy.transform.position.y+ killOffset < transform.position.y)
        {
            return true;
        }
        return false;
    }

    void Respawn()
    {
        transform.position = respawn.transform.position;
    }
}
