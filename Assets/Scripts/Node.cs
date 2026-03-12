using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [SerializeField] Color hoverColor;
    [SerializeField] Color notEnoughMoneyHoverColor;

    private Renderer rend;
    private Color startColor;
    private BuildManager buildManager;

    public Vector3 positionOffset;

    [HideInInspector] public GameObject turret;
    [HideInInspector] public TurretBlueprint turretBlueprint;
    [HideInInspector] public bool isUpgraded = false;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    private void OnMouseEnter()
    {
        if (!enabled)
            return;

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        rend.material.color = buildManager.HasEnoughMoney ? hoverColor : notEnoughMoneyHoverColor;
    }

    private void OnMouseExit()
    {
        if (!enabled)
            return;

        rend.material.color = startColor;
    }

    private void OnMouseDown()
    {
        if (!enabled)
            return;

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
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
        Destroy(turret);

        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject tempBuildEffect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(tempBuildEffect, 5f);

        isUpgraded = true;

        Debug.Log("Turret upgraded.");
    }

    public void SellTurret()
    {
        if (turret == null)
            return;

        PlayerStats.Money += turretBlueprint.GetSellValue();
        Destroy(turret);
        turret = null;
        turretBlueprint = null;
        isUpgraded = false;

        GameObject tempBuildEffect = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(tempBuildEffect, 5f);
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
}