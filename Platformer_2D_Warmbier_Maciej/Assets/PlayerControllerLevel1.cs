using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerLevel1 : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.5f; 
    [SerializeField] float jumpForce = 2f;

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
        if(Input.GetKey(KeyCode.RightArrow)){
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.LeftArrow)){
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }

        if(Input.GetMouseButtonDown(0)){
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
