using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Level2EnemyScript : MovingObject
{

    public int StartingPathIndex;
    public Sprite enemySpriteUp;
    public Sprite enemySpriteDown;
    public Sprite enemySpriteLeft;
    public Sprite enemySpriteRight;

    private SpriteRenderer spriteRenderer;

    // Deterministic path of the enemies for Level 2 that is repeated
    private Direction[] path = new Direction[] { Direction.UP, Direction.LEFT, Direction.UP, Direction.UP, Direction.LEFT, Direction.LEFT, Direction.DOWN, Direction.DOWN, Direction.DOWN, Direction.DOWN, Direction.DOWN, Direction.DOWN, Direction.RIGHT, Direction.RIGHT, Direction.UP, Direction.UP, Direction.RIGHT, Direction.UP };
    // Current index to understand next direction for enemy
    private int currentPathIndex;

    protected override void Start()
    {
        currentPathIndex = StartingPathIndex;
        spriteRenderer = GetComponent<SpriteRenderer>();
        base.Start();
    }

    private void Update()
    {
        // See if the enemy is done moving, and then move to the next location
        if (DoneMoving)
        {
            MoveNextLocation();
        }
    }

    private void MoveNextLocation()
    {
        // Switch based on the next direction to travel
        switch (path[currentPathIndex])
        {
            case Direction.UP:
                ChangePlayerDirection(0, 1);
                AttemptMove<Door>(0, 1);
                break;
            case Direction.DOWN:
                ChangePlayerDirection(0, -1);
                AttemptMove<Door>(0, -1);
                break;
            case Direction.LEFT:
                ChangePlayerDirection(-1, 0);
                AttemptMove<Door>(-1, 0);
                break;
            case Direction.RIGHT:
                ChangePlayerDirection(1, 0);
                AttemptMove<Door>(1, 0);
                break;
            default:
                break;
        }
        // Increment current path index to next direction
        currentPathIndex = (currentPathIndex + 1) % path.Length;
    }

    // Updates the sprite based on facing direction
    private void ChangePlayerDirection(int horizontal, int vertical)
    {
        // Only move if input in either horizontal or vertical
        if (horizontal != 0)
        {
            // Facing Right
            if (horizontal > 0)
            {
                spriteRenderer.sprite = enemySpriteRight;
            }
            // Facing Left
            else if (horizontal < 0)
            {
                spriteRenderer.sprite = enemySpriteLeft;
            }
        }
        else if (vertical != 0)
        {
            // Facing Up
            if (vertical > 0)
            {
                spriteRenderer.sprite = enemySpriteUp;
            }
            // Facing Down
            else if (vertical < 0)
            {
                spriteRenderer.sprite = enemySpriteDown;
            }
        }
    }

    protected override bool Move(int xDir, int yDir, out RaycastHit2D hit, out Vector2 destination)
    {
        // Get initial position
        Vector2 start = transform.position;
        // Get final position
        Vector2 end = start + new Vector2(xDir, yDir);
        // Set destination
        destination = end;

        // See if anything is hit
        hit = Physics2D.Linecast(start, end, blockingLayer);

        // If nothing has been hit, then start moving
        if (hit.transform == null)
        {
            DoneMoving = false;
            StartCoroutine(SmoothMovement(end));
            return true;
        }

        return false;
    }

    // This should not happen based on path of enemy
    // Logging GameObject for debug purposes
    protected override void OnCantMove<T>(T component)
    {
        GameObject hitGameObject = component.gameObject;
        Debug.Log("Enemy cannot move");
        Debug.Log("Enemy hit GameObject: " + hitGameObject);
    }


}
