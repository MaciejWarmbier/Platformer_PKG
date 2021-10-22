using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float xMin;
    [SerializeField] float xMax;
    [SerializeField] float moveSpeed;
    [SerializeField] Animator animator;
    [SerializeField] float killOffset;
    [SerializeField] float dieTime = 1f;
    bool isDead = false;
    bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        CheckDirection();
        if (isFacingRight)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }

        
    }

    void CheckDirection(){
        if(isFacingRight && transform.position.x >= xMax)
        {
            Flip();
        }else if(!isFacingRight && transform.position.x <= xMin)
        {
            Flip();
        }
    }

    void MoveLeft()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

    }

    void MoveRight()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            if (isUnderPlayer(other))
            {
                isDead = true;
                animator.SetBool("isDead", isDead);
                StartCoroutine(KillOnAnimationEnd());
            }
        }
    }
    bool isUnderPlayer(Collider2D player)
    {

        if (player.transform.position.y  > transform.position.y + killOffset)
        {
            return true;
        }
        return false;
    }

    IEnumerator KillOnAnimationEnd()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(dieTime);
        gameObject.SetActive(false);

    }

}
