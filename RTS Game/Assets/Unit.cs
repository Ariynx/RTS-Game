using UnityEngine;

public class Unit : MonoBehaviour
{
    private float unitHealth;
    public float unitMaxHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UnitSelectionManager.instance.allUnitsList.Add(gameObject);
        unitHealth = unitMaxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        UnitSelectionManager.instance.allUnitsList.Remove(gameObject);
    }
    internal void TakeDamage(int damageToInflict)
    {
        unitHealth -= damageToInflict;
        UpdateHealth();
    }
    private void UpdateHealth()
    {
        //where ui to display health would go

        if (unitHealth < 0)
        {
            //Add dying effects later
            OnDestroy();
            Destroy(gameObject);
        }
    }
}
