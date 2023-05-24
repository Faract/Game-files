using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public DefenderClass standardDefender;
    public DefenderClass towerDefender;
    public DefenderClass tankDefender;
    
    public DefenderClass UpdatedStandardDefender;
    public DefenderClass UpdatedTowerDefender;
    public DefenderClass UpdatedTankDefender;
    
    private BuildManager buildManager;
    public Base baseToBuildOn;
    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void PurchaseStandardDefender()
    {
        buildManager.SetDefenderToBuild(standardDefender);
        
        if (!buildManager.CanBuild)
            return;
        
        if (baseToBuildOn.defender != null)
        {
            Debug.Log("Уже построен");
            return;
        }

        buildManager.BuildDefenderOn(baseToBuildOn);
        Destroy(transform.parent.gameObject);
    }
    
    public void PurchaseTowerDefender()
    {
        buildManager.SetDefenderToBuild(towerDefender);
        
        if (!buildManager.CanBuild)
            return;
        
        if (baseToBuildOn.defender != null)
        {
            Debug.Log("Уже построен");
            return;
        }

        buildManager.BuildDefenderOn(baseToBuildOn);
        Destroy(transform.parent.gameObject);
 
    }
    
    public void PurchaseTankDefender()
    {
        buildManager.SetDefenderToBuild(tankDefender);
        
        if (!buildManager.CanBuild)
            return;
        
        if (baseToBuildOn.defender != null)
        {
            Debug.Log("Уже построен");
            return;
        }

        buildManager.BuildDefenderOn(baseToBuildOn);
        Destroy(transform.parent.gameObject);

    }

    public void CloseShop()
    {
        Destroy(transform.parent.gameObject);
    }

    
    /*Меню после постройки защитника*/
    public void UpgradeStandardDefender() 
    {
        if (PlayerStats.Money < 200)
        {
            return;
        }
        Destroy(baseToBuildOn.defender);
        buildManager.SetDefenderToBuild(UpdatedStandardDefender);
        buildManager.UpdateDefenderOn(baseToBuildOn);
        Destroy(transform.parent.gameObject);
        
    }
    public void UpgradeTowerDefender() 
    {
        if (PlayerStats.Money < 400)
        {
            return;
        }
        Destroy(baseToBuildOn.defender);
        buildManager.SetDefenderToBuild(UpdatedTowerDefender);
        buildManager.UpdateDefenderOn(baseToBuildOn);
        Destroy(transform.parent.gameObject);

    }
    public void UpgradeTankDefender() 
    {
        if (PlayerStats.Money < 500)
        {
            return;
        }
        Destroy(baseToBuildOn.defender);
        buildManager.SetDefenderToBuild(UpdatedTankDefender);
        buildManager.UpdateDefenderOn(baseToBuildOn);
        Destroy(transform.parent.gameObject);

    }

    
    public void DestroyDefender()
    {
        if (PlayerStats.Money < 50)
        {
            return;
        }
        Destroy(baseToBuildOn.defender);
        buildManager.ClearBaseMoney();
        Destroy(transform.parent.gameObject);
        baseToBuildOn.defenderIsUpdated = false;
    }
}


