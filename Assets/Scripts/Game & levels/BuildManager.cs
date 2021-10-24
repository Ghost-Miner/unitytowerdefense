using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
        }

        instance = this;
    }

    //public GameObject buildEffect;

    public GameObject standardTurretPrefab;
    public GameObject whiteTurretPrefab;

    private TurretBlueprint turretToBuild;
    private Node selectedNode;
    public NodeUI nodeUI;


    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.money >= turretToBuild.buyPrice; } }


    public void SelectTurretToBuild (TurretBlueprint turret)
    {
        turretToBuild = turret;
        selectedNode = null;

        DeselectNode();
    }

    public void SelectNode (Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
        }

        selectedNode = node;
        turretToBuild = null;

        //nodeUI.SetTarget(node);
        nodeUI.EnableUi();
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.DisableUI();
    }

    public void BuildTurretOn (Node node)
    { }

    public TurretBlueprint GetTurretToBuild ()
    {
        return turretToBuild;
    }
}
