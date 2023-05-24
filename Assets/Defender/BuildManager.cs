using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake() {
        if (instance != null) {
            Debug.LogError("More than one BuildManager in Scene!");
            return;
        }
        instance = this; 
    }

    public GameObject standardDefenderPrefab;
    public GameObject towerDefenderPrefab;
    public GameObject tankDefenderPrefab;
    

    private DefenderClass defenderToBuild;

    public bool CanBuild
    {
        get { return defenderToBuild != null; }
    }

    public void ClearBaseMoney()
    {
        PlayerStats.Money -= 50;
    }

    public void BuildDefenderOn(Base @base)
    {
        if (PlayerStats.Money < defenderToBuild.cost)
        {
            Debug.Log("не хватает денег");
            return;
        }

        PlayerStats.Money -= defenderToBuild.cost;
        
        GameObject defender = (GameObject) Instantiate(defenderToBuild.prefab, @base.transform.position,
            defenderToBuild.prefab.transform.rotation);
        @base.defender = defender;
        var currentDef = defender.GetComponent<Defender>();
        @base.upgradeShopPrefab = currentDef.shopPrefab;
        
    }
    
    public void UpdateDefenderOn(Base @base)
    {
        if (PlayerStats.Money < defenderToBuild.updateCost)
        {
            Debug.Log("не хватает денег");
            return;
        }

        PlayerStats.Money -= defenderToBuild.updateCost;
        
        GameObject defender = (GameObject) Instantiate(defenderToBuild.prefab, @base.transform.position,
            defenderToBuild.prefab.transform.rotation);
        @base.defender = defender;
        @base.defenderIsUpdated = true;

    }
    
    public void SetDefenderToBuild(DefenderClass defender)
    {
        defenderToBuild = defender;
    }
}
