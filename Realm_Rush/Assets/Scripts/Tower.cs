using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    [SerializeField] float buildDelay = 1f;

    void Start()
    {
        StartCoroutine(Build());
    }

    IEnumerator Build() //build timer rather than the tower just appearing immediately
    {
        //instead of setting active to false by default, do it right away when build starts
        foreach (Transform child in transform) //loop through this object's children objects
        {
            child.gameObject.SetActive(false); //set them inactive
            foreach (Transform grandchild in child) //do the same for the children of children
            {
                grandchild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform) //do the same as above to reenable
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay); //delay between each piece of the tower building

            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
            }
        }
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null)
        {
            return false;
        }

        if (bank.CurrentBalance >= cost)
        {
            Instantiate(tower, position, Quaternion.identity);
            bank.Withdraw(cost);
            return true;
        }
        return false; //if all else fails return false
    }
}
