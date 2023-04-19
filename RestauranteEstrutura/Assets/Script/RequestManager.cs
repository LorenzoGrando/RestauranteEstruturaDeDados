using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RequestManager : MonoBehaviour
{
    public Queue<PlateStack> requestsQueue;
    PlateStack newestRequest;
    public int requestSize;

    void Start()
    {
        requestsQueue = new Queue<PlateStack>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)) {
            newestRequest = CreateRequest("Request", MealStackInfo.PlateType.IceCream, requestSize);
            requestsQueue.Enqueue(newestRequest);
        }

        if(Input.GetKeyDown(KeyCode.H)) {
            newestRequest = CreateRequest("Request", MealStackInfo.PlateType.Hamburguer, requestSize);
            requestsQueue.Enqueue(newestRequest);
        }


        if(Input.GetKeyDown(KeyCode.Q) && requestsQueue.Count != 0) {
            PlateStack oldestRequest = requestsQueue.Dequeue();
            Debug.Log(oldestRequest.stackName);
            
            Debug.Log("Oldest Request's List of Ingredients from Top to Bottom: ");

            if(oldestRequest.pilha.pilhaSorvete != null) {
                for(int i = oldestRequest.pilha.pilhaSorvete.Length; i > 0; i-- ) {
                    if(oldestRequest.pilha.ChecarTopoSorvete() == MealStackInfo.IceCreamFlavours.Null) {
                        break;
                    }
                    Debug.Log(oldestRequest.pilha.ChecarTopoSorvete().ToString());
                    oldestRequest.pilha.DesempilharSorvete();
                }
            }

            if(oldestRequest.pilha.pilhaHamburguer != null) {
                for(int i = oldestRequest.pilha.pilhaHamburguer.Length; i > 0; i-- ) {
                    if(oldestRequest.pilha.ChecarTopoHamburguer() == MealStackInfo.HamburguerIngredient.Null) {
                        break;
                    }
                    Debug.Log(oldestRequest.pilha.ChecarTopoHamburguer().ToString());
                    oldestRequest.pilha.DesempilharHamburguer();
                }
            }
            
        }
    }

    public MealStackInfo.IceCreamFlavours[] GenerateIceCreamIngredients(int requestSize) {
        MealStackInfo.IceCreamFlavours[] requestFlavours = new MealStackInfo.IceCreamFlavours[requestSize];
        
        for(int i = 0; i < requestSize; i++) {
            if(i != 0) {
                var enumSize = Enum.GetNames(typeof(MealStackInfo.IceCreamFlavours));
                int ingredientID = UnityEngine.Random.Range(2, enumSize.Length);
                requestFlavours[i] = (MealStackInfo.IceCreamFlavours)ingredientID;
            }
            else {
                requestFlavours[i] = MealStackInfo.IceCreamFlavours.Cone;
            }
        }

        return requestFlavours;
    }

        public MealStackInfo.HamburguerIngredient[] GenerateHamburguerIngredients(int requestSize) {
        MealStackInfo.HamburguerIngredient[] requestIngredients = new MealStackInfo.HamburguerIngredient[requestSize];
        
        for(int i = 0; i < requestSize; i++) {
            if(i == 0) {
                requestIngredients[i] = MealStackInfo.HamburguerIngredient.BreadBottom;
            }
            else if (i == requestSize - 1) {
                requestIngredients[i] = MealStackInfo.HamburguerIngredient.BreadTop;
            }
            else{
                var enumSize = Enum.GetNames(typeof(MealStackInfo.HamburguerIngredient));
                int ingredientID = UnityEngine.Random.Range(3, enumSize.Length);
                requestIngredients[i] = (MealStackInfo.HamburguerIngredient)ingredientID;
            }
        }

        return requestIngredients;
    }

    public PlateStack CreateRequest(string requestName, MealStackInfo.PlateType requestType, int requestSize) {
        PlateStack thisRequest = new PlateStack(requestName, requestType);
        MealStackInfo.IceCreamFlavours[] iceFlavours = null;
        MealStackInfo.HamburguerIngredient[] hambIngredients = null;

        if(requestType == MealStackInfo.PlateType.IceCream) {
            iceFlavours = GenerateIceCreamIngredients(requestSize);
        }

        if(requestType == MealStackInfo.PlateType.Hamburguer) {
            hambIngredients = GenerateHamburguerIngredients(requestSize);
        }

        if(iceFlavours != null){
            foreach(MealStackInfo.IceCreamFlavours ingredient in iceFlavours) {
                thisRequest.pilha.Empilhar(MealStackInfo.HamburguerIngredient.Null, ingredient);
            }
        }
        
        if(hambIngredients != null){
            foreach(MealStackInfo.HamburguerIngredient ingredient in hambIngredients) {
                thisRequest.pilha.Empilhar(ingredient, MealStackInfo.IceCreamFlavours.Null);
            }
        }

        Debug.Log("Created request of type " + requestType.ToString() + " and size " + requestSize);
        return thisRequest;
    }
}
