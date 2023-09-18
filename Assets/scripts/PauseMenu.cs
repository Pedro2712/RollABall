using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject ReloadPanel;
    public GameObject QuitPanel;

    // Update is called once per frame
    void Start()
    {
       PausePanel.SetActive(false);
       ReloadPanel.SetActive(false);
       QuitPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Time.timeScale == 1) {
                Pause();
            } else {
                Continue();            
            }
        
        }
    }

    public void Pause() {
        PausePanel.SetActive(true);
        ReloadPanel.SetActive(true);
        QuitPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        ReloadPanel.SetActive(false);
        QuitPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Reload()
    {
        PausePanel.SetActive(false);
        ReloadPanel.SetActive(false);
        QuitPanel.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        PausePanel.SetActive(false);
        ReloadPanel.SetActive(false);
        QuitPanel.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
