using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    public string sceneToLoad;

    GameObject[] enemies;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        enemyTracker();
    }

    void enemyTracker()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wizzard" && enemies.Length == 0)
        {
            Debug.Log("Scene Transition");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
