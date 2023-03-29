using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//gave bad name, should prolly be reverse between this and the next class;
public class PlateStack
{
    public string stackName;
    public Stack<MealStackInfo.HamburguerIngredient> hamburguerStack;
    public Stack<MealStackInfo.IceCreamFlavours> iceCreamStack;
    
    public PlateStack(string name) {
        this.stackName = name;
        this.hamburguerStack = new Stack<MealStackInfo.HamburguerIngredient>();
        this.iceCreamStack = new Stack<MealStackInfo.IceCreamFlavours>();
    }
}


public static class MealStackInfo{
    public enum HamburguerIngredient{BreadBottom, BreadTop,Patty, Salad, Cheese, Tomato, Pickles};
    public enum IceCreamFlavours{Cone,Chocolate, Vanilla, Strawberry};
    public enum PlateType{Hamburguer, IceCream};
}
