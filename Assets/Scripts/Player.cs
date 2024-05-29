using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerTeam
{
    Red,
    Blue
}

public class Player : MonoBehaviour
{
    public int moveSpeed = 1;  
    private int xPosition = -1;
    private int yPosition = -1;

    private int targetXPos = -1;
    private int targetYPos = -1;
    private float targetHeight = -1;

    private bool moving = false;

    public PlayerTeam team;

    public PlayerTeam getTeam()
    {
        return team;
    }

    public void setPos(int x, int y, float height)
    {
        // Rotate the player to face the direction of movement
        var targetVector = new Vector3(x, 0, y);
        var currentVector = new Vector3(xPosition, 0, yPosition);
        var direction = targetVector - currentVector;
        if (direction.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        xPosition = x;
        yPosition = y;
        targetHeight = height;
        targetXPos = x;
        targetYPos = y;   

        float currentHeight = this.transform.position.y;
        var animator = this.GetComponent<Animator>();
        if (targetHeight != currentHeight)
        {
            // Set animator bool isJumping to true
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }
        moving = true;

    }

    public int getX()
    {
        return xPosition;
    }

    public int getY()
    {
        return yPosition;
    }
    // Start is called before the first frame update
    void Start()
    {
        var animator = this.GetComponent<Animator>();
        animator.SetBool("isWalking", false);
        animator.SetBool("isJumping", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moving)
        {
            float step = moveSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetXPos, targetHeight, targetYPos), step);

            if ((transform.position - new Vector3(targetXPos, targetHeight, targetYPos)).magnitude < 0.001f)
            {
                moving = false;
                var animator = this.GetComponent<Animator>();
                animator.SetBool("isWalking", false);
                animator.SetBool("isJumping", false);
            }
        }
    }
}
