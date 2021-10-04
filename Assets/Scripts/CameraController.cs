using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool doMovement = false;

    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    public float panMinX = 0f;
    public float panMaxX = 40f;

    public float scrollSpeed = 5f;
    public float zoomMinY = 10f;
    public float zoomMaxY = 42;

    [HideInInspector]
    public Vector3 defaultCamPosition;

    private void Start()
    {
        defaultCamPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        //Debug.Log("CAMERA POS.: " + defaultCamPosition);
    }

    public void ResetCameraPosition ()
    {
        transform.position = defaultCamPosition;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            doMovement = true;
        }
        else
        {
            doMovement = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCameraPosition();   
        }

        if (!doMovement)
        {
            return;
        }

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * 100 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, zoomMinY, zoomMaxY);

        transform.position = pos;

    }
}
