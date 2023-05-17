using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restockManager : MonoBehaviour
{
    // EXTERNAL VARS
    public GameObject shelfHere;
    public GameObject boxHere;
    // INITIALIZATION
    private void Start() {
        gameManager.GetInstance().shelfTotal += 1;
    }
    // METHODS
    public void boxPickup()
    { // to pick up the box
        boxHere.transform.SetParent(GameObject.FindWithTag("Player").transform); // set parent to player
        //boxHere.GetComponent<BoxCollider>().enabled = false; // disable box collider
        Destroy(boxHere.GetComponent<Rigidbody>()); // destroy rigidbody
        boxHere.transform.localPosition = new Vector3(0, 0, 1); // to center of player
        gameManager.GetInstance().currentBox = boxHere; // set boxHere as currentBox
        gameManager.GetInstance().RestockUI();
    }
    public void tryRestock() 
    { // check if can restock
        if (gameManager.GetInstance().currentBox == boxHere)
        { // if correct box
            Debug.Log("Correct Box");
            boxHere.transform.SetParent(shelfHere.transform); // new daddy
            boxHere.transform.localPosition = new Vector3(0, 0, 0); // to center of shelf
            boxHere = null; // player has no box
            gameManager.GetInstance().AddShelf(); // increment restocked shelf counter
            gameManager.GetInstance().occupied = false; // player is unoccupied
        } else { // if wrong box
            Debug.Log("Wrong Box");
        }
    } 
}