using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fetchManager : MonoBehaviour
{
    // VARIABLES
    [Header("Target Objs")]
    public GameObject npcBody;
    public GameObject targetFetch;
    [Header("Ink Files")]
    [SerializeField] TextAsset defaultInk;
    [SerializeField] TextAsset negativeInk;
    [SerializeField] TextAsset satisfiedInk;
    // METHODS
    private void Start() {
        gameManager.GetInstance().fetchTotal += 1; // increment fetch total
        npcBody.GetComponent<dialogueTrigger>().inkJSON = defaultInk; // set ink to default
    }
    public void FetchPickup() {
        gameManager.GetInstance().heldFetch = targetFetch; // player's fetch is target fetch
        gameManager.GetInstance().FetchUI(); // update fetch UI
    }
    public void TryFetch() {
        if (gameManager.GetInstance().heldFetch == targetFetch) { // if current fetch is target fetch
            Debug.Log("Correct fetch");
            gameManager.GetInstance().AddFetch(); // increment fetched total
            npcBody.GetComponent<dialogueTrigger>().inkJSON = satisfiedInk;
            Destroy(this); // no double-dipping
        } else {
            Debug.Log("Wrong fetch");
            npcBody.GetComponent<dialogueTrigger>().inkJSON = negativeInk;
        }
        gameManager.GetInstance().heldFetch = null; // clear held fetch
        gameManager.GetInstance().occupied = false; // player is unoccupied
        gameManager.GetInstance().FetchUI(); // update UI
    }
}