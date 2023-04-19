using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            RequestManager requestRef = FindObjectOfType<RequestManager>();
            MealStackInfo.PlateType requestType;
            int rngDecision = Random.Range(1,3);
            int rngSize = Random.Range(3,7);
            requestType = (MealStackInfo.PlateType)rngDecision;
            requestRef.requestsQueue.Enqueue(requestRef.CreateRequest("Request " + requestRef.requestsQueue.Count, 
                                                                                                requestType, rngSize));
            madeDecision = true;
        }
    }

    void MoveToSeat() {
       transform.position = Vector2.MoveTowards(transform.position, targetSeatPosition, speed * Time.deltaTime);
        if(transform.position == (Vector3)targetSeatPosition){
            isSeated = true;
        }
    }
}
