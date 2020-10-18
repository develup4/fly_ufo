using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour {
    private bool isPaused = false;

    void OnPauseButtonClick()
    {
		audio.Play ();

        if(!isPaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        isPaused = !isPaused;
    }
}
