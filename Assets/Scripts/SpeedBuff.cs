using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff : MonoBehaviour
{
    public float SpeedAmount = 2f;
    

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerManager>().AddSpeed(SpeedAmount);
            Destroy(this.gameObject);
        }
    }
}
