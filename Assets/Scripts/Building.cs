using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private int tier = 0;
    private int xPosition;
    private int yPosition;

    // List of 3d models for each tier
    public GameObject[] tierModels = new GameObject[5];

    
    public int getTier()
    {
        return tier;
    }

    public int setTier(int t)
    {
        tier = t;
        // Change the model to the correct tier
        for (int i = 0; i < tierModels.Length; i++)
        {
            if (i == t)
            {
                tierModels[i].SetActive(true);
            }
            else
            {
                tierModels[i].SetActive(false);
            }
        }

        return 0;
    }

    public int setPos(int x, int y)
    {
        xPosition = x;
        yPosition = y;
        return 0;
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
        // Set the building to the first tier
        // setTier(0);

        
    }

    // Update is called once per frame
    private int i = 0;
    void FixedUpdate()
    {
    //     // Iterate through the tier models every 5 seconds
    //     if (i == 50)
    //     {
    //         i = 0;
    //         setTier((getTier() + 1) % 4);
    //     }
    //     i++;
    }
}
