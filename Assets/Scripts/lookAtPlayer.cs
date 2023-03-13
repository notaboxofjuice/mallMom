using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtPlayer : MonoBehaviour
{
    private Transform player; // finding the player
    private Vector3 targetPos; // target position

    /// METHODS
    void Awake(){
        player = GameObject.FindWithTag("Player").transform; // find player transform
    }

    void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) { // if it's player
            transform.LookAt(targetPos); // look at target
            Debug.Log("Looking"); // print to console thx babe
        }
    }

    private void Update() {
        targetPos = new Vector3(player.position.x, transform.position.y, player.position.z); // only get x and z
    }

}
