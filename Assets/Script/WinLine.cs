using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == "Player")
        {
            other.transform.root.GetComponent<Player>().setGameComplete();
        }
    }
}
