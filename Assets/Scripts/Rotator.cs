using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public enum RotationState
{
    Clockwise,
    CounterClockwise,
    Idle
}

public class Rotator : MonoBehaviour
{
    public GameObject board;
    public GameController gameController;
    private RotationState state = RotationState.Idle;
    public float rotationSpeed = 10;
    public float rotationStep = 45;

    private float rotationLeft = 0;

    public void rotateClockwise()
    {
        if (state == RotationState.Idle)
        {
            state = RotationState.Clockwise;
            rotationLeft = rotationStep;
        }
        
    }

    public void rotateCounterClockwise()
    {
        if (state == RotationState.Idle)
        {
            state = RotationState.CounterClockwise;
            rotationLeft = rotationStep;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        this.transform.LookAt(board.transform.position + new Vector3(0f,-4f,0f));
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            rotateCounterClockwise();
        }
        if (Input.GetKeyDown("q"))
        {
            rotateClockwise();
        }

         // On mouse click get the object clicked
        
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse clicked");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject objectHit = hit.transform.gameObject;

                Debug.Log("Clicked on " + objectHit.name);
                Debug.Log("Phase is " + gameController.getPhase());
                
                if(gameController.getPhase() == GamePhase.BlueBuildTurn || gameController.getPhase() == GamePhase.RedBuildTurn)
                {
                    if (objectHit != null)
                    {
                        Debug.Log("Building floor");
                    // Get parent of the object clicked
                        GameObject parent = objectHit.transform.parent.gameObject;
                        if (parent == null)
                        {
                            Debug.Log("No parent found");
                            return;
                        }
                        Building building = parent.GetComponent<Building>();
                        
                        if (building != null)
                        {
                            int xPos = building.getX();
                            int yPos = building.getY();

                            // Get board state
                            BoardState boardState = board.GetComponent<BoardState>();
                            

                            // Build a floor
                            bool res = boardState.buildFloor(xPos, yPos);
                            if(res) gameController.nextPhase();

                            Debug.Log("Building floor at " + xPos + " " + yPos);
                        }
                        else
                        {
                            Debug.Log("No building script found");
                        }
                    }
                }
                else if (gameController.getPhase() == GamePhase.RedMoveTurn || gameController.getPhase() == GamePhase.BlueMoveTurn)
                {
                   Debug.Log("Moving player"); 
                    // Get parent of the object clicked
                    GameObject parent = objectHit.transform.parent.gameObject;
                    if (parent == null)
                    {
                        Debug.Log("No parent found");
                        return;
                    }
                    Building building = parent.GetComponent<Building>();

                    if (building != null)
                    {
                        int xPos = building.getX();
                        int yPos = building.getY();

                        // Move player
                        Debug.Log("Moving player to " + xPos + " " + yPos);
                        bool res = gameController.movePlayer(gameController.getPhase() == GamePhase.RedMoveTurn, xPos, yPos);
                        if(res) gameController.nextPhase();
                    }
                    else
                    {
                        Debug.Log("No building script found");
                    }
                }
                else if (gameController.getPhase() == GamePhase.RedSetup || gameController.getPhase() == GamePhase.BlueSetup)
                {
                    Debug.Log("Setting up player");
                    // Get parent of the object clicked
                    GameObject parent = objectHit.transform.parent.gameObject;
                    if (parent == null)
                    {
                        Debug.Log("No parent found");
                        return;
                    }
                    Building building = parent.GetComponent<Building>();

                    if (building != null)
                    {
                        int xPos = building.getX();
                        int yPos = building.getY();

                        // Move player
                        Debug.Log("Setting up player at " + xPos + " " + yPos);
                        bool res = gameController.setupPlayer(gameController.getPhase() == GamePhase.RedSetup, xPos, yPos);
                        if(res) gameController.nextPhase();
                    }
                    else
                    {
                        Debug.Log("No building script found");
                    }
                }
                else
                {
                    Debug.Log("Invalid phase");
                }
            }
            else 
            {
                Debug.Log("No object clicked");
            }
        }
    
    }

    void FixedUpdate()
    {
        if (state != RotationState.Idle)
        {
            if (rotationLeft > 0)
            {
                this.transform.RotateAround(board.transform.position + new Vector3(2f,0,2f), Vector3.up, (state == RotationState.Clockwise)?rotationSpeed * Time.deltaTime:-rotationSpeed * Time.deltaTime);
                rotationLeft -= rotationSpeed * Time.deltaTime;
            }
            else
            {
                state = RotationState.Idle;
            }
        }

       

    }
}
