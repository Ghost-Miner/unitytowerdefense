using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public NavMeshSurface surface;

    //public PlayerNavmesh playerNavmesh;

    public Color   hooverColor;
    public Color   notEnoughMoneyColour;
    public Vector3 positionOffset;

    [HideInInspector] public GameObject      turret;
    [HideInInspector] public TurretBlueprint turretBlueprint;
    [HideInInspector] public bool            isUpgraded;

    private Renderer rend;
    private Color    startColor;

    private BuildManager BuildManager;

    private float meshUpdateDelay = 0.1f;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        BuildManager = BuildManager.instance;
    }    

    public Vector3 GetBuildPosition ()
    {
        return transform.position + positionOffset;
    }

    void BuildTurret (TurretBlueprint blueprint)
    {
        if (PlayerStats.money < blueprint.buyPrice)
        {
            //Debug.Log("not enougn money");
            return;
        }
        PlayerStats.money -= blueprint.buyPrice;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);

        turret = _turret;
        turretBlueprint = blueprint;

        //GameObject effect = (GameObject)Instantiate(BuildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        //Destroy(effect, 5f);
        
        Invoke("UpdateMesh", meshUpdateDelay);

        Debug.Log("### turret built, money: " + PlayerStats.money);
    }

    public void UpgradeTurret ()
    {
        if (PlayerStats.money < turretBlueprint.upgradePrice)
        {
            Debug.Log("not enougn money to upgrade");
            return;
        }
        PlayerStats.money -= turretBlueprint.upgradePrice;

        Destroy(turret);

        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradePrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        //GameObject effect = (GameObject)Instantiate(BuildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        //Destroy(effect, 5f);

        isUpgraded = true;

        Invoke("UpdateMesh", meshUpdateDelay);

        //Debug.Log("turret upgraded, money: " + PlayerStats.money);
    }

    public void SellTurret ()
    {
        PlayerStats.money += turretBlueprint.sellPrice;
        
        Destroy(turret);
        turretBlueprint = null;

        Invoke("UpdateMesh", meshUpdateDelay);
    }

    void UpdateMesh ()
    {
        surface.BuildNavMesh();

        Debug.Log("Mesh updated");
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (turret != null)
        {
            BuildManager.SelectNode(this);
            return;
        }

        if (!BuildManager.CanBuild)
        {
            return;
        }

        BuildTurret(BuildManager.GetTurretToBuild());
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!BuildManager.CanBuild)
        {
            return;
        }

        if (BuildManager.HasMoney)
        {
            rend.material.color = hooverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColour;
        }
        
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
