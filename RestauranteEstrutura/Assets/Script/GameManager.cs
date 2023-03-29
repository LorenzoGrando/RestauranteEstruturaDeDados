using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraManager camManager;
    public SeatManager seatManager;
    public GameObject customerRef;
    public GameObject customerSpawnPoint;
    public GameObject customerDecisionPoint;
    public List<GameObject> allCustomers;
    void Start()
    {
        allCustomers = new List<GameObject>();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Tab)) {
            camManager.SwitchActiveCamera();
        }

        if(Input.GetKeyDown(KeyCode.C)) {
            if(seatManager.HasAvailableChairs() && allCustomers.Count < seatManager.sceneChairs.Length) {
                GameObject customer = Instantiate(customerRef, customerSpawnPoint.transform.position, Quaternion.Euler(0,0,0));
                customer.GetComponent<Customer>().targetDecisionPosition = customerDecisionPoint.transform;
                allCustomers.Add(customer);
            }
            else {
                Debug.Log("No available seats");
            }
        }
    }

}
