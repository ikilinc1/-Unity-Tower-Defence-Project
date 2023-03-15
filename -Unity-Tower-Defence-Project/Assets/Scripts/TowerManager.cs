using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;
    public Tower activeTower;

    public Transform indicator;
    public bool isPlacing;
    public LayerMask whatIsPlacement, whatIsObstacle;

    public float topSafePercent = 15f;
    
    private void Awake()
    {
        instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlacing)
        {
            indicator.position = GetGridPosition();

            RaycastHit hit;
            if (Input.mousePosition.y > Screen.height * (1f - (topSafePercent / 100f)))
            {
                indicator.gameObject.SetActive(false);
            }
            else if (Physics.Raycast(indicator.position + new Vector3(0, -2f, 0), Vector3.up,out hit, 10f, whatIsObstacle))
            {
                indicator.gameObject.SetActive(false);
            }
            else
            {
                indicator.gameObject.SetActive(true);
                
                if (Input.GetMouseButtonDown(0))
                {
                    isPlacing = false;

                    Instantiate(activeTower, indicator.position, activeTower.transform.rotation);
                
                    indicator.gameObject.SetActive(false);
                }
            }
        }
    }

    public void StartTowerPlacement(Tower towerToPlace)
    {
        activeTower = towerToPlace;

        isPlacing = true;
        
        Destroy(indicator.gameObject);
        Tower placeTower = Instantiate(activeTower);
        placeTower.enabled = false;
        placeTower.GetComponent<Collider>().enabled = false;
        indicator = placeTower.transform;
        
        placeTower.rangeModel.SetActive(true);
        placeTower.rangeModel.transform.localScale = new Vector3(placeTower.range, 1f, placeTower.range);
    }

    private Vector3 GetGridPosition()
    {
        Vector3 location = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200f,whatIsPlacement ))
        {
            location = hit.point;
        }

        location.y = 0;
        return location;
    }
}
