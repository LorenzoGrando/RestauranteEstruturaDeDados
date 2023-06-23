using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree
{
    public bool leaf = false;
    public Tree optionTrue;
    public Tree optionFalse;
    public MealStackInfo.PlateType returnPlateType;
    public int returnRequestSize;
    public System.Func<Customer.CustomerColor, Customer.CustomerShape, Customer.CustomerType, int, bool> condition;
}
