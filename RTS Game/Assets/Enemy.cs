using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void TakeDamage(int damageToInflict)
    {
        health -= damageToInflict;
    }
}
