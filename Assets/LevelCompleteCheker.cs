using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteCheker : MonoBehaviour
{

    public GameObject[] Enemies;
    public GameObject Portal;
    public int DeadEnemies = 0;

    private void Awake()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Portal = GameObject.FindGameObjectWithTag("Protal");
        Portal.SetActive(false);
    }

    private void Start()
    {
        Portal.SetActive(false);
    }

    private void Update()
    {
        CheckLevelComplete();
        if (DeadEnemies == Enemies.Length)
        {
            Portal.SetActive(true);
        }
    }
    private void FixedUpdate()
    {

    }

    private void CheckLevelComplete()
    {
        for (int i = 0; i < Enemies.Length; i++)
        {
            try
            {
                if (!Enemies[i].GetComponent<EnemyManager>().isAlive)
                {
                    DeadEnemies++;
                    Enemies[i] = null;
                }
            }
            catch
            {

            }

        }
    }
}
