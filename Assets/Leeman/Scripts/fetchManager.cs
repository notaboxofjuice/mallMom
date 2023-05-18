using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fetchManager : MonoBehaviour
{
    // VARIABLES
    [Header("Target Objs")]
    public GameObject npcBody;
    public GameObject targetFetch;
    // METHODS
    private void Start() {
        gameManager.GetInstance().fetchTotal += 1; // increment fetch total
    }
    public void FetchPickup() {
        Debug.Log("Picked up fetch");
        targetFetch.transform.SetParent(GameObject.FindWithTag("Player").transform); // set fetch parent to player
        targetFetch.transform.localPosition = new Vector3(0, 0, 1); // set fetch position
        gameManager.GetInstance().heldFetch = targetFetch; // player's fetch is target fetch
        gameManager.GetInstance().FetchUI(); // update fetch UI
    }
    public void TryFetch() {
        if (gameManager.GetInstance().heldFetch == targetFetch) { // if current fetch is target fetch
            Debug.Log("Correct fetch");
            gameManager.GetInstance().AddFetch(); // increment fetched total
            npcBody.GetComponent<audioHandler>().SetClips("positive"); // set audio clips to positive
            gameManager.GetInstance().heldFetch = null; // clear held fetch
            gameManager.GetInstance().occupied = false; // player is unoccupied
            Destroy(targetFetch); // destroy fetch
            Destroy(this); // no double-dipping
        } else {
            Debug.Log("Wrong fetch");
            npcBody.GetComponent<audioHandler>().SetClips("negative"); // set audio clips to negative
        }
    }
}