using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBuff : MonoBehaviour
{
    public int HealAmount = 100;
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
            other.GetComponent<PlayerManager>().AddHealth(HealAmount);
            Destroy(this.gameObject);
        }
    }
}
