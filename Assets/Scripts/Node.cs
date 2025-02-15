using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    
    [SerializeField] Color hoverColor;
    [SerializeField] Color notEnoughMoneyHoverColor;
    private Renderer rend;
    public Vector3 positionOffset; //to fix the position of the turret that is built

    [HideInInspector]
    public GameObject turret;

    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Color startColor;
    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    void BuildTurret(TurretBlueprint blueprint)
    {

        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("You don't have enough money to build that. :) ");
            return;
        }

        PlayerStats.Money -= blueprint.cost;
        GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject tempBuildEffect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(tempBuildEffect, 5f);

        Debug.Log("Turret built. Money left: " + PlayerStats.Money);
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("You don't have enough money to upgrade that. :) ");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;
        Destroy(turret);//get rid of the old turret

        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity); //build new turret
        turret = _turret;

        GameObject tempBuildEffect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(tempBuildEffect, 5f);
        isUpgraded = true;

        Debug.Log("Turret upgraded.");
    }

    private void OnMouseEnter()
    {
        if (this.enabled == true)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            if (!buildManager.CanBuild)
            {
                return;
            }
            if (buildManager.HasEnoughMoney)
            {
                rend.material.color = hoverColor;
            }
            else
            {
                rend.material.color = notEnoughMoneyHoverColor;
            }
        }
    }

    private void OnMouseExit()
    {
        if (this.enabled == true) 
        { 
            rend.material.color = startColor;
        }
    }

    private void OnMouseDown()
    {
        if (this.enabled == true) 
        { 

            if (turret != null)
            {
                buildManager.SelectNode(this);
                return;
            }

            if (!buildManager.CanBuild)
            {
                return;
            }

            BuildTurret(buildManager.GetTurretToBuild());
        }
    }

    public void SellTurret()
    {
        if (turret != null)
        {
            PlayerStats.Money += turretBlueprint.GetSellValue();
            Destroy(turret);
            turretBlueprint = null;
            isUpgraded = false;

            GameObject tempBuildEffect = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
            Destroy(tempBuildEffect, 5f);
        }
    }

    public Vector3 GetBuildPosition() { return transform.position + positionOffset; }

}
