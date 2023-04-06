using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour, IDamagable
{
    [SerializeField]private int initPlayerHealth = 100;
    private int playerHealth;
    public int heroDamage = 10;

    public static bool gameOver;
    public TextMeshProUGUI playerHealthText;


    private DamageScript damageScript;

    private void Awake() 
    {
        damageScript = GetComponentInChildren<DamageScript>();
    }

    void Start()
    {
        playerHealth = initPlayerHealth;
        gameOver = false;
        damageScript.damageAmount = heroDamage;
        playerHealthText.text = "" + playerHealth;
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
