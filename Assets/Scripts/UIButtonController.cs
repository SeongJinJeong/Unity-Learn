using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickRestart()
    {
        Time.timeScale = 0;
        GameManager.getInstance().endGame();
        GameManager.getInstance().startGame();
        Time.timeScale = 1;
    }

    public void onClickHome()
    {
        Time.timeScale = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
    }
}
