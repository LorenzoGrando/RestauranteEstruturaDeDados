using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour {
    public bool isOccupied = false;
    public GameObject seatedCustomer;

    public Chair() {
        this.isOccupied = false;
        this.seatedCustomer = null;
    }
}
