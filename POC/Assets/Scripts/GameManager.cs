using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CinemachineVirtualCamera currentCam;
    Scene currentScene;
    bool paused = false;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] FollowPointRotation fpRotation;

    private void Awake()
    {
        instance = this;
        currentScene = SceneManager.GetActiveScene();
    }

    private void Start()
    {
        LockMouse();
    }

    // Update is called once per frame
    void Update()
    {
        PauseMenu();
    }

    void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused || Input.GetKeyDown(KeyCode.Joystick1Button7) && !paused)
        {
            Time.timeScale = 0;
            FreeMouse();
            pauseCanvas.SetActive(true);
            paused = true;
            fpRotation.camRotate = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused || Input.GetKeyDown(KeyCode.Joystick1Button7) && paused)
        {

            Time.timeScale = 1;
            pauseCanvas.SetActive(false);
            LockMouse();
            paused = false;
            fpRotation.camRotate = true;
        }
    }

    void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FreeMouse()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        fpRotation.camRotate = false;
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
