using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateStack
{
    public string stackName;
    public Pilha pilha;
    //public Stack<MealStackInfo.HamburguerIngredient> hamburguerStack;
    //public Stack<MealStackInfo.IceCreamFlavours> iceCreamStack;
    
    public PlateStack(string name, MealStackInfo.PlateType type) {
        this.stackName = name;
        pilha = new Pilha(type, 10);
        //this.hamburguerStack = new Stack<MealStackInfo.HamburguerIngredient>();
        //this.iceCreamStack = new Stack<MealStackInfo.IceCreamFlavours>();
    }

    void AddIngredientToPlate(MealStackInfo.HamburguerIngredient hamIngredient, MealStackInfo.IceCreamFlavours iceFlavour){
        if(pilha.tipoPilha == MealStackInfo.PlateType.Null) {
            if(hamIngredient != MealStackInfo.HamburguerIngredient.Null) {
                UpdateStackType(MealStackInfo.PlateType.Hamburguer);
            }
            else {
                UpdateStackType(MealStackInfo.PlateType.IceCream);
            }
        }
        pilha.Empilhar(hamIngredient, iceFlavour);
    }

    void RemoveIngredientFromPlate() {
        if(pilha.tipoPilha == MealStackInfo.PlateType.Hamburguer) {
            pilha.DesempilharHamburguer();
        }
        else {
            pilha.DesempilharSorvete();
        }
    }

    void UpdateStackType(MealStackInfo.PlateType type) {
        pilha = new Pilha(type, 10);
    }
}


public static class MealStackInfo{
    public enum HamburguerIngredient{Null, BreadBottom, BreadTop, Patty, Salad, Cheese, Tomato, Pickles};
    public enum IceCreamFlavours{Null, Cone, Chocolate, Vanilla, Strawberry};
    public enum PlateType{Null, Hamburguer, IceCream};
}
