using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        Debug.Log("Collision");
        if(other.gameObject.tag == "Player"){
            Debug.Log("Player");
            SceneManager.LoadScene(1);
        }
    }
}
