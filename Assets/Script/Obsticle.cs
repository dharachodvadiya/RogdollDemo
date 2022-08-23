using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticle : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.transform.root.name);

        if(collision.transform.root.tag == "Player")
        {
            collision.transform.root.GetComponent<Player>().setGameOver(collision.gameObject.GetComponent<Rigidbody>());
        }
    }
}
