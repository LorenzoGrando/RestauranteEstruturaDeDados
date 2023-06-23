using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Customer : MonoBehaviour
{
    SeatManager seats;
    public float speed;
    Vector2 targetSeatPosition;
    public Transform targetDecisionPosition;
    int seatID;
    bool isSeated;
    bool foundSeat;
    bool madeDecision;
    public enum CustomerType {Null, Child, Adult, Elder};
    public enum CustomerShape {Null, Square, Circle};
    public enum CustomerColor {Null, Red, Blue};
    public CustomerType myType;
    public CustomerShape myShape;
    public CustomerColor myColor;
    public int customerHunger;
    public Tree myDecisionTree;
    public TextMeshProUGUI myTypeText;
    public TextMeshProUGUI myHunger;

    void Start()
    {
        seats = FindObjectOfType<SeatManager>();
    }
    void Update()
    {
        if(!madeDecision && targetDecisionPosition != null) {
            MoveToDecision();
        }

        if(!foundSeat && madeDecision) {
            ChooseSeat();
        }
        if(!isSeated && foundSeat) {
            MoveToSeat();
        }
    }

    void ChooseSeat() {
        foreach(Chair chair in seats.sceneChairs) {
            if(chair.isOccupied == false) {
                GameObject chairTransform = chair.gameObject;
                targetSeatPosition = chairTransform.gameObject.transform.position;
                chair.isOccupied = true;
                chair.seatedCustomer = this.gameObject;
                foundSeat = true;
                break;
            }
        }
    }

    void MoveToDecision() {
        transform.position = Vector2.MoveTowards(transform.position, targetDecisionPosition.position, speed * Time.deltaTime);
        if(transform.position == targetDecisionPosition.position) {
            MakeDecision();
        }
    }

    void MoveToSeat() {
       transform.position = Vector2.MoveTowards(transform.position, targetSeatPosition, speed * Time.deltaTime);
        if(transform.position == (Vector3)targetSeatPosition){
            isSeated = true;
        }
    }

    void MakeDecision() {
        Tree currentNode = myDecisionTree;
        while(currentNode.leaf == false) {
            if(currentNode.condition(myColor, myShape, myType, customerHunger) == false) {
                currentNode = currentNode.optionFalse;
            }
            else {
                currentNode = currentNode.optionTrue;
            }
        }
        if(currentNode != null) {
            MakeRequest(currentNode.returnPlateType, currentNode.returnRequestSize);
        }
    }

    void MakeRequest(MealStackInfo.PlateType requestType, int requestSize) {
        RequestManager requestRef = FindObjectOfType<RequestManager>();     
        requestRef.requestsQueue.Inserir(requestRef.CreateRequest("Request " + requestRef.requestsQueue.Tamanho(), 
                                                                                                requestType, requestSize));
        madeDecision = true;
    }
}
