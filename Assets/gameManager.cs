using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{


    GameObject[] enemies;
  

    // Update is called once per frame
    void Update()
    {
        enemyTracker();
    }

    void enemyTracker()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
       
    }
}
