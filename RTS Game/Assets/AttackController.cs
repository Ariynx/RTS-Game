using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Transform targetToAttack;

    public Material idleStateMaterial;
    public Material followStateMaterial;
    public Material attackStateMaterial;

    public bool isPlayer;

    public int unitDamage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayer && other.CompareTag("Enemy") && targetToAttack == null)
        {
            targetToAttack = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && targetToAttack != null)
        {
            targetToAttack = null;
        }
    }

    public void SetIdleMaterial()
    {
        GetComponent<Renderer>().material = idleStateMaterial;
    }
    public void SetFollowMaterial()
    {
        GetComponent<Renderer>().material = followStateMaterial;
    }
    public void SetAttackMaterial()
    {
        GetComponent<Renderer>().material = attackStateMaterial;
    }
    //debug method to visualize and balance ranges
    private void OnDrawGizmos()
    {
        //Follow Distance area
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 75f * 0.2f);
        //Attack Distance Area
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10f);
        //Stop Attack Distance Area
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 12f);

    }

}
