using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tetrimino : MonoBehaviour {

    /// <summary>
    /// Code attached to cubes, it moves the cube
    /// </summary>
    /// <author name='Surya teja Jogi'></author>
    /// 
    public Text text;
    public enum Side { left,middle,right};

    public Side side = Side.middle;
    
    public float fallSpeed = 1;
    bool xboxPressed = false;

    public float timePerKey = 1f;
    public float timeSinceLastKeyPress = 1f;
   
    //We need to add offsets for each block type in their prfabs 

    public bool allowRotation = true;
    public bool limitRotation = false;

    [HideInInspector]
    public Vector3 movementVector = new Vector3(1, 0, 0);
    // Use this for initialization

    private CubeManager cubemanager;

    void Awake()
    {
        InvokeRepeating("Fall", 0, fallSpeed);
        cubemanager = GameObject.FindObjectOfType<CubeManager>();
    }

    void Start()
    {
        text = GameObject.FindObjectOfType<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        CheckUserInput();
       // CheckIfGameOver();
	}

  

        void CheckUserInput()
    {
        if(Input.GetAxis("Horizontal") < 0)
        {
           if(timeSinceLastKeyPress >= timePerKey)
            {
                transform.position += movementVector;

                if (CheckIsValidPosition() && !InsideStationary())
                {

                }
                else
                {
                    transform.position += movementVector * -1;
                }

                timeSinceLastKeyPress -= timePerKey;
            }
            else
            {
                timeSinceLastKeyPress += Time.deltaTime;
            }
        }
        else if(Input.GetAxis("Horizontal") > 0)
        {
            if(timeSinceLastKeyPress > timePerKey)
            {
                transform.position += movementVector * -1;

                if (CheckIsValidPosition() && !InsideStationary())
                {

                }
                else
                {
                    transform.position += movementVector;
                }

                timeSinceLastKeyPress -= timePerKey;
            }
            else
            {
                timeSinceLastKeyPress += Time.deltaTime;
            }
        }
        
        else if (Input.GetAxis("Vertical") > 0.001)
        {
            if(timeSinceLastKeyPress > timePerKey)
            {
                transform.position += new Vector3(0, -1, 0);

                if (CheckIsValidPosition() && !InsideStationary())
                {

                }
                else
                {
                    transform.position += new Vector3(0, 1, 0);
                }

                timeSinceLastKeyPress -= timePerKey;
            }
            else
            {
                timeSinceLastKeyPress += Time.deltaTime;
            }
        }
      
        

        //else if(Input.GetKeyUp(KeyCode.DownArrow))
        //{

        //    CancelInvoke("Fall");
        //    InvokeRepeating("Fall", 0, fallSpeed);

        //}
        else if (Input.GetKeyDown(KeyCode.R) || Input.GetButton("X button"))
        {

            if (timeSinceLastKeyPress >= timePerKey)
            {
                if (allowRotation)
                {
                    if (limitRotation)
                    {
                        if (transform.rotation.eulerAngles.z >= 90)
                        {
                            transform.Rotate(0, 0, -90);
                        }
                        else
                        {
                            transform.Rotate(0, 0, 90);
                        }
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                    if (CheckIsValidPosition() && !InsideStationary())
                    {

                    }
                    else
                    {
                        if (limitRotation)
                        {

                            if (transform.rotation.eulerAngles.z >= 90)
                            {
                                transform.Rotate(0, 0, -90);
                            }
                            else
                            {
                                transform.Rotate(0, 0, -90);

                            }
                        }
                        else
                        {
                            transform.Rotate(0, 0, -90);
                        }
                    }
                }

                timeSinceLastKeyPress -= timePerKey;
            }
            else
            {
                timeSinceLastKeyPress += Time.deltaTime;
            }
        }
        else if(Input.GetKeyDown(KeyCode.Space) || Input.GetButton("Y button"))
        {

            if (timeSinceLastKeyPress >= timePerKey)
            {
                switch (side)
                {
                    case Side.left:


                        side = Side.middle;
                        transform.position = new Vector3(-(transform.position.z) - 1, transform.position.y, 0);
                        transform.Rotate(0, 90, 0);
                        movementVector = new Vector3(1, 0, 0);
                        if (CheckIsValidPosition() && !InsideStationary())
                        {

                        }
                        else
                        {
                            side = Side.left;
                            transform.position = new Vector3(0, transform.position.y, -(transform.position.x) - 1);
                            movementVector = new Vector3(0, 0, 1);
                            transform.Rotate(0, -90, 0);
                        }

                        break;
                    case Side.middle:
                        side = Side.right;
                        transform.position = new Vector3(9, transform.position.y, -(transform.position.x) - 1);
                        transform.Rotate(0, 90, 0);
                        movementVector = new Vector3(0, 0, -1);
                        if (CheckIsValidPosition() && !InsideStationary())
                        {

                        }
                        else
                        {
                            side = Side.middle;
                            transform.position = new Vector3(-(transform.position.z) - 1, transform.position.y, 0);
                            movementVector = new Vector3(1, 0, 0);
                            transform.Rotate(0, -90, 0);
                        }



                        break;
                    case Side.right:
                        side = Side.left;
                        transform.position = new Vector3(0, transform.position.y, transform.position.z + 1);
                        //transform.Rotate(0, -90, 0);
                        movementVector = new Vector3(0, 0, 1);
                        if (CheckIsValidPosition() && !InsideStationary())
                        {

                        }
                        else
                        {
                            side = Side.right;
                            transform.position = new Vector3(9, transform.position.y, transform.position.z - 1);
                            movementVector = new Vector3(0, 0, -1);
                            transform.Rotate(0, 0, 0);
                        }


                        break;
                }

                timeSinceLastKeyPress -= timePerKey;
            }
            else
            {
                timeSinceLastKeyPress += Time.deltaTime;
            }
        }

        else
        {
            timeSinceLastKeyPress = 1f;
            Debug.Log(timeSinceLastKeyPress);
        }

    }

    bool CheckIsValidPosition()
    {
        foreach(Transform mino in transform)
        {
            Vector3 pos = FindObjectOfType<Game>().Round(mino.position);
            if(FindObjectOfType<Game>().checkIsInsideGrid(pos)==false)
            {
               // SceneManager.LoadScene("GameOver");
                return false;
               
            }
            
        }
       
        return true;
    }

    void Fall()
    {
        //CheckForCubesBelow();
        transform.position += new Vector3(0, -1, 0);
        if (CheckIsValidPosition() && !InsideStationary())
        {

        }
        else
        {
            transform.position += new Vector3(0, 1, 0);
            DisableObject();
        }
    }

 /*   public void CheckForCubesBelow()
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Stationary");


        foreach(Transform child in transform)
        {
            //FindObjectOfType<Game>() .Round(child.position);
            foreach(GameObject cube in cubes)
            {
                if (child.transform.position.x ==  cube.transform.position.x &&
                    child.transform.position.z == cube.transform.position.z &&
                    child.transform.position.y -2 == cube.transform.position.y )
                {

                    DisableObject();
                    return;
                }
            }
        }
    }
    */

    void DisableObject()
    {
        //Disable the fall
        CancelInvoke("Fall");

        //Label child cubes to stationary
        foreach (Transform child in transform)
        {
            child.transform.tag = "Stationary";
        }

        //Move to Stationary Object
        for (int i = transform.childCount - 1; i >= 0; --i)
        {
            Transform child = transform.GetChild(i);
            child.SetParent(GameObject.FindGameObjectWithTag("StationaryObjects").transform, true);
        }



        //Disable the block
        enabled = false;
        FindObjectOfType<Game>().CheckForGameOver();

        //Update stationary blocks
        cubemanager.GetComponent<CubeManager>().UpdateGameBoard();
        //cubemanager.GetComponent<CubeManager>().DisplayOccupide();

        //check for rows to delete
        //FindObjectOfType<EmptyCube>().clearLines();

        //Spawn
        FindObjectOfType<Game>().spawnObject();
    }

    bool InsideStationary()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Stationary");

        foreach(Transform child in transform)
        {
            foreach(GameObject obj in objects)
            {
                if(child.position == obj.transform.position)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
