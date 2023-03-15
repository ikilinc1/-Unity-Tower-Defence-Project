using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 3f;
    public LayerMask whatIsEnemy;

    
    public List<EnemyController> enemiesInRange = new List<EnemyController>();

    [HideInInspector]
    public bool enemiesUpdated;

    public GameObject rangeModel;
    
    public float checkTime = 0.2f;
    private float checkCounter;
    
    private Collider[] collidersInRange;

    // Start is called before the first frame update
    void Start()
    {
        checkCounter = checkTime;
    }

    // Update is called once per frame
    void Update()
    {
        enemiesUpdated = false;
        checkCounter -= Time.deltaTime;
        if (checkCounter <= 0)
        {
            checkCounter = checkTime;
            
            collidersInRange = Physics.OverlapSphere(transform.position, range, whatIsEnemy);
        
            enemiesInRange.Clear();
            foreach (Collider col in collidersInRange)
            {
                enemiesInRange.Add(col.GetComponent<EnemyController>());
            }

            enemiesUpdated = true;
        }
    }
}
