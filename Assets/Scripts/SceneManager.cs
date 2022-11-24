using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using sceneMgr = UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private static SceneManager instacne = null;
    public static SceneManager getInstance()
    {
        if(instacne == null)
        {
            instacne = new SceneManager();
            return instacne;
        } else
        {
            return instacne;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onGameEnd()
    {
        Time.timeScale = 0;
        DontDestroyOnLoad(this);
        sceneMgr.SceneManager.LoadScene("Lobby", sceneMgr.LoadSceneMode.Single);
    }
}
