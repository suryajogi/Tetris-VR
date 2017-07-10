using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCube : MonoBehaviour {
    /// <summary>
    /// onTrigger event for the empty cubes 
    /// </summary>
    /// <author name='Surya teja Jogi'></author>

    public bool isFull = false;
    public int flag = 0;
    //bool running = false;

	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		//if(running)
  //      {
            
  //      }
	}

    void OnTriggerStay(Collider collider)
    {
        if (!isFull)
        { 
            if (collider.gameObject.tag == "Stationary" && collider.transform.position == transform.position)
            {
                
                    Debug.Log(string.Format(" I'm cube [{0}] I got marked as full!", gameObject.name));
                    isFull = true;
                flag = 1;
            }
        }
    }


   public void clearLines()
    {

       // FindObjectOfType<CreateWall>().isFull();
        
        
        GameObject[] emptyGameObjects = GameObject.FindGameObjectsWithTag("E Cube");
        int count = 0;
       
            for (int y = 1; y < 15; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                foreach (GameObject objects in emptyGameObjects)

                {
                    if (objects.transform.position==new Vector3(x,y,0) && objects.GetComponent<EmptyCube>().isFull)
                    {



                            count++;
                            Debug.Log("count");
                     
                    }
                }
                //Debug.Log("count");
                
            }
            Debug.Log("delete me");
           
        }
        
    }


    //void OnTriggerExit(Collider collider)
    //{
    //    running = false;
    //}
}
