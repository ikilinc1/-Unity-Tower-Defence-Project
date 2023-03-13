using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public float timeBetweenAttacks;
    public float damagePerAttack;

    private Path thePath;
    private int currentPoint;
    private bool reachedEnd;
    private float attackCounter;
    private int selectedAttackPoint;

    private Castle theCastle;

    // Start is called before the first frame update
    void Start()
    {
        if (thePath == null)
        {
            thePath = FindObjectOfType<Path>();
        }

        if (theCastle == null)
        {
            theCastle = FindObjectOfType<Castle>();
        }

        attackCounter = timeBetweenAttacks;
    }

    // Update is called once per frame
    void Update()
    {
        if (theCastle.currentHealth > 0)
        {
            if (!reachedEnd)
            {
                transform.LookAt(thePath.points[currentPoint]);
            
                transform.position = Vector3.MoveTowards(transform.position, thePath.points[currentPoint].position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, thePath.points[currentPoint].position) < 0.01f)
                {
                    currentPoint++;
                    if (currentPoint >= thePath.points.Length)
                    {
                        reachedEnd = true;

                        selectedAttackPoint = Random.Range(0, theCastle.attackPoints.Length);
                    }
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    theCastle.attackPoints[selectedAttackPoint].position, moveSpeed * Time.deltaTime);
            
                attackCounter -= Time.deltaTime;
                if (attackCounter <= 0)
                {
                    attackCounter = timeBetweenAttacks;
                
                    theCastle.TakeDamage(damagePerAttack);
                }
            }
        }
        
       
    }

    public void Setup(Castle newCastle, Path newPath)
    {
        theCastle = newCastle;
        thePath = newPath;
    }
}
