using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool moveMouse = true;
    public float panSpeed = 30f; //pan is when you move around on the flat plane (X, Z axys)
    public float panBorderThickness = 10f; //this gives a bit of a border to where the mouse will move our screen when moved to the edge / used to check if we are  within 10 px of the border of the screen
    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;

    public float minX = -50f;
    public float maxX = 50f;

    public float minZ = -50f;
    public float maxZ = 50f;
    void Update()
    {
        if (GameManager.gameOver)
        {
            this.enabled = false;
            return;
        }
        if(Input.GetKeyDown(KeyCode.Escape)) moveMouse = !moveMouse; //disable and enable camera movement

        if (!moveMouse)
        {
            return;
        }
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness) //GetKeyDown will work only once until the key is released. GetKey will work while the key is pressed.
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World); //transform.Translate is a good choice to move when we don't have physycs applied
            //Vector3.forward is the same as new Vector 3(0f,0f,1f)
            //we multiply with Time.deltaTime because we want the speed to be independant from the frame rates
            //Space.World ensures that you are moving the camera based on the coordinates relative to the world, not the object itself.
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("q"))
        {
            transform.Rotate(Vector3.down * panSpeed * Time.deltaTime, Space.World);
        }        
        if (Input.GetKey("e"))
        {
            transform.Rotate(Vector3.up * panSpeed * Time.deltaTime, Space.World);
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 position = transform.position;

        position.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;

        // Clamp height
        position.y = Mathf.Clamp(position.y, minY, maxY);

        // Clamp camera area
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.z = Mathf.Clamp(position.z, minZ, maxZ);

        transform.position = position;
    }
}
