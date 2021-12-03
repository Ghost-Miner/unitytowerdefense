using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool doMovement = false;

    [SerializeField] private float panSpeed = 30f;
    [SerializeField] private float panBorderThickness = 10f;

    [SerializeField] private float panMinX = 0f;
    [SerializeField] private float panMaxX = 40f;

    [SerializeField] private float scrollSpeed = 5f;
    [SerializeField] private float zoomMinY = 10f;
    [SerializeField] private float zoomMaxY = 42;

    [Header("Cam Y pos for aspect ratios")]
    [SerializeField] private float yPos169  = 44f;
    [SerializeField] private float yPos1610 = 45f;
    [SerializeField] private float yPos43   = 57f;
    [SerializeField] private float yPos54   = 55f;
    [SerializeField] private float yPos32   = 50f;

    private Vector3 defaultCamPosition;

    private void Start()
    {
        defaultCamPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        GetAspectRatio();

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

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            GetAspectRatio();
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

    private void ChangeCameraHeight (float yPos)
    {
        Vector3 camPos = new Vector3 (transform.position.x, yPos, transform.position.z);
        transform.position = camPos;
    }

    private void GetAspectRatio()
    {
        //16:9  - 1134 x 638 = 1,777
        //16:10 - 1021 x 638 = 1,600
        //3:2   - 957 x  638 = 1,500
        //4:3   - 851 x  638 = 1,333
        //5:4   - 798 x  638 = 1,250

        float sWidth = Screen.width;
        float sHeight = Screen.height;

        float ratio = sWidth / sHeight;
        string curRes = Screen.width + " x " + Screen.height;

        if (ratio < 1.25f)
        {
            ChangeCameraHeight(yPos43);

            Debug.Log("ASPECT RATIO:  OTHER, Narrow, || Resolution: " + curRes);
        }
        else if (ratio >= 1.25f && ratio < 1.333f)
        {
            ChangeCameraHeight(yPos54);

            Debug.Log("ASPECT RATIO:  5 : 4, || Resolution: " + curRes);
        }
        else if (ratio >= 1.333 && ratio < 1.5f)
        {
            ChangeCameraHeight(yPos43);

            Debug.Log("ASPECT RATIO:  4 : 3, || Resolution: " + curRes);
        }
        else if (ratio >= 1.5f && ratio < 1.6)
        {
            ChangeCameraHeight(yPos32);

            Debug.Log("ASPECT RATIO:  3 : 2, || Resolution: " + curRes);
        }
        else if (ratio >= 1.6 && ratio < 1.77)
        {
            ChangeCameraHeight(yPos1610);

            Debug.Log("ASPECT RATIO:  16 : 10, || Resolution: " + curRes);
        }
        else if (ratio >= 1.77 && ratio < 1.8)
        {
            ChangeCameraHeight(yPos169);

            Debug.Log("ASPECT RATIO:  16 : 9, || Resolution: " + curRes);
        }
        else if (ratio >= 1.8)
        {
            ChangeCameraHeight(yPos169);

            Debug.Log("ASPECT RATIO:  OTHER, Wide, || Resolution: " + curRes);
        }
    }
}
