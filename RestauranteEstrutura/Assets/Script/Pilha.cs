using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilha
{
    public MealStackInfo.HamburguerIngredient[] pilhaHamburguer;
    public MealStackInfo.IceCreamFlavours[] pilhaSorvete;
    public MealStackInfo.PlateType tipoPilha;
    int indexTopo;

    public Pilha(MealStackInfo.PlateType type, int size) {
        if(type == MealStackInfo.PlateType.Hamburguer) {
            pilhaHamburguer = new MealStackInfo.HamburguerIngredient[size];
        }
        if(type == MealStackInfo.PlateType.IceCream) {
            pilhaSorvete = new MealStackInfo.IceCreamFlavours[size];
        }

        tipoPilha = type;
        indexTopo = -1;
    }

    public void Empilhar(MealStackInfo.HamburguerIngredient ingredienteHamburguer, MealStackInfo.IceCreamFlavours saborSorvete) {
        if(pilhaHamburguer != null && !ChecarPreenchimento(false, indexTopo, pilhaHamburguer.Length)) {
            indexTopo ++;
            pilhaHamburguer[indexTopo] = ingredienteHamburguer;
        }
        else if(pilhaSorvete != null && !ChecarPreenchimento(false, indexTopo, pilhaSorvete.Length)) {
            indexTopo ++;
            pilhaSorvete[indexTopo] = saborSorvete;
        }
        else {
            Debug.Log("Pilha Cheia");
        }
    }

    public MealStackInfo.HamburguerIngredient DesempilharHamburguer() {
        if(pilhaHamburguer != null && ChecarPreenchimento(true, indexTopo, pilhaHamburguer.Length)) {
            Debug.Log("Pilha Vazia");
            return MealStackInfo.HamburguerIngredient.Null;
        }
        else if(pilhaHamburguer == null) {
            Debug.Log("Pilha Inexistente");
            return MealStackInfo.HamburguerIngredient.Null;
        }
        else {
            MealStackInfo.HamburguerIngredient ingredienteTopo = pilhaHamburguer[indexTopo];
            pilhaHamburguer[indexTopo] = MealStackInfo.HamburguerIngredient.Null;
            indexTopo--;
            return ingredienteTopo;
        }
    }

    public MealStackInfo.HamburguerIngredient ChecarTopoHamburguer() {
        if(pilhaHamburguer != null && ChecarPreenchimento(true, indexTopo, pilhaHamburguer.Length)) {
            Debug.Log("Pilha Vazia");
            return MealStackInfo.HamburguerIngredient.Null;
        }
        else if(pilhaHamburguer == null) {
            Debug.Log("Pilha Inexistente");
            return MealStackInfo.HamburguerIngredient.Null;
        }
        else {
            MealStackInfo.HamburguerIngredient ingredienteTopo = pilhaHamburguer[indexTopo];
            return ingredienteTopo;
        }
    }

    public MealStackInfo.IceCreamFlavours DesempilharSorvete() {
        if(pilhaSorvete != null && ChecarPreenchimento(true, indexTopo, pilhaSorvete.Length)) {
            Debug.Log("Pilha Vazia");
            return MealStackInfo.IceCreamFlavours.Null;
        }
        else if(pilhaSorvete == null) {
            Debug.Log("Pilha Inexistente");
            return MealStackInfo.IceCreamFlavours.Null;
        }
        else {
            MealStackInfo.IceCreamFlavours ingredienteTopo = pilhaSorvete[indexTopo];
            pilhaSorvete[indexTopo] = MealStackInfo.IceCreamFlavours.Null;
            indexTopo--;
            return ingredienteTopo;
        }
    }

    public MealStackInfo.IceCreamFlavours ChecarTopoSorvete() {
        if(pilhaSorvete != null && ChecarPreenchimento(true, indexTopo, pilhaSorvete.Length)) {
            Debug.Log("Pilha Vazia");
            return MealStackInfo.IceCreamFlavours.Null;
        }
        else if(pilhaSorvete == null) {
            Debug.Log("Pilha Inexistente");
            return MealStackInfo.IceCreamFlavours.Null;
        }
        else {
            MealStackInfo.IceCreamFlavours ingredienteTopo = pilhaSorvete[indexTopo];
            return ingredienteTopo;
        }
    }

    public bool ChecarPreenchimento(bool checarSeVazio, int topo, int tamanho) {
        if(checarSeVazio) {
            return true ? topo == -1 : false;
        }
        else {
            return true ? topo + 1 == tamanho : false;
        }
    }
}
