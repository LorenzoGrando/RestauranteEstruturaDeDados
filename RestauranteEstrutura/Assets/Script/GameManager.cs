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
    float lastSpawnTime = 0;
    float minSpawnTimeDelay = 1;
    void Start()
    {
        allCustomers = new List<GameObject>();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Tab)) {
            camManager.SwitchActiveCamera();
        }
        if(lastSpawnTime + minSpawnTimeDelay < Time.time) {
            GenerateNewCustomer();
        }
    }

    void GenerateNewCustomer() {
        if(seatManager.HasAvailableChairs() && allCustomers.Count < seatManager.sceneChairs.Length) {
            int randomSpawnChance = UnityEngine.Random.Range(1, 11);
            if(randomSpawnChance > 6) {
                GameObject customer = Instantiate(customerRef, customerSpawnPoint.transform.position, Quaternion.Euler(0,0,0));
                customer.GetComponent<Customer>().targetDecisionPosition = customerDecisionPoint.transform;
                allCustomers.Add(customer);  
            }
            lastSpawnTime = Time.time;
        }
    }

}
