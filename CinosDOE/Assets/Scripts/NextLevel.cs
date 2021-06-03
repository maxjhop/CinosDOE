using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextLevel : MonoBehaviour
{
    public GameObject cinos;
    public Vector3 nextLevelLocation;

    // Start is called before the first frame update
    void Start()
    {
        nextLevelLocation = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Vector3.Distance(cinos.transform.position, nextLevelLocation));
        if (Vector3.Distance(cinos.transform.position, nextLevelLocation) < 3f)
        {
            if (SceneManager.GetActiveScene().name == "TutorialScene")
            {
                SceneManager.LoadScene("LevelTwo");
            }
            else if (SceneManager.GetActiveScene().name == "LevelOne")
            {
                SceneManager.LoadScene("LevelTwo");
            }
            else if (SceneManager.GetActiveScene().name == "LevelTwo")
            {
                SceneManager.LoadScene("LevelThree");
            }
        }
    }
}
