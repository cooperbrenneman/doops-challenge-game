using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBlock : MovingObject
{
    private Vector2 destination;
    private LevelManager levelManager;
    private bool isMovingOntoWater;
    private BoxCollider2D moveableBlockBoxCollider;
    private SpriteRenderer spriteRenderer;
    public Sprite waterBlockSprite;
    private Transform waterTransform;

    protected override void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveableBlockBoxCollider = GetComponent<BoxCollider2D>();
        DoneMoving = true;
        isMovingOntoWater = false;
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        base.Start();
    }

    private void Update()
    {
        // Only update if the block is not done moving
        if (!DoneMoving)
        {
            // If block has reached its final location and moving on water
            if (transform.position.Equals(destination))
            {
                Debug.Log("Entered Destination");
                if (isMovingOntoWater)
                {
                    // Change the sprite to new walkable block and remove box collider
                    moveableBlockBoxCollider.enabled = false;
                    spriteRenderer.sprite = waterBlockSprite;
                    // Set layer and sort order so player can walk
                    spriteRenderer.sortingOrder = 0;
                    waterTransform.gameObject.layer = LayerMask.NameToLayer("Default");
                }
                isMovingOntoWater = false;
                LevelManager.PlayerTurn = true;
            }
        }
    }

    protected override void OnCantMove<T>(T component)
    {
        // If the block is unable to move
        GameObject hitGameObject = component.gameObject;
        Debug.Log("Moveable Block hit " + hitGameObject);
        // Allow player to move again
        LevelManager.PlayerTurn = true;
    }

    protected override bool Move(int xDir, int yDir, out RaycastHit2D hit, out Vector2 destination)
    {
        // Get initial position
        Vector2 start = transform.position;
        // Get final position
        Vector2 end = start + new Vector2(xDir, yDir);
        // Set destination
        destination = end;

        // Disable BoxCollider so that linecast does not hit own BoxCollider
        moveableBlockBoxCollider.enabled = false;
        // Cast ray from Start to End looking for anything on the blocking layer
        hit = Physics2D.Linecast(start, end, blockingLayer);
        // Reenable BoxCollider
        moveableBlockBoxCollider.enabled = true;

        if (hit.transform == null)
        {
            // Stop player movement so block can move
            LevelManager.PlayerTurn = false;
            // Set DoneMoving to false for block
            DoneMoving = false;
            // Move block
            StartCoroutine(SmoothMovement(end));
            return true;
        } 
        // If moving into water, then update bools to allow for sprite update
        else if (hit.transform.CompareTag("Water"))
        {
            // Set waterTransform
            waterTransform = hit.transform;
            // Stop player movement so block can move
            LevelManager.PlayerTurn = false;
            // Set DoneMoving to false for block
            DoneMoving = false;
            // Move block
            StartCoroutine(SmoothMovement(end));
            // Sets bool if the block is specifically moving onto water
            isMovingOntoWater = true;
            
            return true;
        }

        return false;
    }

    public override void AttemptMove<T>(int xDir, int yDir)
    {
        // If we are attempting to move the block, we know the character shouldn't move
        LevelManager.PlayerTurn = false;

        base.AttemptMove<T>(xDir, yDir);

        RaycastHit2D hit;

        if (Move(xDir, yDir, out hit, out destination))
        {
            //Call RandomizeSfx of SoundManager to play the move sound, passing in two audio clips to choose from.
        }
        else
        {
            LevelManager.PlayerTurn = true;
            // If the block was attempted to be moved and cannot, then player can move
        }
    }

}
