using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera kitchenCamera;
    public Camera customerCamera;


    public void SwitchActiveCamera() {
        if(Camera.main == kitchenCamera) {
            kitchenCamera.gameObject.SetActive(false);
            customerCamera.gameObject.SetActive(true);
            return;
        }
        else{
            customerCamera.gameObject.SetActive(false);
            kitchenCamera.gameObject.SetActive(true);
        }
    }
}
