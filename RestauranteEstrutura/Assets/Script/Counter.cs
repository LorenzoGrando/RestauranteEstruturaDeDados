using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public bool isEmpty = true;
    public GameObject plate;


    public void PlacePlate(GameObject plateInput, Player originPlayer) {
        isEmpty = false;
        plate = plateInput;
        plate.transform.parent = transform;
        plate.transform.position = transform.position;
        originPlayer.heldItem = null;
    }

    public void RemovePlate(Player originPlayer) {
        if(originPlayer.heldItem == null) {
            isEmpty = true;
            originPlayer.heldItem = plate;
            plate = null;
        }
    }
}
