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

    public bool CanAfford (string turretName)
    {
        //bool canAfford;

        switch (turretName)
        {
            default:
                Debug.LogWarning("Shop panel: turretName was no defined");
                return false;
            //break;

            case "standard":
                if (PlayerStats.money >= standardTurretBlueprint.buyPrice)
                    return true;

                else 
                    return false;
            //break;

            case "white":
                if (PlayerStats.money >= whiteTurretBlueprint.buyPrice)
                    return true;

                else
                    return false;
             //break;
        }
    }
}
