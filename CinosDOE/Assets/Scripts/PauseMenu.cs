using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject shopMenu;
    public GameObject shop;
    public GameObject cam;
    public GameObject gameplayUI;
    private ShopScript shopScript;
    private MouseMovement cameraScript;

    void Start()
    {
        shopScript = shop.GetComponent<ShopScript>();
        cameraScript = cam.GetComponent<MouseMovement>();
    }

    public void Resume() {
        if (shopScript.inShop == true)
        {
            shopMenu.SetActive(true);
        }
        pauseMenu.SetActive(false);
        gameplayUI.SetActive(true);
        Time.timeScale = 1f;
        GamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Pause() {
        if (shopMenu.activeSelf == true)
        {
            shopMenu.SetActive(false);
        }
        pauseMenu.SetActive(true);
        gameplayUI.SetActive(false);
        Time.timeScale = 0f;
        GamePaused = true;
        Cursor.lockState = CursorLockMode.None;


    }

    public void ExitToMenu() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void GoToOptions()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void GoToPause()
    {
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void Sensitivity()
    {
        string text = optionsMenu.transform.GetChild(2).gameObject.GetComponent<InputField>().text;
        
        int value = int.Parse(text);
        Debug.Log("value: " + value.ToString());
        cameraScript.horizontalSpeed = value;
        cameraScript.verticalSpeed = value;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GamePaused)
            {
                Resume();
            }
            else {
                Pause();
            }
        }
        
    }
}
