using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Functions : MonoBehaviour {

    bool paused = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Pause()
    {
        if (paused)
        {
            //resumes the game
            paused = false;
            Time.timeScale = 1;
        } else
        {
            paused = true;
            GetComponentInChildren<Text>().text = "▷";
            Time.timeScale = 0;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
