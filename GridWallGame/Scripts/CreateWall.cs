using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWall : MonoBehaviour {
    /// <summary>
    /// script for creating the empty instances of the cubes for triggering
    /// </summary>
    /// <author name='Surya teja Jogi'></author>
    public int Xmax = 15;
    public int Ymax = 20;
    public int XMin = 0;
    public int YMin=0;
    public int Zmax = -1;
    public int Zmin = -15;
    public GameObject GenerateWallPrefab;
    public int[,] flag;
   
    GameObject newEmpty;
	// Use this for initialization
	void Start () {
        GenerateWall();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void GenerateWall()
    {
        int counter = 0;
        for (YMin=0; YMin <= 15; YMin++)
        {
            for (XMin =0; XMin <= 10; XMin++)
            {
                for (Zmin = -11; Zmin <= 0; Zmin++)
                {
                    Vector3 a = new Vector3(XMin, YMin, Zmin);
                    newEmpty = Instantiate(GenerateWallPrefab, a, Quaternion.identity) as GameObject;
                    newEmpty.transform.parent = transform;
                    newEmpty.gameObject.name = "emptyGame " + counter;
                    counter++;
                }

            }
        }
    }

    public void isFull()
    {
        foreach(Transform child in transform)
        {
            Vector3 pos = FindObjectOfType<Game>().Round(child.position);
            for(YMin=1;YMin<Ymax;YMin++)
            {
                for (XMin = 0; XMin < Xmax;XMin++)
                {
                    if(newEmpty.transform.position.x==child.transform.position.x && newEmpty.transform.position.y==child.transform.position.y)
                    {
                        flag[XMin, YMin] = 1;
                        Debug.Log("1");
                    }
                    else
                    {
                        flag[XMin, YMin] = 0;
                        Debug.Log("0");
                    }
                }
            }
        }
    }


    
}
