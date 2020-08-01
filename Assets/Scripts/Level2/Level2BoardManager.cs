using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2BoardManager : BoardManager
{
    [Header("Level Information")]

    public Position playerStartingPosition;

    private Transform boardHolder;

    public GameObject Exit;
    public GameObject Bone;
    public GameObject Wall;
    public GameObject Ground;
    public GameObject Player;
    public GameObject Bone_Gate;
    public GameObject Enemy;
    public GameObject Water;
    public GameObject MoveableBlock;

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
        GenerateEnemies();
        GenerateWalls();
        GenerateWater();
        GenerateMoveableBlocks();
        GenerateCollectables();

        RequiredBoneCount = 4;
    }

    private void GenerateEnemies()
    {
        Transform enemyObjects = new GameObject("Enemies").transform;
        enemyObjects.transform.SetParent(boardHolder);

        GameObject enemy = Enemy;

        GameObject enemyInstance = (GameObject)Instantiate(enemy, new Vector3(13, 18, 0f), Quaternion.identity);
        enemyInstance.GetComponent<Level2EnemyScript>().StartingPathIndex = 16;
        enemyInstance.transform.SetParent(enemyObjects);

        enemyInstance = (GameObject)Instantiate(enemy, new Vector3(14, 19, 0f), Quaternion.identity);
        enemyInstance.GetComponent<Level2EnemyScript>().StartingPathIndex = 0;
        enemyInstance.transform.SetParent(enemyObjects);

        enemyInstance = (GameObject)Instantiate(enemy, new Vector3(13, 20, 0f), Quaternion.identity);
        enemyInstance.GetComponent<Level2EnemyScript>().StartingPathIndex = 2;
        enemyInstance.transform.SetParent(enemyObjects);
    }

    private void GenerateMoveableBlocks()
    {
        Transform moveableObjects = new GameObject("MoveableObjects").transform;
        moveableObjects.transform.SetParent(boardHolder);

        GameObject moveableBlock = MoveableBlock;
        List<Position> moveableBlockPositions = new List<Position>();
        moveableBlockPositions.Add(new Position(19, 19));
        moveableBlockPositions.Add(new Position(20, 19));

        foreach (Position position in moveableBlockPositions)
        {
            GameObject wallInstance = (GameObject)Instantiate(moveableBlock, new Vector3(position.X, position.Y, 0f), Quaternion.identity);

            wallInstance.transform.SetParent(moveableObjects);
        }
    }

    private void GenerateWater()
    {
        Transform waterTransform = new GameObject("Water").transform;
        waterTransform.transform.SetParent(boardHolder);

        GameObject waterObject = Water;
        List<Position> waterPositions = new List<Position>();
        waterPositions.Add(new Position(16, 18));
        waterPositions.Add(new Position(17, 18));
        waterPositions.Add(new Position(16, 19));
        waterPositions.Add(new Position(17, 19));
        waterPositions.Add(new Position(16, 20));
        waterPositions.Add(new Position(17, 20));

        foreach (Position position in waterPositions)
        {
            GameObject wallInstance = (GameObject)Instantiate(waterObject, new Vector3(position.X, position.Y, 0f), Quaternion.identity);

            wallInstance.transform.SetParent(waterTransform);
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
        bonePositions.Add(new Position(12, 15));
        bonePositions.Add(new Position(12, 23));

        bonePositions.Add(new Position(23, 18));
        bonePositions.Add(new Position(23, 20));


        foreach (Position position in bonePositions)
        {
            GameObject instance = (GameObject)Instantiate(bone, new Vector3(position.X, position.Y, 0f), Quaternion.identity);

            instance.transform.SetParent(collectabiles);
        }
    }

    private void GenerateWalls()
    {
        Transform walls = new GameObject("Walls").transform;
        walls.transform.SetParent(boardHolder);

        // Walls
        GameObject toInstantiate = Wall;

        List<Position> wallPositions = new List<Position>();
        wallPositions.Add(new Position(9, 14));
        wallPositions.Add(new Position(10, 14));
        wallPositions.Add(new Position(11, 14));
        wallPositions.Add(new Position(12, 14));
        wallPositions.Add(new Position(13, 14));
        wallPositions.Add(new Position(14, 14));
        wallPositions.Add(new Position(15, 14));

        wallPositions.Add(new Position(9, 15));
        wallPositions.Add(new Position(15, 15));

        wallPositions.Add(new Position(9, 16));
        wallPositions.Add(new Position(15, 16));

        wallPositions.Add(new Position(9, 17));
        wallPositions.Add(new Position(12, 17));
        wallPositions.Add(new Position(15, 17));
        wallPositions.Add(new Position(16, 17));
        wallPositions.Add(new Position(17, 17));
        wallPositions.Add(new Position(18, 17));
        wallPositions.Add(new Position(19, 17));
        wallPositions.Add(new Position(20, 17));
        wallPositions.Add(new Position(21, 17));
        wallPositions.Add(new Position(22, 17));
        wallPositions.Add(new Position(23, 17));
        wallPositions.Add(new Position(24, 17));

        wallPositions.Add(new Position(7, 18));
        wallPositions.Add(new Position(8, 18));
        wallPositions.Add(new Position(9, 18));
        wallPositions.Add(new Position(12, 18));
        wallPositions.Add(new Position(24, 18));

        wallPositions.Add(new Position(7, 19));
        wallPositions.Add(new Position(12, 19));
        wallPositions.Add(new Position(13, 19));
        wallPositions.Add(new Position(24, 19));

        wallPositions.Add(new Position(7, 20));
        wallPositions.Add(new Position(8, 20));
        wallPositions.Add(new Position(9, 20));
        wallPositions.Add(new Position(12, 20));
        wallPositions.Add(new Position(24, 20));

        wallPositions.Add(new Position(9, 21));
        wallPositions.Add(new Position(12, 21));
        wallPositions.Add(new Position(15, 21));
        wallPositions.Add(new Position(16, 21));
        wallPositions.Add(new Position(17, 21));
        wallPositions.Add(new Position(18, 21));
        wallPositions.Add(new Position(19, 21));
        wallPositions.Add(new Position(20, 21));
        wallPositions.Add(new Position(21, 21));
        wallPositions.Add(new Position(22, 21));
        wallPositions.Add(new Position(23, 21));
        wallPositions.Add(new Position(24, 21));

        wallPositions.Add(new Position(9, 22));
        wallPositions.Add(new Position(15, 22));

        wallPositions.Add(new Position(9, 23));
        wallPositions.Add(new Position(15, 23));

        wallPositions.Add(new Position(9, 24));
        wallPositions.Add(new Position(10, 24));
        wallPositions.Add(new Position(11, 24));
        wallPositions.Add(new Position(12, 24));
        wallPositions.Add(new Position(13, 24));
        wallPositions.Add(new Position(14, 24));
        wallPositions.Add(new Position(15, 24));

        foreach (Position position in wallPositions)
        {
            GameObject wallInstance = (GameObject)Instantiate(toInstantiate, new Vector3(position.X, position.Y, 0f), Quaternion.identity);

            wallInstance.transform.SetParent(walls);
        }

        //Exit
        GameObject exit = Exit;
        GameObject exitInstance = (GameObject)Instantiate(exit, new Vector3(8, 19, 0f), Quaternion.identity);
        exitInstance.transform.SetParent(walls);

        //Bone Gate
        GameObject boneGate = Bone_Gate;
        GameObject boneGateInstance = (GameObject)Instantiate(boneGate, new Vector3(9, 19, 0f), Quaternion.identity);
        boneGateInstance.transform.SetParent(walls);

    }
}
