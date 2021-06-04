using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class Deathmenu : MonoBehaviour
{
  
    public void opendeath()
    {
        gameObject.SetActive(true);
        Time.timeScale = 1f;
    }

    public void mainmenubutton()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene("Main Menu");
    }

    public void restartbutton()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
