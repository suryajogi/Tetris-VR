using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
    /// <summary>
    /// script for pausing the game
    /// </summary>
    /// <author name='Surya teja Jogi'></author>
    public GameObject canvas;
   // public GameObject grid;
    bool paused;
    public GameObject cam;
	// Use this for initialization
	void Start () {
        paused = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("A button"))
        {
            paused = !paused;
            if (paused)
            {
                Pause();
            }
            else if (!paused)
            {
                Resume();
            }
        }else if (Input.GetKeyDown(KeyCode.A))
        {
            paused = !paused;
            if (paused)
            {
                Pause();
            }
            else if (!paused)
            {
                Resume();
            }
        }

    }
   
   public void Pause()
    {
       // cam.transform.position = new Vector3();
        canvas.SetActive(true);
        //grid.SetActive(false);
        Time.timeScale = 0;
    }
    public void Resume()
    {
       // cam.transform.position = new Vector3(5, 5, -10);
        canvas.SetActive(false);
        //grid.SetActive(true);
        Time.timeScale = 1;
    }
}
