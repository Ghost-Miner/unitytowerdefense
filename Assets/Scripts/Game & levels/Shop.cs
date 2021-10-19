using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    private BuildManager buildManager;

    public TurretBlueprint BasicTurretBP;
    public TurretBlueprint LaserTurretBP;
    public TurretBlueprint MachineTurret;
    public TurretBlueprint FlameTurretBP;

    [SerializeField] private TMP_Text basicPrice;
    [SerializeField] private TMP_Text laserPrice;
    [SerializeField] private TMP_Text machinePrice;
    [SerializeField] private TMP_Text flaePrice;

    private void Start()
    {
        buildManager = BuildManager.instance;

        basicPrice.text     = "Cost: " + BasicTurretBP.buyPrice;
        laserPrice.text     = "Cost: " + LaserTurretBP.buyPrice;
        machinePrice.text   = "Cost: " + MachineTurret.buyPrice;
        flaePrice.text      = "Cost: " + FlameTurretBP.buyPrice;
    }

    public void SelectStandardTurret ()
    {
        //Debug.Log("st. turr selected");
        buildManager.SelectTurretToBuild(BasicTurretBP);
    }

    public void SelectWhiteTurret()
    {
        //Debug.Log("wite turr elected");
        buildManager.SelectTurretToBuild(LaserTurretBP);
    }

    public void SelectTurret (string name)
    {
        switch (name)
        {
            case "basic":
                buildManager.SelectTurretToBuild(BasicTurretBP);
                break;

            case "laser":
                buildManager.SelectTurretToBuild(LaserTurretBP);
                break;

            case "machine":
                buildManager.SelectTurretToBuild(MachineTurret);
                break;

            case "flame":
                buildManager.SelectTurretToBuild(FlameTurretBP);
                break;
        }
    }

    public bool CanAfford (string turretName)
    {
        switch (turretName)
        {
            default:
                Debug.LogError("Shop panel: turretName was no defined");
                return false;
            //break;

            case "basic":
                if (PlayerStats.money >= BasicTurretBP.buyPrice)
                    return true;

                else 
                    return false;
            //break;

            case "laser":
                if (PlayerStats.money >= LaserTurretBP.buyPrice)
                    return true;

                else
                    return false;
            //break;

            case "flame":
                if (PlayerStats.money >= FlameTurretBP.buyPrice)
                    return true;

                else
                    return false;
            //break;

            case "machine":
                if (PlayerStats.money >= MachineTurret.buyPrice)
                    return true;

                else
                    return false;
                //break;
        }
    }
}
