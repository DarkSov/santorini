using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardState : MonoBehaviour
{
    private int[,] board = new int[5,5];
    public GameController gameController;
    public GameObject buildingPrefab;

    public int[,] getBoard()
    {
        return board;
    }

    public bool buildFloor(int x, int y)
    {
        Player redPlayer = gameController.redPlayer;
        Player bluePlayer = gameController.bluePlayer;

        if (redPlayer.getX() == x && redPlayer.getY() == y)
        {
            return false;
        }

        if (bluePlayer.getX() == x && bluePlayer.getY() == y)
        {
            return false;
        }
        if (gameController.getPhase() == GamePhase.RedBuildTurn && (Math.Abs(x - redPlayer.getX()) > 1 || Math.Abs(y - redPlayer.getY()) > 1))
        {
            return false;
        }

        if (gameController.getPhase() == GamePhase.BlueBuildTurn && (Math.Abs(x - bluePlayer.getX()) > 1 || Math.Abs(y - bluePlayer.getY()) > 1))
        {
            return false;
        }
      

    


        if (board[x, y] < 4)
        {
            board[x, y] += 1;
            GameObject building = Instantiate(buildingPrefab, new Vector3(x, board[x,y]==0?0:board[x,y]-0.5f, y), Quaternion.identity);
            building.transform.parent = this.transform;
            Building buildingScript = building.GetComponent<Building>();
            buildingScript.setPos(x, y);
            buildingScript.setTier(board[x, y]);
            return true;
        }
        return false;
    }

    

    public bool isMoveValid(int x1, int y1, int x2, int y2){
        // Check if the cell is adjacent
        if (Mathf.Abs(x1 - x2) + Mathf.Abs(y1 - y2) != 1)
        {
            return false;
        }

        // Check if the cell difference is less than 1
        if (board[x2, y2] - board[x1, y1] > 1)
        {
            return false;
        }

        if(board[x2,y2] == 4){
            return false;
        }

        return true;
    }


    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                board[i, j] = -1;
                buildFloor(i, j);
            }
        }
    }

    // Update is called once per frame
    private int i = 0;
    void FixedUpdate()
    {
        // if (i == 50)
        // {
        //     i = 0;
        //     // Build a random floor
        //     int x = Random.Range(0, 5);
        //     int y = Random.Range(0, 5);
        //     buildFloor(x, y);
        // }
        // i++;
    }
}
