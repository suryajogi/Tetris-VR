using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    /// <summary>
    /// main code which spawns new prefabs and keeps the block to be inside the grid
    /// </summary>
    /// <author name='Surya teja Jogi'></author>

    public GameObject rulesText;
	public static int xRange = 9;
    public static int yRange = 15;
    public static int zRange = 0;
    public static Transform[,] gridXY = new Transform[9, 14];
    public static Transform[,] gridYZ = new Transform[14, 9];
    public GameObject[] Blocks;
    public Transform[] spawnLocations;
    // Use this for initialization
	void Start () {
        rulesText.SetActive(true);
        //spawnObject();
	}
	public void SetToFalse()
    {
        rulesText.SetActive(false);
        spawnObject();
    }
	// Update is called once per frame
	void Update () {
		
	}




    // This method checks the block to be inside the grid
    public bool checkIsInsideGrid(Vector3 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x <= xRange && (int)pos.y >= 1 && (int)pos.z >= -10 && (int)pos.z <= zRange);
    }
    //This method is used to round the vector3 positions 
    public Vector3 Round (Vector3 pos)
    {
        return new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
    }


    public void spawnObject()
    {
       
        int i = Random.Range(0, spawnLocations.Length);

        int b = Random.Range(0, Blocks.Length);
        GameObject block =Instantiate(Blocks[b], spawnLocations[i].position, Quaternion.identity) as GameObject;

        block.GetComponent<Tetrimino>().side = (Tetrimino.Side)i;

        if (i == 0)
        {
            block.transform.Rotate(0, 90, 0);
            block.GetComponent<Tetrimino>().movementVector = new Vector3(0, 0, 1);
        }
        else if (i == 2)
        {
            block.transform.Rotate(0, 90, 0);
            block.GetComponent<Tetrimino>().movementVector = new Vector3(0, 0, -1);
        }



    }
   
    public void CheckForGameOver()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Stationary");
        foreach(GameObject child in objects)
        {
if(child.transform.position.y==15)
            {
            
               SceneManager.LoadScene("GameOver");
                //  Application.LoadLevel();
            }
        }

    }
 /* public void ClearLines()
    {
        GameObject[] emptyGameObjects= GameObject.FindGameObjectsWithTag("E Cube");
        int count = 0;
        for(int y=0;y<15;y++)
        {
            for(int x=0;x<10;x++)
            {
               foreach(GameObject objects in emptyGameObjects)
                {
                  //  if(GetComponent<EmptyCube>().flag==1)
                   // {
                        Debug.Log("i am full");
                        count++;
                   // }
                }
               
            }
            Debug.Log("ready to get deleted");
        }
    }


    */




    /*  public void SpawnNextTetrimino()
      {
          GameObject nextTetrimino=(GameObject)Instantiate(Resources.Load(GetRandomTetrimino(),typeof(GameObject)),new Vector3
      }
      */

    /*  string GetRandomTetrimino()
      {
          int randomTetrimino = Random.Range(1, 8);
          string randomTetriminoName = "Block_T";
          switch(randomTetrimino)
          {
              case 1:
                  randomTetriminoName = "Block_T";
                  break;
              case 2:
                  randomTetriminoName = "Block_L";
                  break;
              case 3:
                  randomTetriminoName = "Block_O";
                  break;
              case 4:
                  randomTetriminoName = "Block_Z";
                  break;
              case 5:
                  randomTetriminoName = "Block_S";
                  break;
              case 6:
                  randomTetriminoName = "Block_J";
                  break;
              case 7:
                  randomTetriminoName = "Block_I";
                  break;

          }
          return randomTetriminoName;
      }
      */


    //update grid code
    /*
     * 
    public Transform getTransformAtGridPosition(Vector3 pos)
    {
        if(pos.y>14)
        {
            return null;
        }
      
       
        else
        {
            return gridXY[(int)pos.x, (int)pos.y];
            return gridYZ[(int)pos.y, (int)pos.z];

        }
    }



    public void UpdateGrid(Tetrimino tetrimino)
    {
        for(int y=1;y<15;y++)
        {
            for(int x=0;x<=10;x++)
            {
                if(gridXY[x,y] !=   null)
        {
        if(gridXY[x,y].parent==tetrimino.transform)
                    {
                        gridXY[x, y] = null;
                    }
        }
            }
        }

        for (int y = 1; y < 15; y++)
        {
            for (int z = -10; z < 0; z++)
            {
                if (gridYZ[y,z] != null)
                {
                    if (gridYZ[y,z].parent == tetrimino.transform)
                    {
                        gridYZ[y,z] = null;
                    }
                }
            }
        }

        foreach (Transform mino in tetrimino.transform)
        {
            Vector3 pos = Round(mino.position);
            if(pos.y<15)
            {
                gridXY[(int)pos.x, (int)pos.y] = mino;
                gridYZ[(int)pos.y, (int)pos.z] = mino;
            }
        }
    }


    else if(Input.GetKeyDown(KeyCode.Space))
        {

         
          
            switch (side)
            {
                case Side.left:


                    side = Side.middle;
                    transform.position = new Vector3(-(transform.position.z)-1, transform.position.y, 0);
                    transform.Rotate(0, 90, 0);
                    movementVector = new Vector3(1, 0, 0);
                    if (CheckIsValidPosition() && !InsideStationary())
                    {

                    }
                    else
                    {
                        side = Side.left;
                        transform.position = new Vector3(0, transform.position.y, transform.position.z);
                        transform.Rotate(0, -90, 0);
                        movementVector = new Vector3(0, 0, 1);
                    }

                    break;

                case Side.middle:
                    side = Side.right;
                    transform.position = new Vector3(9, transform.position.y, -(transform.position.x)-1);
                    transform.Rotate(0, 90, 0);
                    movementVector = new Vector3(0, 0, -1);
                    if (CheckIsValidPosition() && !InsideStationary())
                    {

                    }
                    else
                    {
                        side = Side.middle;
                        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                        transform.Rotate(0, -90, 0);
                        movementVector = new Vector3(1, 0, 0);
                    }

                    break;
                case Side.right:
                    side = Side.left;
                    transform.position = new Vector3(0, transform.position.y, transform.position.z+1);
                    //transform.Rotate(0, -90, 0);
                    movementVector = new Vector3(0, 0, 1);
                    if (CheckIsValidPosition() && !InsideStationary())
                    {

                    }
                    else
                    {
                        side = Side.right;
                        transform.position = new Vector3(0, transform.position.y, transform.position.z);
                        //transform.Rotate(0, -90, 0);
                        movementVector = new Vector3(0, 0, -1);
                    }

                    break;
            }
           
        }
     */
}
