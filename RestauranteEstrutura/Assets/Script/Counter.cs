using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public bool isEmpty = true;
    public PlateStack plate;


    void PlacePlate(PlateStack plateInput, Player originPlayer) {
        isEmpty = false;
        plate = plateInput;
        originPlayer.heldPlate = null;
    }

    void RemovePlate(Player originPlayer) {
        if(originPlayer.heldPlate == null) {
            isEmpty = true;
            originPlayer.heldPlate = plate;
            plate = null;
        }
    }
}
