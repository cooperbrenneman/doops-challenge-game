using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class Player : MovingObject
{
    private LevelManager levelManager;
    private bool canUnlockBoneGate = false;
    private bool exitedLevel;
    private Inventory inventory;
    private BoxCollider2D playerBoxCollider;
    private bool canSlipOnIce = true;
    private bool canSlipOnPad = true;
    private Direction[] lowerIcePath = new Direction[] { Direction.LEFT, Direction.LEFT, Direction.UP, Direction.UP, Direction.UP, Direction.RIGHT, Direction.RIGHT, Direction.RIGHT };
    private Direction[] upperIcePath = new Direction[] { Direction.LEFT, Direction.LEFT, Direction.DOWN, Direction.DOWN, Direction.DOWN, Direction.RIGHT, Direction.RIGHT, Direction.RIGHT };
    private Direction[] currentIcePath;
    private Vector2 lowerIcePathStart = new Vector2(13, 15);
    private Vector2 upperIcePathStart = new Vector2(13, 18);
    private int iceIndex = 0;
    private bool isSlippingOnIce = false;
    private SpriteRenderer playerSpriteRender;
    public Sprite playerSpriteUp;
    public Sprite playerSpriteDown;
    public Sprite playerSpriteLeft;
    public Sprite playerSpriteRight;
    public Sprite playerSpriteSwimUp;
    public Sprite playerSpriteSwimDown;
    public Sprite playerSpriteSwimLeft;
    public Sprite playerSpriteSwimRight;

    private bool isSwimming = false;

    protected override void Start()
    {
        // Setup inventory based on level
        inventory = new Inventory(LevelManager.Level);
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        playerBoxCollider = GetComponent<BoxCollider2D>();
        playerSpriteRender = GetComponent<SpriteRenderer>();
        currentIcePath = lowerIcePath;
        base.Start();
    }

    void Update()
    {
        // If it is not the player's turn, then do nothing
        if (!LevelManager.PlayerTurn)
        {
            return;
        }

        // Check input and normalize to int 0 or 1
        int horizontal = (int)Input.GetAxisRaw("Horizontal");
        int vertical = (int)Input.GetAxisRaw("Vertical");

        // Do not allow diagonal movement
        if(horizontal != 0)
        {
            vertical = 0;
        }

        // Check if done moving - if moving, then do nothing
        if (DoneMoving)
        {
            // If we landed in the exit level, then initiate Level Complete
            if (exitedLevel)
            {
                Debug.Log("Level Complete");
                LevelManager.PlayerTurn = false;
                levelManager.LevelComplete();
            }

            // If iceIndex is greater than length of the array, then it means the player just finished slipping on ice
            if (iceIndex > currentIcePath.Length - 1)
            {
                // Reset ice index
                isSlippingOnIce = false;
                iceIndex = 0;
                return;
            }

            // Check current block
            Vector2 start = transform.position;
            playerBoxCollider.enabled = false;
            RaycastHit2D onSlipperySurface = Physics2D.Linecast(start, start + new Vector2(0, 0.1f), slippingLayer);
            playerBoxCollider.enabled = true;

            // Check to see if we are on a slippery surface
            if (onSlipperySurface.transform != null)
            {
                // Check if player is on a Slip Pad and player can slip on pads
                if(onSlipperySurface.transform.CompareTag("Slip_Pad") && canSlipOnPad)
                {
                    // Get the direction based on the slip pad orientation
                    Direction nextDirection = onSlipperySurface.transform.GetComponent<SlipPad>().Direction;
                    // Move player based on direction
                    MovePlayerOnSlipperySurface(nextDirection);
                }
                // Check if player is on ice and player can slip on ice
                else if (onSlipperySurface.transform.tag == "Ice" && canSlipOnIce)
                {
                    // If already slipping on ice
                    if (isSlippingOnIce)
                    {
                        // Find next Direction and increment, then move player
                        Direction nextDirection = currentIcePath[iceIndex];
                        iceIndex++;
                        MovePlayerOnSlipperySurface(nextDirection);
                    }
                    // If not slipping on ice
                    else
                    {
                        // See if they entered from lower level or upper level
                        if(start.Equals(lowerIcePathStart))
                        {
                            Debug.Log("Hit Lower Ice");
                            isSlippingOnIce = true;
                            iceIndex = 0;
                            currentIcePath = lowerIcePath;
                        } else if (start.Equals(upperIcePathStart))
                        {
                            Debug.Log("Hit Upper Ice");
                            isSlippingOnIce = true;
                            iceIndex = 0;
                            currentIcePath = upperIcePath;
                        }
                    }
                }
                else
                {
                    // If there is input
                    if ((horizontal != 0 || vertical != 0))
                    {
                        // Attempt to move the Player
                        AttemptMove<Door>(horizontal, vertical);
                    }
                }
            }
            // If player is not currently on a slippery surface and there is input
            else if(onSlipperySurface.transform == null && (horizontal != 0 || vertical != 0))
            {
                // Attempt to move the Player
                AttemptMove<Door>(horizontal, vertical);
            }
        }
    }

    private void MovePlayerOnSlipperySurface(Direction nextDirection)
    {
        switch (nextDirection)
        {
            case Direction.UP:
                MoveOnSlipperySurface(0, 1);
                return;
            case Direction.DOWN:
                MoveOnSlipperySurface(0, -1);
                return;
            case Direction.LEFT:
                MoveOnSlipperySurface(-1, 0);
                return;
            case Direction.RIGHT:
                MoveOnSlipperySurface(1, 0);
                return;
            default:
                break;
        }
    }

    private void ChangePlayerDirection(int horizontal, int vertical)
    {
        if(horizontal != 0)
        {
            // Facing Right
            if(horizontal > 0)
            {
                 playerSpriteRender.sprite = isSwimming ? playerSpriteSwimRight : playerSpriteRight;
            }
            // Facing Left
            else if(horizontal < 0)
            {
                playerSpriteRender.sprite = isSwimming ? playerSpriteSwimLeft : playerSpriteLeft;
            }
        } 
        else if(vertical != 0)
        {
            // Facing Up
            if (vertical > 0)
            {
                playerSpriteRender.sprite = isSwimming ? playerSpriteSwimUp : playerSpriteUp;
            }
            // Facing Down
            else if (vertical < 0)
            {
                playerSpriteRender.sprite = isSwimming ? playerSpriteSwimDown : playerSpriteDown;
            }
        }
    }

    protected override void OnCantMove<T>(T component)
    {
        GameObject hitGameObject = component.gameObject;
        if(hitGameObject.tag == "BlueDoor" && inventory.Keys["Blue"].Total > 0)
        {
            inventory.Keys["Blue"].RemoveKey();
            Door hitDoor = component as Door;
            hitDoor.RemoveDoor();
        }
        else if (hitGameObject.tag == "GreenDoor" && inventory.Keys["Green"].Total > 0)
        {
            inventory.Keys["Green"].RemoveKey();
            Door hitDoor = component as Door;
            hitDoor.RemoveDoor();
        }
        else if (hitGameObject.tag == "RedDoor" && inventory.Keys["Red"].Total > 0)
        {
            inventory.Keys["Red"].RemoveKey();
            Door hitDoor = component as Door;
            hitDoor.RemoveDoor();
        }
        else if (hitGameObject.tag == "YellowDoor" && inventory.Keys["Yellow"].Total > 0)
        {
            inventory.Keys["Yellow"].RemoveKey();
            Door hitDoor = component as Door;
            hitDoor.RemoveDoor();
        }
        else if (hitGameObject.tag == "BoneGate" && canUnlockBoneGate)
        {
            Door hitDoor = component as Door;
            hitDoor.RemoveDoor();
        }
    }

    public bool MoveOnSlipperySurface(int xDir, int yDir)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        playerBoxCollider.enabled = false;
        RaycastHit2D hit = Physics2D.Linecast(end, end + new Vector2(xDir, yDir + 0.1f), blockingLayer);
        playerBoxCollider.enabled = true;

        DoneMoving = false;
        ChangePlayerDirection(xDir,yDir);
        StartCoroutine(SmoothMovement(end));
        return true;
    }

    protected override bool Move(int xDir, int yDir, out RaycastHit2D hit, out Vector2 destination)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);
        destination = end;

        playerBoxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        RaycastHit2D hitSwimming = Physics2D.Linecast(end, end + new Vector2(.1f,0f), swimmingLayer);
        playerBoxCollider.enabled = true;

        // If moving onto a swimming block layer OR if already swimming and hit wall
        if (hitSwimming.transform != null || (isSwimming && hit.transform != null))
        {
            // Set isSwimming and change direction
            isSwimming = true;
            ChangePlayerDirection(xDir,yDir);
        }
        else
        {
            // Set isSwimming and change direction
            isSwimming = false;
            ChangePlayerDirection(xDir, yDir);
        }

        // If player did not hit blocking layer, move player
        if (hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }
        // If player is moving into MoveableBlock
        else if (hit.transform.CompareTag("MoveableBlock"))
        {
            // Do not let player move
            LevelManager.PlayerTurn = false;
            // Get the block that was hit
            MoveableBlock moveableBlock = hit.transform.GetComponent<MoveableBlock>();
            // Attempt to move that block in the direction that the play was moving
            moveableBlock.AttemptMove<MoveableBlock>(xDir, yDir);
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If player hits exit, then set exit so that ExitLevel is loaded once movement completes
        if (collision.CompareTag("Exit"))
        {
            exitedLevel = true;
        }
        else if (collision.CompareTag("Bone"))
        {
            AddBone();
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("BlueKey"))
        {
            inventory.Keys["Blue"].AddKey();
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("GreenKey"))
        {
            inventory.Keys["Green"].AddKey();
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("RedKey"))
        {
            inventory.Keys["Red"].AddKey();
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("YellowKey"))
        {
            inventory.Keys["Yellow"].AddKey();
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("FireShoe"))
        {
            inventory.Shoes.Add("FireShoe");
            // Turn all fire GameObjects off blocking layer
            GameObject[] fireTiles = GameObject.FindGameObjectsWithTag("Fire");
            foreach (GameObject fire in fireTiles)
            {
                fire.gameObject.layer = LayerMask.NameToLayer("Default");
            }
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("IceShoe"))
        {
            inventory.Shoes.Add("IceShoe");
            canSlipOnIce = false;
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("SlipPadShoe"))
        {
            inventory.Shoes.Add("SlipPadShoe");
            canSlipOnPad = false;
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("WaterShoe"))
        {
            inventory.Shoes.Add("WaterShoe");
            // Turn all water GameObjects off blocking layer
            GameObject[] waterTiles = GameObject.FindGameObjectsWithTag("Water");
            foreach(GameObject water in waterTiles)
            {
                water.gameObject.layer = LayerMask.NameToLayer("SwimmingLayer");
            }
            collision.gameObject.SetActive(false);
        }
        else if(collision.CompareTag("Enemy"))
        {
            levelManager.GameOver();
        }

    }

    public void AddBone()
    {
        // Add one bone to the inventory
        inventory.AddBone();

        // Check if total bones is greater than the required bone count
        if(inventory.TotalBones >= BoardManager.RequiredBoneCount)
        {
            // Allow player to unlock the gate
            canUnlockBoneGate = true;
        }

        // Log that the player can unlock bone gates
        if(inventory.TotalBones == BoardManager.RequiredBoneCount)
        {
            Debug.Log("Can now unlock bone gate");
        }
    }

}
