using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooting : MonoBehaviour
{
    private Rigidbody _rigidbody;

    //When shooting, the projectile will be set to the centre of the actor that's shooting it, rotate to face the target vector,
    //moves outward by the radius, then is spawned in and will move forward until it reaches the end of its timer, or it has
    //collided with something.
    public Transform projectileSpawnpoint; //This will assume the transform returns the centre of the actor shooting.
    public float radiusFromActor; //Moves this far away from the actor before spawning in.
    public float projectileSpeed = 3.0f;
    public GameObject projectilePrefab;
    public Transform playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        //var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        if (Input.GetMouseButtonDown(0)) //Semi auto with no cooldown as of the moment.
        {
            projectileSpawnpoint.position = playerPosition.position; //Resets the shooting point to the player.



            float screenWidthHalfway = -(Screen.width / 2);
            float screenHeightHalfway = -(Screen.height / 2);

            Debug.Log("Half width = " + screenWidthHalfway);
            Debug.Log("Half height = " + screenHeightHalfway);



            float mouseX = -Input.mousePosition.x;
            float mouseY = -Input.mousePosition.y;

            

            //Debug.Log("Mouse position = " + mousePositionVector.ToString());




            if (mouseX >= screenWidthHalfway)
            {
                mouseX = -mouseX;
                Debug.Log("mouse X less than halfway. Switched to negative");
            }
            if (mouseY >= screenHeightHalfway)
            {
                mouseY = -mouseY;
                Debug.Log("mouse Y less than halfway. Switched to negative");
            }

            Vector3 mousePositionVector = new Vector3(mouseX, 0.0f, mouseY); //The position of the mouse when shooting.
            Vector3 test = Camera.main.ScreenToWorldPoint(mousePositionVector);
            RaycastHit hit;
            Vector3 anything = Vector3.zero;
            if (Physics.Raycast(test, Camera.main.transform.forward, out hit))
            {
                anything = hit.transform.position;
            }

            //mousePositionVector.Normalize();

            projectileSpawnpoint.LookAt(anything);
            projectileSpawnpoint.transform.Translate(0.0f, 0.0f, radiusFromActor); //moves to the point the bullet will spawn at.
            projectileSpawnpoint.eulerAngles = new Vector3(90.0f, 0.0f, 0.0f);



















            //float mouseX = -Input.mousePosition.x;
            //float mouseY = -Input.mousePosition.z;


            ////The width and height of the screen in pixels.
            //float screenWidth = Screen.width;
            //float screenHeight = Screen.height;

            ////Translates mouse X and Y to values between -1 and 1 where (0,0) is the center of the screen.
            //float offsetX = ((mouseX / screenWidth) * 2) - 1.0f;
            //float offsetY = ((mouseY / screenHeight) * 2) - 1.0f;


            //mouseX = Mathf.Acos(offsetX);
            //mouseY = Mathf.Asin(offsetY);

            //mouseX /= radiusFromActor;
            //mouseY /= radiusFromActor;

            //mouseX /= (180 /Mathf.PI);
            //mouseY /= (180 / Mathf.PI);

            //Debug.Log("MouseX degrees = " + mouseX);
            //Debug.Log("MouseY degrees = " + mouseY);






























            Instantiate(projectilePrefab, projectileSpawnpoint.position, projectileSpawnpoint.rotation);
        }
    }
}
