using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Base : MonoBehaviour
{
    public Color hoverColor;

    public GameObject defender;
    public Transform shopPrefab;
    public Transform shopDefDestroy;
    public Transform spawnPoint;

    private SpriteRenderer rend;
    private Color startColor;

    private BuildManager buildManager;
    private Transform prefab;

    public Transform upgradeShopPrefab;
    public bool defenderIsUpdated = false;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.color;
    }

    void OnMouseDown()
    {
        if (Pause) return;
        
        
        if (GameManager.currentShop != null)
        {
            Destroy(GameManager.currentShop.gameObject);
        }

        Transform shopCanvas;

        if (defender == null)
        {
            shopCanvas = Instantiate(shopPrefab, spawnPoint.position, shopPrefab.transform.rotation);
            var shop = shopCanvas.GetComponentInChildren<Shop>();
            shop.baseToBuildOn = this;
        }
         else if(defenderIsUpdated == false)
        {
            shopCanvas = Instantiate(upgradeShopPrefab, spawnPoint.position, upgradeShopPrefab.transform.rotation);
            var shop = shopCanvas.GetComponentInChildren<Shop>();
            shop.baseToBuildOn = this;
        }
        else
        {
            shopCanvas = Instantiate(shopDefDestroy, spawnPoint.position, shopDefDestroy.transform.rotation);
            var shop = shopCanvas.GetComponentInChildren<Shop>();
            shop.baseToBuildOn = this;
        }

        GameManager.currentShop = shopCanvas;
    }

    void OnMouseEnter()
    {
        if (Pause) return;
        rend.color = hoverColor;
    }

    void OnMouseExit()
    {
        if (Pause) return;
        rend.color = startColor;
    }

    bool Pause
    {
        get => PlayerStats.Lose == true || Time.timeScale == 0f;
    }
    
}
