using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBuff : MonoBehaviour
{
    public int DamageAmount = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerManager>().AddDamage(DamageAmount);
            Destroy(this.gameObject);
        }
    }
}
