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
    public Color[] customerColors;
    public Sprite[] customerSprites;

    float lastSpawnTime = 0;
    float minSpawnTimeDelay = 1;
    Vector4 nextCustomerIntParamaters;
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
                GenerateCustomerParameters();
                GameObject customer = Instantiate(customerRef, customerSpawnPoint.transform.position, Quaternion.Euler(0,0,0));
                Customer customerScript = customer.GetComponent<Customer>();
                SpriteRenderer renderer = customer.GetComponent<SpriteRenderer>();

                customerScript.myType = (Customer.CustomerType)nextCustomerIntParamaters.x;

                renderer.sprite = customerSprites[(int)nextCustomerIntParamaters.y];
                customerScript.myShape = (Customer.CustomerShape)nextCustomerIntParamaters.y;

                renderer.color = customerColors[(int)nextCustomerIntParamaters.z];
                customerScript.myColor = (Customer.CustomerColor)nextCustomerIntParamaters.z;

                customerScript.customerHunger = (int)nextCustomerIntParamaters.w;

                customerScript.targetDecisionPosition = customerDecisionPoint.transform;
                GenerateCustomerDecisionTree(customerScript);
                customerScript.myTypeText.text = customerScript.myType.ToString();
                customerScript.myHunger.text = "Hunger: " + customerScript.customerHunger;
                allCustomers.Add(customer); 
            }
            lastSpawnTime = Time.time;
        }
    }

    void GenerateCustomerParameters() {
        int customerType = Random.Range(1, System.Enum.GetNames(typeof(Customer.CustomerType)).Length);
        int customerColor = Random.Range(1, System.Enum.GetNames(typeof(Customer.CustomerColor)).Length);
        int customerShape = Random.Range(1, System.Enum.GetNames(typeof(Customer.CustomerShape)).Length);
        int hunger = Random.Range(1, 101);
        nextCustomerIntParamaters = new Vector4(customerType, customerShape, customerColor, hunger);
    }

    void GenerateCustomerDecisionTree(Customer customer) {
        Tree rootTree = new Tree();

        switch (customer.myType) {
            case Customer.CustomerType.Child:
                //Setup root tree
                rootTree.leaf = false;        
                rootTree.condition = (color, shape, type, hunger) => hunger < 60;

                //Setup True Option
                rootTree.optionTrue = new Tree();
                rootTree.optionTrue.leaf = true;
                rootTree.optionTrue.returnPlateType = MealStackInfo.PlateType.IceCream;
                rootTree.optionTrue.returnRequestSize = 3;
                
                //Setup False Option
                rootTree.optionFalse = new Tree();
                rootTree.optionFalse.leaf = false;        
                rootTree.optionFalse.condition = (color, shape, type, hunger) => color == Customer.CustomerColor.Red;
                
                //Setup False->True Option
                rootTree.optionFalse.optionTrue = new Tree();
                rootTree.optionFalse.optionTrue.leaf = true;
                rootTree.optionFalse.optionTrue.returnPlateType = MealStackInfo.PlateType.Hamburguer;
                rootTree.optionFalse.optionTrue.returnRequestSize = 4;

                //Setup False->False Option
                rootTree.optionFalse.optionFalse = new Tree();
                rootTree.optionFalse.optionFalse.leaf = true;
                rootTree.optionFalse.optionFalse.returnPlateType = MealStackInfo.PlateType.IceCream;
                rootTree.optionFalse.optionFalse.returnRequestSize = 5;
            break;

            case Customer.CustomerType.Adult:
                //Setup root tree
                rootTree.leaf = false;        
                rootTree.condition = (color, shape, type, hunger) => shape == Customer.CustomerShape.Circle;

                //Setup True option
                rootTree.optionTrue = new Tree();
                rootTree.optionTrue.leaf = false;
                rootTree.optionTrue.condition = (color, shape, type, hunger) => hunger > 60;

                //Setup True->True option
                rootTree.optionTrue.optionTrue = new Tree();
                rootTree.optionTrue.optionTrue.leaf = true;
                rootTree.optionTrue.optionTrue.returnPlateType = MealStackInfo.PlateType.Hamburguer;
                rootTree.optionTrue.optionTrue.returnRequestSize = 6;

                //Setup True->False option
                rootTree.optionTrue.optionFalse = new Tree();
                rootTree.optionTrue.optionFalse.leaf = true;
                rootTree.optionTrue.optionFalse.returnPlateType = MealStackInfo.PlateType.Hamburguer;
                rootTree.optionTrue.optionFalse.returnRequestSize = 4;

                //Setup False option
                rootTree.optionFalse = new Tree();
                rootTree.optionFalse.leaf = false;
                rootTree.optionFalse.condition = (color, shape, type, hunger) => color == Customer.CustomerColor.Red;

                //Setup False->True option
                rootTree.optionFalse.optionTrue = new Tree();
                rootTree.optionFalse.optionTrue.leaf = true;
                rootTree.optionFalse.optionTrue.returnPlateType = MealStackInfo.PlateType.Hamburguer;
                rootTree.optionFalse.optionTrue.returnRequestSize = 3;

                //Setup False->False option
                rootTree.optionFalse.optionFalse = new Tree();
                rootTree.optionFalse.optionFalse.leaf = true;
                rootTree.optionFalse.optionFalse.returnPlateType = MealStackInfo.PlateType.IceCream;
                rootTree.optionFalse.optionFalse.returnRequestSize = 5;
            break;

            case Customer.CustomerType.Elder:
                //Setup root tree
                rootTree.leaf = false;
                rootTree.condition = (color, shape, type, hunger) => color == Customer.CustomerColor.Red;

                //Setup True option
                rootTree.optionTrue = new Tree();
                rootTree.optionTrue.leaf = false;
                rootTree.optionTrue.condition = (color, shape, type, hunger) => shape == Customer.CustomerShape.Square;

                //Setup True->True option
                rootTree.optionTrue.optionTrue = new Tree();
                rootTree.optionTrue.optionTrue.leaf = true;
                rootTree.optionTrue.optionTrue.returnPlateType = MealStackInfo.PlateType.Hamburguer;
                rootTree.optionTrue.optionTrue.returnRequestSize = 4;

                //Setup True->False option
                rootTree.optionTrue.optionFalse = new Tree();
                rootTree.optionTrue.optionFalse.leaf = true;
                rootTree.optionTrue.optionFalse.returnPlateType = MealStackInfo.PlateType.IceCream;
                rootTree.optionTrue.optionFalse.returnRequestSize = 6;

                //Setup False option
                rootTree.optionFalse = new Tree();
                rootTree.optionFalse.leaf = false;
                rootTree.optionFalse.condition = (color, shape, type, hunger) => shape == Customer.CustomerShape.Square;

                //Setup False->True option
                rootTree.optionFalse.optionTrue = new Tree();
                rootTree.optionFalse.optionTrue.leaf = true;
                rootTree.optionFalse.optionTrue.returnPlateType = MealStackInfo.PlateType.IceCream;
                rootTree.optionFalse.optionTrue.returnRequestSize = 2;

                //Setup False->False option
                rootTree.optionFalse.optionFalse = new Tree();
                rootTree.optionFalse.optionFalse.leaf = true;
                rootTree.optionFalse.optionFalse.returnPlateType = MealStackInfo.PlateType.IceCream;
                rootTree.optionFalse.optionFalse.returnRequestSize = 3;
            break;
        }

        customer.myDecisionTree = rootTree;
    }
}
