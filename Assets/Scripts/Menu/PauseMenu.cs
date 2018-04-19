using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    
    public GameObject pauseMenuUi;
    public GameObject swipe;

	// Use this for initialization
	void Start () {
	}

    public void Resume()
    {
        Sound.loadSound("Sounds/click_button");
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        ApplicationModel.gameIsPaused= false;
    }

    public void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        ApplicationModel.gameIsPaused = true;
    }

    public void BackToMenu()
    {
        Resume();
        ApplicationModel.clear();
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
