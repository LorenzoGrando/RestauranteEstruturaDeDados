using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    Vector2 velocity;
    float cameraHalfWidth;
    public PlateStack heldPlate;

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

        if(transform.position.y > Camera.main.orthographicSize) {
            newPos.y = Camera.main.orthographicSize;
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
}
