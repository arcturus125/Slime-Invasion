using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArm : MonoBehaviour
{

    public float maxCameraOffset = 2.5f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if(Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        //Gets cursor position in pixel coordinates.
        float mouseX = Input.mousePosition.x; 
        float mouseY = Input.mousePosition.y;

        //The width and height of the screen in pixels.
        float screenWidth = Screen.width; 
        float screenHeight = Screen.height;

        //Translates mouse X and Y to values between -1 and 1 where (0,0) is the center of the screen
        float offsetX = ((mouseX / screenWidth) * 2) - 1.0f;
        float offsetY = ((mouseY / screenHeight) * 2) - 1.0f;

        //Dampens player input, making it so that mouse movement near the centre of the screen moves the camera less.
        if (offsetX < 0) offsetX = Mathf.Pow(offsetX, 2);
        else offsetX = -Mathf.Pow(offsetX, 2);
        if (offsetY < 0) offsetY = Mathf.Pow(offsetY, 2);
        else offsetY = -Mathf.Pow(offsetY, 2);

        //Actually moves the camera's offset to player.
        this.transform.localPosition = new Vector3( offsetX * maxCameraOffset, 0.0f, offsetY * maxCameraOffset);
    }
}
