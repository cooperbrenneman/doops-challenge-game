using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Level1BoardManager : BoardManager
{
    [Header("Level Information")]
    
    public Position playerStartingPosition;

    private Transform boardHolder;

    public GameObject Exit;
    public GameObject Door_Blue;
    public GameObject Door_Green;
    public GameObject Door_Red;
    public GameObject Door_Yellow;
    public GameObject Key_Blue;
    public GameObject Key_Green;
    public GameObject Key_Red;
    public GameObject Key_Yellow;
    public GameObject Bone;
    public GameObject Wall;
    public GameObject Ground;
    public GameObject Player;
    public GameObject Bone_Gate;

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
        GenerateDoors();
        GenerateCollectables();

        RequiredBoneCount = 11;
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

        // Keys
        GameObject blueKey = Key_Blue;
        GameObject greenKey = Key_Green;
        GameObject redKey = Key_Red;
        GameObject yellowKey = Key_Yellow;

        List<Position> blueKeys = new List<Position>();
        List<Position> greenKeys = new List<Position>();
        List<Position> redKeys = new List<Position>();
        List<Position> yellowKeys = new List<Position>();

        blueKeys.Add(new Position(13, 16));
        blueKeys.Add(new Position(13, 18));

        greenKeys.Add(new Position(16, 11));

        redKeys.Add(new Position(17, 16));
        redKeys.Add(new Position(17, 18));

        yellowKeys.Add(new Position(10, 19));
        yellowKeys.Add(new Position(20, 19));

        foreach (Position position in blueKeys)
        {
            GameObject instance = (GameObject)Instantiate(blueKey, new Vector3(position.X, position.Y, 0f), Quaternion.identity);

            instance.transform.SetParent(collectabiles);
        }

        foreach (Position position in greenKeys)
        {
            GameObject instance = (GameObject)Instantiate(greenKey, new Vector3(position.X, position.Y, 0f), Quaternion.identity);

            instance.transform.SetParent(collectabiles);
        }

        foreach (Position position in redKeys)
        {
            GameObject instance = (GameObject)Instantiate(redKey, new Vector3(position.X, position.Y, 0f), Quaternion.identity);

            instance.transform.SetParent(collectabiles);
        }

        foreach (Position position in yellowKeys)
        {
            GameObject instance = (GameObject)Instantiate(yellowKey, new Vector3(position.X, position.Y, 0f), Quaternion.identity);

            instance.transform.SetParent(collectabiles);
        }

        // Bones
        GameObject bone = Bone;

        List<Position> bonePositions = new List<Position>();
        bonePositions.Add(new Position(14, 12));
        bonePositions.Add(new Position(16, 12));

        bonePositions.Add(new Position(15, 15));

        bonePositions.Add(new Position(10, 16));
        bonePositions.Add(new Position(20, 16));
        
        bonePositions.Add(new Position(13, 17));
        bonePositions.Add(new Position(17, 17));

        bonePositions.Add(new Position(10, 18));
        bonePositions.Add(new Position(20, 18));

        bonePositions.Add(new Position(12, 21));
        bonePositions.Add(new Position(18, 21));

        foreach (Position position in bonePositions)
        {
            GameObject instance = (GameObject)Instantiate(bone, new Vector3(position.X, position.Y, 0f), Quaternion.identity);

            instance.transform.SetParent(collectabiles);
        }
    }

    private void GenerateDoors()
    {
        Transform doors = new GameObject("Doors").transform;
        doors.transform.SetParent(boardHolder);

        // Keys Doors
        GameObject blueDoor = Door_Blue;
        GameObject greenDoor = Door_Green;
        GameObject redDoor = Door_Red;
        GameObject yellowDoor = Door_Yellow;

        List<Position> blueDoors = new List<Position>();
        List<Position> greenDoors = new List<Position>();
        List<Position> redDoors = new List<Position>();
        List<Position> yellowDoors = new List<Position>();

        blueDoors.Add(new Position(12, 19));
        blueDoors.Add(new Position(18, 15));

        greenDoors.Add(new Position(13, 20));
        greenDoors.Add(new Position(17, 20));

        redDoors.Add(new Position(12, 15));
        redDoors.Add(new Position(18, 19));

        yellowDoors.Add(new Position(14, 14));
        yellowDoors.Add(new Position(16, 14));

        foreach (Position position in blueDoors)
        {
            GameObject instance = (GameObject)Instantiate(blueDoor, new Vector3(position.X, position.Y, 0f), Quaternion.identity);

            instance.transform.SetParent(doors);
        }

        foreach (Position position in greenDoors)
        {
            GameObject instance = (GameObject)Instantiate(greenDoor, new Vector3(position.X, position.Y, 0f), Quaternion.identity);

            instance.transform.SetParent(doors);
        }

        foreach (Position position in redDoors)
        {
            GameObject instance = (GameObject)Instantiate(redDoor, new Vector3(position.X, position.Y, 0f), Quaternion.identity);

            instance.transform.SetParent(doors);
        }

        foreach (Position position in yellowDoors)
        {
            GameObject instance = (GameObject)Instantiate(yellowDoor, new Vector3(position.X, position.Y, 0f), Quaternion.identity);

            instance.transform.SetParent(doors);
        }
    }

    private void GenerateWalls()
    {
        Transform walls = new GameObject("Walls").transform;
        walls.transform.SetParent(boardHolder);

        GameObject toInstantiate = Wall;

        List<Position> wallPositions = new List<Position>();
        wallPositions.Add(new Position(12, 10));
        wallPositions.Add(new Position(13, 10));
        wallPositions.Add(new Position(14, 10));
        wallPositions.Add(new Position(15, 10));
        wallPositions.Add(new Position(16, 10));
        wallPositions.Add(new Position(17, 10));
        wallPositions.Add(new Position(18, 10));

        wallPositions.Add(new Position(12, 11));
        wallPositions.Add(new Position(15, 11));
        wallPositions.Add(new Position(18, 11));

        wallPositions.Add(new Position(12, 12));
        wallPositions.Add(new Position(15, 12));
        wallPositions.Add(new Position(18, 12));

        wallPositions.Add(new Position(12, 13));
        wallPositions.Add(new Position(15, 13));
        wallPositions.Add(new Position(18, 13));

        wallPositions.Add(new Position(8, 14));
        wallPositions.Add(new Position(9, 14));
        wallPositions.Add(new Position(10, 14));
        wallPositions.Add(new Position(11, 14));
        wallPositions.Add(new Position(12, 14));
        wallPositions.Add(new Position(13, 14));
        wallPositions.Add(new Position(15, 14));
        wallPositions.Add(new Position(17, 14));
        wallPositions.Add(new Position(18, 14));
        wallPositions.Add(new Position(19, 14));
        wallPositions.Add(new Position(20, 14));
        wallPositions.Add(new Position(21, 14));
        wallPositions.Add(new Position(22, 14));

        wallPositions.Add(new Position(8, 15));
        wallPositions.Add(new Position(22, 15));

        wallPositions.Add(new Position(8, 16));
        wallPositions.Add(new Position(12, 16));
        wallPositions.Add(new Position(18, 16));
        wallPositions.Add(new Position(22, 16));

        wallPositions.Add(new Position(8, 17));
        wallPositions.Add(new Position(9, 17));
        wallPositions.Add(new Position(10, 17));
        wallPositions.Add(new Position(11, 17));
        wallPositions.Add(new Position(12, 17));
        wallPositions.Add(new Position(18, 17));
        wallPositions.Add(new Position(19, 17));
        wallPositions.Add(new Position(20, 17));
        wallPositions.Add(new Position(21, 17));
        wallPositions.Add(new Position(22, 17));

        wallPositions.Add(new Position(8, 18));
        wallPositions.Add(new Position(12, 18));
        wallPositions.Add(new Position(18, 18));
        wallPositions.Add(new Position(22, 18));

        wallPositions.Add(new Position(8, 19));
        wallPositions.Add(new Position(22, 19));

        wallPositions.Add(new Position(8, 20));
        wallPositions.Add(new Position(9, 20));
        wallPositions.Add(new Position(10, 20));
        wallPositions.Add(new Position(11, 20));
        wallPositions.Add(new Position(12, 20));
        wallPositions.Add(new Position(14, 20));
        wallPositions.Add(new Position(16, 20));
        wallPositions.Add(new Position(18, 20));
        wallPositions.Add(new Position(19, 20));
        wallPositions.Add(new Position(20, 20));
        wallPositions.Add(new Position(21, 20));
        wallPositions.Add(new Position(22, 20));

        wallPositions.Add(new Position(10, 21));
        wallPositions.Add(new Position(14, 21));
        wallPositions.Add(new Position(16, 21));
        wallPositions.Add(new Position(20, 21));

        wallPositions.Add(new Position(10, 22));
        wallPositions.Add(new Position(14, 22));
        wallPositions.Add(new Position(15, 22));
        wallPositions.Add(new Position(16, 22));
        wallPositions.Add(new Position(20, 22));

        wallPositions.Add(new Position(10, 23));
        wallPositions.Add(new Position(11, 23));
        wallPositions.Add(new Position(12, 23));
        wallPositions.Add(new Position(13, 23));
        wallPositions.Add(new Position(14, 23));
        wallPositions.Add(new Position(16, 23));
        wallPositions.Add(new Position(17, 23));
        wallPositions.Add(new Position(18, 23));
        wallPositions.Add(new Position(19, 23));
        wallPositions.Add(new Position(20, 23));

        foreach (Position position in wallPositions)
        {
            GameObject wallInstance = (GameObject)Instantiate(toInstantiate, new Vector3(position.X, position.Y, 0f), Quaternion.identity);

            wallInstance.transform.SetParent(walls);
        }

        GameObject exit = Exit;
        GameObject exitInstance = (GameObject)Instantiate(exit, new Vector3(15, 21, 0f), Quaternion.identity);
        exitInstance.transform.SetParent(walls);

        GameObject boneGate = Bone_Gate;
        GameObject boneGateInstance = (GameObject)Instantiate(boneGate, new Vector3(15, 20, 0f), Quaternion.identity);
        boneGateInstance.transform.SetParent(walls);

    }
}
