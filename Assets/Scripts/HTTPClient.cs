using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONData
{
    public string player;
    public string action;
    public int x;
    public int y;
    public int height;
    public int tier;
    public string team;
    public string phase;
    public string winner;
}

public class HTTPClient : MonoBehaviour
{
    // Start is called before the first frame update

    public string url = "http://localhost:5000";




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
