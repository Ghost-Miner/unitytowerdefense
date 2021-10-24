using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;       // Canvas object
    public GameObject camera;   // Main camera

    public TMP_Text upgradeCostText;
    public TMP_Text sellCost;
    public Button   upgradeButton;

    private Node target;        // Select / build turret

    private float cameraZUp      = -12f;        // Position when turret menu goes outside the vidible area
    private float cameraZDefault = -19f;   // Default Z position
    private float camMoveSpeed   = 5f;

    [SerializeField] private CameraController cameraController;


    public void SetTarget (Node _target)
    {
        this.target = _target;

        transform.position = target.GetBuildPosition();


        // Is upgraded? Display "Max level".
        if (_target.isUpgraded)
        {
            upgradeCostText.text = "Max level";
            upgradeButton.interactable = false;
        }
        // not enough money?
        else if (PlayerStats.money < _target.turretBlueprint.upgradePrice)
        {
            upgradeButton.interactable = false;
            upgradeCostText.text = "Not enough money";
        }
        // Everything ok? display upgrade cost
        else
        {
            upgradeCostText.text = _target.turretBlueprint.upgradePrice.ToString();
            upgradeButton.interactable = true;
        }

        ui.SetActive(true);

        sellCost.text = _target.turretBlueprint.sellPrice.ToString(); 
    }

    // Hide turret options (world canvas)
    public void DisableUI ()
    {
        MoveCameraDown();
        ui.SetActive(false);
    }

    public void EnableUi()
    {
        ui.SetActive(true);
    }

    private void Update()
    {
        // RMB to disable turret menu
        if (Input.GetButtonDown("MouseRight"))
        {
            DisableUI();
        }

        // Move camera up if the turret menu goes outside the screen
        if (ui.transform.position.z >= -6 && ui.activeInHierarchy)
        {
            MoveCameraUp();
        }
    }

    public void Upgrade ()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell ()
    {
        DisableUI();

        target.isUpgraded = false;
        target.SellTurret();
    }

    // Move camera up
    void MoveCameraUp ()
    {
        Vector3 oldPos = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z);
        Vector3 newPos = new Vector3(camera.transform.position.x, camera.transform.position.y, cameraZUp);

        Vector3 smoothMove = Vector3.Lerp(oldPos, newPos, camMoveSpeed * Time.deltaTime);

        camera.transform.position = new Vector3(smoothMove.x, smoothMove.y, smoothMove.z);
    }

    // Move camera back to its default position
    void MoveCameraDown()
    {
        camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, cameraZDefault);
        /*
        Vector3 oldPos = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z);
        Vector3 newPos = new Vector3(camera.transform.position.x, camera.transform.position.y, cameraZDefault);

        Vector3 smoothMove = Vector3.Lerp(oldPos, newPos, camMoveSpeed * Time.deltaTime);

        camera.transform.position = new Vector3(smoothMove.x, smoothMove.y, smoothMove.z);*/
    }
}


//camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, -26);