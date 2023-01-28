using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class debugButton : MonoBehaviour
{
    // Start is called before the first frame update
    bool dButton;

    void Update()
    {
        dButton = Input.GetKey("p");
        debug();
    }


    public void OnClickRestart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Button working");
    }

    void debug()
    {
        if (dButton)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }
}
