using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CubeManager : MonoBehaviour {

    /// <summary>
    /// Script for deleting the rows and updating the score
    /// </summary>
    /// <author name='Surya Teja Jogi'></author>
    /// 
    [SerializeField]
    public Text scoreText;
    public int xOffset = 0;
    public int yOffset = 0;
    public int zOffset = 10;
    public int lineCount = 0;
    public int score = 0;
    private bool[,,] occupide = new bool[10, 17, 11];
    public int linesToClear;

    public void Start()
    {
        scoreText.text = "Score: " + score;
    }
    public void UpdateGameBoard()
    {
        //Updates the array of cubes currently on the board
        UpdateOccupied();
        ClearCubes();

    }

    private void ClearCubes()
    {
        //Mark the cubes for deletion
        List<Vector3> cubesToDestroy = MarkCubesToDelete();

        if (cubesToDestroy.Count != 0)
        {
            //Get stationary cubes on the board
            GameObject[] cubes = GameObject.FindGameObjectsWithTag("Stationary");

            foreach(Vector3 destroyCube in cubesToDestroy)
            {
                //Destroy the cube
                foreach (GameObject cube in cubes)
                {
                    if(cube.transform.position == new Vector3(destroyCube.x - xOffset,destroyCube.y -yOffset,destroyCube.z -zOffset))
                    {
                        DestroyObject(cube);
                    }
                }
               
                //Shift column related to cube
                ShiftColumnDown(new Vector3(destroyCube.x - xOffset, destroyCube.y - yOffset, destroyCube.z - zOffset));
            }
            UpdateScore();
          // lineCount++;
            CheckForLevelComplete();
        }
        
    }
    public void CheckForLevelComplete()
    {
        if (lineCount >= linesToClear)
        {
            SceneManager.LoadScene("levelCleared");
        }
    }

    public void UpdateScore()
    {

        score = score + 100;
        scoreText.text = "Score: " + score;
    }

    private void ShiftColumnDown(Vector3 position)
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Stationary");
        
        foreach(GameObject cube in cubes)
        {
            if(cube.transform.position.x == position.x && cube.transform.position.y > position.y && cube.transform.position.z == position.z)
            {
                cube.transform.position += new Vector3(0, -1, 0);
            }
        }
        lineCount++;
    }

    private void UpdateOccupied()
    {
        //clear current values in the array
        ClearOccupide();

        //find all stationary cubes
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Stationary");

        //repopulate array
        foreach (GameObject cube in cubes)
        {

            if (transform.position.y < 17)
            {
                occupide[Convert.ToInt32(cube.transform.position.x) + xOffset, Convert.ToInt32(cube.transform.position.y) + yOffset, Convert.ToInt32(cube.transform.position.z) + zOffset] = true;
            }
        }
    }

    private void ClearOccupide()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 17; y++)
            {
                for (int z = 0; z < 11; z++)
                {
                    occupide[x, y, z] = false;
                }
            }
        }
    }

    public void DisplayOccupide()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 17; y++)
            {
                for (int z = 0; z < 11; z++)
                {
                    
                    if (occupide[x, y, z]) 
                    {
                        Debug.Log(string.Format("X {0} Y{1} Z{2}",x, y, z));
                        Debug.Log(string.Format("Without Offset X {0} Y{1} Z{2}", x - xOffset, y - yOffset, z - zOffset));
                    }
                }
            }
        }
    }

    public List<Vector3> MarkCubesToDelete()
    {
        List<Vector3> results = new List<Vector3>();

        //Check X's
        for( int y = 0; y < 15; y++)
        {
            //Check for full row
            int counter = 0;
            int z = 10;
            for (int x = 0; x < 10; x++)
            {
                if(occupide[x,y,z])
                {

                    counter++;
                }
            }

            //if row is full add to result list
            if(counter == 10)
            {
                for (int x = 0; x < 10; x++)
                {
                    results.Add(new Vector3(x, y, z));
                }
            }
        }


        //Check for Z's
        for (int y = 0; y < 15; y++)
        {
            //check left
            int counter = 0;
            int x = 0;
            for (int z = 0; z < 11; z++)
            {
                if(occupide[x,y,z])
                {
                    counter++;
                }
            }
            
            //if left full add to results
            if( counter == 11)
            {
                for (int z = 0; z < 11; z++)
                {
                    results.Add(new Vector3(x, y, z));
                }
            }

            //check right
            counter = 0;
            x = 9;
            for (int z = 0; z < 11; z++)
            {
                if (occupide[x, y, z])
                {
                    counter++;
                }
            }

            //if right full add to results
            if (counter == 11)
            {
                for (int z = 0; z < 11; z++)
                {
                    results.Add(new Vector3(x, y, z));
                }
            }

        }

        return results;
    }
}
