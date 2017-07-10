using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myMusicManager : MonoBehaviour {
    /// <summary>
    /// script for volume control
    /// </summary>
    /// <author name='Surya teja Jogi'></author>
    public GameObject slider;
    public Slider volumeSlider;
    public AudioSource myAudio;
	// Update is called once per frame
	void Update () {
        myAudio.volume = volumeSlider.value;
	}
    public void OnMusicClick()
    {
        slider.SetActive(true);

    }
}
