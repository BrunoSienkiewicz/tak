using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    public float enemyHealth = 100f;

    public void DeductHealth(float deductedhealth)
    {
        enemyHealth -= deductedhealth;

        if (enemyHealth <= 0)
            Destroy(gameObject);
    }
    void Update()
    {
        Debug.Log(enemyHealth);
    }
}
