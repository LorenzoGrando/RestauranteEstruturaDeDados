using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatManager : MonoBehaviour
{
    public Chair[] sceneChairs;

    void Start()
    {
        sceneChairs = new Chair[FindObjectsOfType<Chair>().Length];
        sceneChairs = FindObjectsOfType<Chair>();
    }

    void AddCustomerToChair(){

    }

    public bool HasAvailableChairs() {
        foreach(Chair chair in sceneChairs) {
            if(chair.isOccupied == false) {
                return true;
            }
        }
        return false;
    }
}

