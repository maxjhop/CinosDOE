using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pauseMenu;
    public GameObject shopMenu;
    public GameObject shop;
    private ShopScript shopScript;

    void Start()
    {
        shopScript = shop.GetComponent<ShopScript>();
    }

    public void Resume() {
        if (shopScript.inShop == true)
        {
            shopMenu.SetActive(true);
        }
        pauseMenu.SetActive(false);
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
        Time.timeScale = 0f;
        GamePaused = true;
        Cursor.lockState = CursorLockMode.None;


    }

    public void ExitToMenu() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        GamePaused = false;
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
