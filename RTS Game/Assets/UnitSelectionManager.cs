using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager instance { get; set; }

    public List<GameObject> allUnitsList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    public LayerMask Clickable;
    public LayerMask ground;
    public LayerMask attackable;
    public bool attackCursorVisible;

    public GameObject groundMarker;

    private Camera cam;

    //singleton instance method to make sure only 1 UnitSelectionManager exists at one time
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    MultiSelect(hit.collider.gameObject);
                }
                else
                {
                    SelectByClicking(hit.collider.gameObject);
                }

            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift) == false)
                {
                    DeselectAll();
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && unitsSelected.Count > 0)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                groundMarker.transform.position = hit.point;
                groundMarker.SetActive(false);
                groundMarker.SetActive(true);
            }
        }

        if (unitsSelected.Count > 0 && AtleastOneOffensiveUnit(unitsSelected))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, attackable))
            {
                Debug.Log("Enemy Hovered with mouse");

                attackCursorVisible = true;

                if (Input.GetMouseButtonDown(1))
                {
                    Transform target = hit.transform;

                    foreach (GameObject unit in unitsSelected)
                    {
                        if (unit.GetComponent<AttackController>())
                        {
                            unit.GetComponent<AttackController>().targetToAttack = target;
                        }
                    }
                }
            }
            else
            {
                attackCursorVisible = false;
            }
        }


    }

    private void SelectByClicking(GameObject unit)
    {
        DeselectAll();

        unitsSelected.Add(unit);
        SelectUnit(unit, true);
    }

    private void MultiSelect(GameObject unit)
    {
        if (unitsSelected.Contains(unit) == false)
        {
            unitsSelected.Add(unit);
            SelectUnit(unit, true);
        }
        else
        {
            SelectUnit(unit, false);
            unitsSelected.Remove(unit);
        }
    }

    public void DeselectAll()
    {
        foreach (var unit in unitsSelected)
        {
            SelectUnit(unit, false);
        }
        groundMarker.SetActive(false);
        unitsSelected.Clear();
    }
    private void SelectUnit(GameObject unit, bool isSelected)
    {
        TriggerSelectionIndicator(unit, isSelected);
        EnableUnitMovement(unit, isSelected);
    }

    private void EnableUnitMovement(GameObject unit, bool shouldMove)
    {
        unit.GetComponent<UnitMovement>().enabled = shouldMove;
    }
    private void TriggerSelectionIndicator(GameObject unit, bool isVisible)
    {
        unit.transform.GetChild(0).gameObject.SetActive(isVisible);
    }
    public void DragSelect(GameObject unit)
    {
        if (unitsSelected.Contains(unit) == false)
        {
            unitsSelected.Add(unit);
            SelectUnit(unit, true);
        }
    }

    private bool AtleastOneOffensiveUnit(List<GameObject> unitsSelected)
    {
        foreach (GameObject unit in unitsSelected)
        {
            if (unit.GetComponent<AttackController>())
            {
                return true;
            }
        }
        return false;
    }
}
