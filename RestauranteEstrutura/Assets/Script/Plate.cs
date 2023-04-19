using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public PlateStack thisPlateStack;

    void Awake()
    {
        thisPlateStack = new PlateStack("a", MealStackInfo.PlateType.Hamburguer);    
    }
}
