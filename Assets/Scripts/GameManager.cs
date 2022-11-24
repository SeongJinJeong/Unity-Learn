using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameStart = false;
    int currStage = 0;
    int enemiesCount = 0;
    int spawnCount = 0;
    float spawnDelay = 1.0f;

    List<GameObject> enemies = new List<GameObject>() { };

    Dictionary<string, float> stageData = new Dictionary<string, float>()
    {
        {"stage0",10 },
        {"stage1",15 },
        {"stage2",20 },
        {"stage3",30 }
    };


    private static GameManager instance;
    public static GameManager getInstance()
    {
        if(instance == null)
        {
            instance = new GameManager();
        }

        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }

        startGame();
    }

    void startGame()
    {
        float enemyCount = stageData["stage" + currStage];

        makePlayer();
        makeEnemy(enemyCount);

        StartCoroutine(makeEnemy(enemyCount));
    }
    void makePlayer()
    {
        GameObject player = Resources.Load<GameObject>("Prefabs/Players/Player");
        Instantiate(player);
        player.SetActive(true);
    }

    IEnumerator makeEnemy(float enemyCount)
    {
        for(int i=0; i<enemyCount; i++)
        {
            yield return new WaitForSeconds(1f);
            GameObject enemy = null;
            int random = UnityEngine.Random.Range(0, 3);
            switch (random)
            {
                case 0:
                    enemy = Resources.Load("Prefabs/Enemy/Ghost") as GameObject;
                    break;
                case 1:
                    enemy = Resources.Load("Prefabs/Enemy/Zombie") as GameObject;
                    break;
                case 2:
                    enemy = Resources.Load("Prefabs/Enemy/Boss") as GameObject;
                    break;
            }

            Instantiate(enemy);
            enemies.Add(enemy);
            for(int j=0; j<enemies.Count; j++) {
                Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), enemies[j].GetComponent<Collider2D>());
            }
            gameStart = true;
        }
    }

    public void checkEnemyCount()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && gameStart == true)
        {
            stageClear();
        }
    }

    void stageClear()
    {
        GameObject.FindGameObjectWithTag("ClearText").GetComponent<TextMeshProUGUI>().enabled = true;
    }

   

    void endGame()
    {
        resetAll();
        SceneManager.getInstance().onGameEnd();
    }

    void resetAll()
    {
        enemiesCount = 0;
        spawnDelay = 1.0f;
        currStage = 0;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(var i=0; i<enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }

        Destroy(GameObject.FindGameObjectWithTag("Player"));
    }
}
