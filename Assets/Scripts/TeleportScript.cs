using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TeleportScript : MonoBehaviour
{
    public string toLevelName;
    
    void OnTriggerEnter(Collider other)
    {
        //enable UI E to use
        try
        {
            other.GetComponent<PlayerController>().UseButton.SetActive(true);
        }
        catch
        {
            Debug.Log("OnTriggerEnter Error");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //disable UI E to use
        try
        {
            other.GetComponent<PlayerController>().UseButton.SetActive(false);
        }
        catch
        {
            Debug.Log("OnTriggerExit Error");
        }
    }
}
