using UnityEngine;

public class Shop : MonoBehaviour
{
    private BuildManager buildManager;

    public TurretBlueprint standardTurretBlueprint;
    public TurretBlueprint whiteTurretBlueprint;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret ()
    {
        //Debug.Log("st. turr selected");
        buildManager.SelectTurretToBuild(standardTurretBlueprint);
    }

    public void SelectWhiteTurret()
    {
        //Debug.Log("wite turr elected");
        buildManager.SelectTurretToBuild(whiteTurretBlueprint);
    }
}
