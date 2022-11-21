using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    Vector2 movement = new Vector2();
    Rigidbody2D rb2d;

    public float speed = 3.0f;

    Animator animator;
    string animationState = "AnimationState";
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    enum CharStates{
        walkEast = 1,
        walkSouth = 2,
        walkWest = 3,
        walkNorth = 4,
        idleSouth = 5
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        
    }

        private void UpdateState()
    {
        if (movement.x > 0) {
            animator.SetInteger(animationState, (int)CharStates.walkEast);   
        }else if (movement.x < 0) {
            animator.SetInteger(animationState, (int)CharStates.walkWest);   
        }else if (movement.y > 0) {
            animator.SetInteger(animationState, (int)CharStates.walkNorth);   
        }else if (movement.y < 0) {
            animator.SetInteger(animationState, (int)CharStates.walkSouth);   
        }else{
            animator.SetInteger(animationState, (int)CharStates.idleSouth);   
        }
    }

    private void MoveCharacter()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        movement.Normalize();
        rb2d.velocity = movement * speed;
    }

}
