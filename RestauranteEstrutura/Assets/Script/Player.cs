using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    Vector2 velocity;
    float cameraHalfWidth;
    public GameObject platePrefab;
    public GameObject heldItem;
    float lastInputTime;


    void Start()
    {
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
    }

    void Update()
    {
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 direction = playerInput.normalized;

        velocity = direction * speed;
    }

    void FixedUpdate()
    {
        transform.Translate(Time.fixedDeltaTime * velocity);
        Vector3 newPos = transform.position;

        if(transform.position.y > Camera.main.orthographicSize - transform.localScale.y) {
            newPos.y = Camera.main.orthographicSize - transform.localScale.y;
        }
        if(transform.position.y < -Camera.main.orthographicSize) {
            newPos.y = -Camera.main.orthographicSize;
        }
        if(transform.position.x > cameraHalfWidth) {
            newPos.x = cameraHalfWidth;
        }
        if(transform.position.x < - cameraHalfWidth) {
            newPos.x = -cameraHalfWidth;
        }
        transform.position = newPos;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(Input.GetKey(KeyCode.Space) && heldItem != null && other.tag == "Counter") {
            if(Time.time > lastInputTime + .5f) {
                lastInputTime = Time.time;
                PlacePlate(other.gameObject);
            }

        }
        if(Input.GetKey(KeyCode.Space) && heldItem == null && other.tag == "Counter") {
            if(Time.time > lastInputTime + .5f) {
                lastInputTime = Time.time;
                CollectPlate(other.gameObject);
            }
        }

        if(Input.GetKey(KeyCode.Space) && heldItem == null && other.tag == "PlateShelf") {
            if(Time.time > lastInputTime + .5f) {
                lastInputTime = Time.time;
                GeneratePlate();
            }
        }

        if(Input.GetKey(KeyCode.Space) && heldItem != null && other.tag == "Bin") {
            if(Time.time > lastInputTime + .5f) {
                lastInputTime = Time.time;
                heldItem = null;
                Destroy(gameObject.transform.GetChild(0).gameObject);
            }
        }
    }

    void GeneratePlate() {
        heldItem = Instantiate(platePrefab, transform.position, Quaternion.identity);
        heldItem.transform.position = new Vector2(transform.position.x, transform.position.y - 0.75f);
        heldItem.transform.parent = transform;
    }

    void PlacePlate(GameObject target) {
        Plate thisPlate = null;
        if(heldItem.TryGetComponent<Plate>(out thisPlate)) {
            Counter counterRef = target.GetComponent<Counter>();
            if(counterRef.isEmpty) {
                Debug.Log("Placed Plate");
                counterRef.PlacePlate(heldItem, gameObject.GetComponent<Player>());
            }
        }
    }

    void CollectPlate(GameObject target) {
        Counter counterRef = target.GetComponent<Counter>();
        if(counterRef.plate != null) {
            Debug.Log("Collected Plate");
            counterRef.RemovePlate(gameObject.GetComponent<Player>());
            heldItem.transform.parent = null;
            heldItem.transform.position = new Vector2(transform.position.x, transform.position.y - 0.75f);
            heldItem.transform.parent = transform;
        }
    }
}
