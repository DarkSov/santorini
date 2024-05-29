using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum GamePhase
{
    RedSetup,
    BlueSetup,
    RedMoveTurn,
    BlueMoveTurn,
    RedBuildTurn,
    BlueBuildTurn,
    RedWin,
    BlueWin
}

public class GameController : MonoBehaviour
{
    public Player redPlayer;
    public Player bluePlayer;

    public GameObject redPlayerObject;
    public GameObject bluePlayerObject;

    private GamePhase phase;

    public GamePhase getPhase()
    {
        return phase;
    }

    public void nextPhase()
    {
        switch (phase)
        {
            case GamePhase.RedSetup:
                phase = GamePhase.BlueSetup;
                break;
            case GamePhase.BlueSetup:
                phase = GamePhase.RedMoveTurn;
                break;
            case GamePhase.RedMoveTurn:
                phase = GamePhase.RedBuildTurn;
                break;
            case GamePhase.RedBuildTurn:
                phase = GamePhase.BlueMoveTurn;
                break;
            case GamePhase.BlueMoveTurn:
                phase = GamePhase.BlueBuildTurn;
                break;
            case GamePhase.BlueBuildTurn:
                phase = GamePhase.RedMoveTurn;
                break;
            case GamePhase.RedWin:
                break;
            case GamePhase.BlueWin:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public BoardState boardState;

    // public GameObject boardCells;

    public bool movePlayer(bool isRed, int x, int y)
    {
        var boardArray = boardState.getBoard();
        int height = boardArray[x,y];

        if(Math.Abs(x-(isRed?redPlayer.getX():bluePlayer.getX()))>1 || Math.Abs(y- (isRed?redPlayer.getY():bluePlayer.getY()))>1 || height == 4 || height == -1 || height - boardArray[isRed?redPlayer.getX():bluePlayer.getX(), isRed?redPlayer.getY():bluePlayer.getY()] > 1 || (x == (isRed?bluePlayer.getX():redPlayer.getX()) && y == (isRed?bluePlayer.getY():redPlayer.getY())))
        {
            Debug.Log("Invalid move, target cell" + x + " " + y + " Current cell " + redPlayer.getX() + " " + redPlayer.getY()); 
            return false;
        }

        if(x == (isRed?bluePlayer.getX():redPlayer.getX()) && y == (isRed?bluePlayer.getY():redPlayer.getY()))
        {
            Debug.Log("Invalid move, target cell" + x + " " + y + " Current cell " + redPlayer.getX() + " " + redPlayer.getY()); 
            return false;
        }
        
        if (isRed)
        {
            // redPlayerObject.transform.position = new Vector3(x, height, y);
            redPlayer.setPos(x, y, height*0.57f);
            if (height == 3)
            {
                phase = GamePhase.RedWin;
                var animator = redPlayerObject.GetComponent<Animator>();
                animator.SetBool("isJumping", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isWinner", true);
            }
        }
        else
        {
            // bluePlayerObject.transform.position = new Vector3(x, height, y);
            bluePlayer.setPos(x, y, height*0.57f);
            if (height == 3)
            {
                phase = GamePhase.BlueWin;
                var animator = bluePlayerObject.GetComponent<Animator>();
                animator.SetBool("isJumping", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isWinner", true);
            }
        }
        return true;
    
    }

    public bool setupPlayer(bool isRed, int x, int y)
    {
        if (isRed)
        {
            redPlayerObject.transform.position = new Vector3(x, 0f, y);
            redPlayer.setPos(x, y, 0);
        }
        else
        {
            if (x == redPlayer.getX() && y == redPlayer.getY())
            {
                return false;
            }
            bluePlayerObject.transform.position = new Vector3(x, 0f, y);
            bluePlayer.setPos(x, y, 0);
        }
        return true;
    }



    
    // Start is called before the first frame update
    void Start()
    {
        phase = GamePhase.RedSetup;
        // redPlayer.setPos(0, 0);
        // bluePlayer.setPos(4, 4);
        // redPlayerObject.transform.position = new Vector3(0, 1, 0);
        // bluePlayerObject.transform.position = new Vector3(4, 1, 4);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
