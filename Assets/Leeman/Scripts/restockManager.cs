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
        boxHere.transform.parent = GameObject.FindWithTag("Player").transform; // player carries box
        boxHere.GetComponent<BoxCollider>().enabled = false; // disable box collider
        Destroy(boxHere.GetComponent<Rigidbody>()); // destroy rigidbody
        boxHere.transform.position = new Vector3(0, -100, 0); // box to center of player
        gameManager.GetInstance().currentBox = boxHere; // set boxHere as currentBox
        gameManager.GetInstance().RestockUI();
    }
    public void tryRestock() 
    { // check if can restock
        if (gameManager.GetInstance().currentBox == boxHere)
        { // if correct box
            Debug.Log("Correct Box");
            Destroy(boxHere); // get rid of box
            boxHere = null; // player has no box
            gameManager.GetInstance().AddShelf(); // increment restocked shelf counter
            // update shelf mesh (comes later)
            gameManager.GetInstance().occupied = false; // player is unoccupied
        } else { // if wrong box
            Debug.Log("Wrong Box");
        }
    } 
}