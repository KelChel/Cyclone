using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour, IDamagable
{

    public static int playerHealth;
    public static bool gameOver;
    public TextMeshProUGUI playerHealthText;

    public DamageScript damageScript;

    void Start()
    {
        playerHealth = 100;
        gameOver = false;
    }

    void Update()
    {
        if (gameOver)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void OpenDamageCollider()
    {
        damageScript.EnableDamageCollider();
    }
    public void CloseDamageCollider()
    {
        damageScript.DisableDamageCollider();
    }

    public void TakeDamage(int damageAmount)
    {
        playerHealth -= damageAmount;

        if (playerHealth <= 0)
        {
            gameOver = true;
            playerHealth = 0;
        }

        playerHealthText.text = "" + playerHealth;

    }
}
