using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3BoardManager : BoardManager
{
    [Header("Level Information")]

    public Position playerStartingPosition;

    private Transform boardHolder;

    public GameObject Bone;
    public GameObject Bone_Gate;
    public GameObject Bumper;
    public GameObject Exit;
    public GameObject Fire;
    public GameObject Ground;
    public GameObject Ice;
    public GameObject Player;
    public GameObject Slip_Pad;
    public GameObject WaterShoe;
    public GameObject IceShoe;
    public GameObject SlipPadShoe;
    public GameObject FireShoe;
    public GameObject Wall;
    public GameObject Water;

    public CinemachineVirtualCamera CmVCam;

    public virtual void Awake()
    {
        boardHolder = new GameObject("Board").transform;
    }

    // Start is called before the first frame update
    public override void GenerateLevel()
    {
        GenerateGround();
        GeneratePlayer();
        GenerateWalls();
        GenerateWater();
        GenerateIce();
        GenerateFire();
        GenerateSlipPad();
        GenerateCollectables();

        RequiredBoneCount = 4;
    }

    private void GenerateSlipPad()
    {
        Transform slipPadTransform = new GameObject("SlipPad").transform;
        slipPadTransform.transform.SetParent(boardHolder);

        GameObject slipPadObject = Slip_Pad;
        Dictionary<Direction,List<Position>> slipPadPositions = new Dictionary<Direction, List<Position>>();
        // Up
        slipPadPositions.Add(Direction.UP, new List<Position>());
        slipPadPositions[Direction.UP].Add(new Position(14,11));
        slipPadPositions[Direction.UP].Add(new Position(15,11));
        slipPadPositions[Direction.UP].Add(new Position(16,11));
        slipPadPositions[Direction.UP].Add(new Position(17,11));

        slipPadPositions[Direction.UP].Add(new Position(14, 12));
        slipPadPositions[Direction.UP].Add(new Position(16, 12));
        slipPadPositions[Direction.UP].Add(new Position(17, 12));

        slipPadPositions[Direction.UP].Add(new Position(14, 13));
        slipPadPositions[Direction.UP].Add(new Position(15, 13));
        slipPadPositions[Direction.UP].Add(new Position(16, 13));

        slipPadPositions[Direction.UP].Add(new Position(22, 13));
        slipPadPositions[Direction.UP].Add(new Position(22, 14));
        slipPadPositions[Direction.UP].Add(new Position(22, 15));
        slipPadPositions[Direction.UP].Add(new Position(22, 16));
        slipPadPositions[Direction.UP].Add(new Position(22, 17));
        slipPadPositions[Direction.UP].Add(new Position(22, 18));
        slipPadPositions[Direction.UP].Add(new Position(22, 19));

        slipPadPositions[Direction.UP].Add(new Position(19, 20));
        slipPadPositions[Direction.UP].Add(new Position(19, 21));
        slipPadPositions[Direction.UP].Add(new Position(19, 22));

        // Down
        slipPadPositions.Add(Direction.DOWN, new List<Position>());
        slipPadPositions[Direction.DOWN].Add(new Position(9, 14));
        slipPadPositions[Direction.DOWN].Add(new Position(9, 15));
        slipPadPositions[Direction.DOWN].Add(new Position(9, 16));
        slipPadPositions[Direction.DOWN].Add(new Position(9, 17));
        slipPadPositions[Direction.DOWN].Add(new Position(9, 18));
        slipPadPositions[Direction.DOWN].Add(new Position(9, 19));
        slipPadPositions[Direction.DOWN].Add(new Position(9, 20));

        slipPadPositions[Direction.DOWN].Add(new Position(12, 21));
        slipPadPositions[Direction.DOWN].Add(new Position(12, 22));
        slipPadPositions[Direction.DOWN].Add(new Position(12, 23));

        // Left
        slipPadPositions.Add(Direction.LEFT, new List<Position>());
        slipPadPositions[Direction.LEFT].Add(new Position(10, 20));
        slipPadPositions[Direction.LEFT].Add(new Position(11, 20));
        slipPadPositions[Direction.LEFT].Add(new Position(12, 20));
        slipPadPositions[Direction.LEFT].Add(new Position(20, 20));
        slipPadPositions[Direction.LEFT].Add(new Position(21, 20));
        slipPadPositions[Direction.LEFT].Add(new Position(22, 20));

        slipPadPositions[Direction.LEFT].Add(new Position(13, 23));
        slipPadPositions[Direction.LEFT].Add(new Position(14, 23));
        slipPadPositions[Direction.LEFT].Add(new Position(15, 23));
        slipPadPositions[Direction.LEFT].Add(new Position(16, 23));
        slipPadPositions[Direction.LEFT].Add(new Position(17, 23));
        slipPadPositions[Direction.LEFT].Add(new Position(18, 23));
        slipPadPositions[Direction.LEFT].Add(new Position(19, 23));


        // Right
        slipPadPositions.Add(Direction.RIGHT, new List<Position>());
        slipPadPositions[Direction.RIGHT].Add(new Position(9, 13));
        slipPadPositions[Direction.RIGHT].Add(new Position(10, 13));
        slipPadPositions[Direction.RIGHT].Add(new Position(11, 13));
        slipPadPositions[Direction.RIGHT].Add(new Position(12, 13));
        slipPadPositions[Direction.RIGHT].Add(new Position(13, 13));
        slipPadPositions[Direction.RIGHT].Add(new Position(17, 13));
        slipPadPositions[Direction.RIGHT].Add(new Position(18, 13));
        slipPadPositions[Direction.RIGHT].Add(new Position(19, 13));
        slipPadPositions[Direction.RIGHT].Add(new Position(20, 13));
        slipPadPositions[Direction.RIGHT].Add(new Position(21, 13));

        foreach (Direction dir in slipPadPositions.Keys)
        {
            foreach (Position position in slipPadPositions[dir])
            {
                GameObject slipPadInstance = (GameObject)Instantiate(slipPadObject, new Vector3(position.X, position.Y, 0f), Quaternion.identity);
                SlipPad pad = slipPadInstance.GetComponent<SlipPad>();
                pad.Direction = dir;
                switch (dir)
                {
                    case Direction.UP:
                        slipPadInstance.transform.Rotate(Vector3.forward * 90);
                        break;
                    case Direction.DOWN:
                        slipPadInstance.transform.Rotate(Vector3.forward * 270); 
                        break;
                    case Direction.LEFT:
                        slipPadInstance.transform.Rotate(Vector3.forward * 180);
                        break;
                    case Direction.RIGHT:
                        break;
                    default:
                        break;
                }
                slipPadInstance.transform.SetParent(slipPadTransform);
            }
        }
    }

    private void GenerateFire()
    {
        Transform fireTransform = new GameObject("Fire").transform;
        fireTransform.transform.SetParent(boardHolder);

        GameObject fireObject = Fire;
        List<Position> firePositions = new List<Position>();
        firePositions.Add(new Position(18, 15));
        firePositions.Add(new Position(19, 15));
        firePositions.Add(new Position(20, 15));
        firePositions.Add(new Position(18, 16));
        firePositions.Add(new Position(20, 16));
        firePositions.Add(new Position(18, 17));
        firePositions.Add(new Position(20, 17));
        firePositions.Add(new Position(18, 18));
        firePositions.Add(new Position(19, 18));
        firePositions.Add(new Position(20, 18));

        foreach (Position position in firePositions)
        {
            GameObject wallInstance = (GameObject)Instantiate(fireObject, new Vector3(position.X, position.Y, 0f), Quaternion.identity);

            wallInstance.transform.SetParent(fireTransform);
        }
    }

    private void GenerateIce()
    {
        Transform iceTransform = new GameObject("Ice").transform;
        iceTransform.transform.SetParent(boardHolder);

        GameObject iceObject = Ice;
        List<Position> icePositions = new List<Position>();
        icePositions.Add(new Position(11, 15));
        icePositions.Add(new Position(12, 15));
        icePositions.Add(new Position(13, 15));
        icePositions.Add(new Position(11, 16));
        icePositions.Add(new Position(11, 17));
        icePositions.Add(new Position(11, 18));
        icePositions.Add(new Position(12, 18));
        icePositions.Add(new Position(13, 18));

        foreach (Position position in icePositions)
        {
            GameObject wallInstance = (GameObject)Instantiate(iceObject, new Vector3(position.X, position.Y, 0f), Quaternion.identity);

            wallInstance.transform.SetParent(iceTransform);
        }
    }

    private void GenerateWater()
    {
        Transform waterTransform = new GameObject("Water").transform;
        waterTransform.transform.SetParent(boardHolder);

        GameObject waterObject = Water;
        List<Position> waterPositions = new List<Position>();
        waterPositions.Add(new Position(14, 19));
        waterPositions.Add(new Position(15, 19));
        waterPositions.Add(new Position(16, 19));
        waterPositions.Add(new Position(17, 19));
        waterPositions.Add(new Position(14, 20));
        waterPositions.Add(new Position(17, 20));
        waterPositions.Add(new Position(14, 21));
        waterPositions.Add(new Position(15, 21));
        waterPositions.Add(new Position(16, 21));
        waterPositions.Add(new Position(17, 21));

        foreach (Position position in waterPositions)
        {
            GameObject waterInstance = (GameObject)Instantiate(waterObject, new Vector3(position.X, position.Y, 0f), Quaternion.identity);

            waterInstance.transform.SetParent(waterTransform);
        }
    }

    public void GenerateGround()
    {
        Transform ground = new GameObject("Ground").transform;
        ground.transform.SetParent(boardHolder);

        // Create the grid
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject toInstantiate = Ground;

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);

                instance.transform.SetParent(ground);
            }
        }
    }

    private void GeneratePlayer()
    {
        GameObject toInstantiate = Player;

        GameObject instance = (GameObject)Instantiate(toInstantiate, new Vector3(playerStartingPosition.X, playerStartingPosition.Y, 0f), Quaternion.identity);

        instance.transform.SetParent(boardHolder);
        CmVCam.LookAt = instance.transform;
        CmVCam.Follow = instance.transform;
    }

    private void GenerateCollectables()
    {
        Transform collectabiles = new GameObject("Collectables").transform;
        collectabiles.transform.SetParent(boardHolder);

        // Bones
        GameObject bone = Bone;

        List<Position> bonePositions = new List<Position>();
        bonePositions.Add(new Position(15, 12));
        bonePositions.Add(new Position(12, 16));
        bonePositions.Add(new Position(19, 16));
        bonePositions.Add(new Position(16, 20));

        foreach (Position position in bonePositions)
        {
            GameObject instance = (GameObject)Instantiate(bone, new Vector3(position.X, position.Y, 0f), Quaternion.identity);

            instance.transform.SetParent(collectabiles);
        }

        // Shoes
        GameObject waterShoe = WaterShoe;
        GameObject iceShoe = IceShoe;
        GameObject fireShoe = FireShoe;
        GameObject slipPadShoe = SlipPadShoe;

        GameObject waterShoeInstance = (GameObject)Instantiate(waterShoe, new Vector3(16, 15, 0f), Quaternion.identity);
        waterShoeInstance.transform.SetParent(collectabiles);

        GameObject iceShoeInstance = (GameObject)Instantiate(iceShoe, new Vector3(15, 20, 0f), Quaternion.identity);
        iceShoeInstance.transform.SetParent(collectabiles);

        GameObject fireShoeInstance = (GameObject)Instantiate(fireShoe, new Vector3(12, 17, 0f), Quaternion.identity);
        fireShoeInstance.transform.SetParent(collectabiles);

        GameObject slipPadShoeInstance = (GameObject)Instantiate(slipPadShoe, new Vector3(19, 17, 0f), Quaternion.identity);
        slipPadShoeInstance.transform.SetParent(collectabiles);
    }

    private void GenerateWalls()
    {
        Transform walls = new GameObject("Walls").transform;
        walls.transform.SetParent(boardHolder);

        // Walls
        GameObject toInstantiate = Wall;

        List<Position> wallPositions = new List<Position>();
        wallPositions.Add(new Position(13, 10));
        wallPositions.Add(new Position(14, 10));
        wallPositions.Add(new Position(15, 10));
        wallPositions.Add(new Position(16, 10));
        wallPositions.Add(new Position(17, 10));
        wallPositions.Add(new Position(18, 10));

        wallPositions.Add(new Position(13, 11));
        wallPositions.Add(new Position(18, 11));

        wallPositions.Add(new Position(8, 12));
        wallPositions.Add(new Position(9, 12));
        wallPositions.Add(new Position(10, 12));
        wallPositions.Add(new Position(11, 12));
        wallPositions.Add(new Position(12, 12));
        wallPositions.Add(new Position(13, 12));
        wallPositions.Add(new Position(18, 12));
        wallPositions.Add(new Position(19, 12));
        wallPositions.Add(new Position(20, 12));
        wallPositions.Add(new Position(21, 12));
        wallPositions.Add(new Position(22, 12));
        wallPositions.Add(new Position(23, 12));

        wallPositions.Add(new Position(8, 13));
        wallPositions.Add(new Position(23, 13));

        wallPositions.Add(new Position(8, 14));
        wallPositions.Add(new Position(10, 14));
        wallPositions.Add(new Position(11, 14));
        wallPositions.Add(new Position(12, 14));
        wallPositions.Add(new Position(13, 14));
        wallPositions.Add(new Position(18, 14));
        wallPositions.Add(new Position(19, 14));
        wallPositions.Add(new Position(20, 14));
        wallPositions.Add(new Position(21, 14));
        wallPositions.Add(new Position(23, 14));

        wallPositions.Add(new Position(8, 15));
        wallPositions.Add(new Position(10, 15));
        wallPositions.Add(new Position(21, 15));
        wallPositions.Add(new Position(23, 15));

        wallPositions.Add(new Position(8, 16));
        wallPositions.Add(new Position(10, 16));
        wallPositions.Add(new Position(13, 16));
        wallPositions.Add(new Position(21, 16));
        wallPositions.Add(new Position(23, 16));

        wallPositions.Add(new Position(8, 17));
        wallPositions.Add(new Position(10, 17));
        wallPositions.Add(new Position(13, 17));
        wallPositions.Add(new Position(21, 17));
        wallPositions.Add(new Position(23, 17));

        wallPositions.Add(new Position(8, 18));
        wallPositions.Add(new Position(10, 18));
        wallPositions.Add(new Position(21, 18));
        wallPositions.Add(new Position(23, 18));

        wallPositions.Add(new Position(8, 19));
        wallPositions.Add(new Position(10, 19));
        wallPositions.Add(new Position(11, 19));
        wallPositions.Add(new Position(12, 19));
        wallPositions.Add(new Position(13, 19));
        wallPositions.Add(new Position(18, 19));
        wallPositions.Add(new Position(19, 19));
        wallPositions.Add(new Position(20, 19));
        wallPositions.Add(new Position(21, 19));
        wallPositions.Add(new Position(23, 19));

        wallPositions.Add(new Position(8, 20));
        wallPositions.Add(new Position(13, 20));
        wallPositions.Add(new Position(18, 20));
        wallPositions.Add(new Position(23, 20));

        wallPositions.Add(new Position(8, 21));
        wallPositions.Add(new Position(9, 21));
        wallPositions.Add(new Position(10, 21));
        wallPositions.Add(new Position(11, 21));
        wallPositions.Add(new Position(13, 21));
        wallPositions.Add(new Position(18, 21));
        wallPositions.Add(new Position(20, 21));
        wallPositions.Add(new Position(21, 21));
        wallPositions.Add(new Position(22, 21));
        wallPositions.Add(new Position(23, 21));

        wallPositions.Add(new Position(11, 22));
        wallPositions.Add(new Position(13, 22));
        wallPositions.Add(new Position(14, 22));
        wallPositions.Add(new Position(15, 22));
        wallPositions.Add(new Position(16, 22));
        wallPositions.Add(new Position(17, 22));
        wallPositions.Add(new Position(18, 22));
        wallPositions.Add(new Position(20, 22));

        wallPositions.Add(new Position(11, 23));
        wallPositions.Add(new Position(20, 23));

        wallPositions.Add(new Position(11, 24));
        wallPositions.Add(new Position(12, 24));
        wallPositions.Add(new Position(13, 24));
        wallPositions.Add(new Position(14, 24));
        wallPositions.Add(new Position(15, 24));
        wallPositions.Add(new Position(17, 24));
        wallPositions.Add(new Position(18, 24));
        wallPositions.Add(new Position(19, 24));
        wallPositions.Add(new Position(20, 24));

        wallPositions.Add(new Position(15, 25));
        wallPositions.Add(new Position(17, 25));

        wallPositions.Add(new Position(15, 26));
        wallPositions.Add(new Position(16, 26));
        wallPositions.Add(new Position(17, 26));

        foreach (Position position in wallPositions)
        {
            GameObject wallInstance = (GameObject)Instantiate(toInstantiate, new Vector3(position.X, position.Y, 0f), Quaternion.identity);

            wallInstance.transform.SetParent(walls);
        }

        // Bumpers
        GameObject bumper = Bumper;
        GameObject bumperInstance = (GameObject)Instantiate(bumper, new Vector3(11, 15, 0f), Quaternion.identity);
        bumperInstance.transform.SetParent(walls);
        bumperInstance.transform.Rotate(Vector3.forward * 90);
        bumperInstance.GetComponent<SpriteRenderer>().sortingOrder = 1;
        bumperInstance = (GameObject)Instantiate(bumper, new Vector3(11, 18, 0f), Quaternion.identity);
        bumperInstance.GetComponent<SpriteRenderer>().sortingOrder = 1;
        bumperInstance.transform.SetParent(walls);

        //Exit
        GameObject exit = Exit;
        GameObject exitInstance = (GameObject)Instantiate(exit, new Vector3(16, 25, 0f), Quaternion.identity);
        exitInstance.transform.SetParent(walls);

        //Bone Gate
        GameObject boneGate = Bone_Gate;
        GameObject boneGateInstance = (GameObject)Instantiate(boneGate, new Vector3(16, 24, 0f), Quaternion.identity);
        boneGateInstance.transform.SetParent(walls);

    }
}
