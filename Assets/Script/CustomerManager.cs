using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public GameObject[] customers;
    public float minSpawnTime = 20f;
    public float maxSpawnTime = 30f;

    private void OnDisable()
    {
        foreach (var customer in customers)
            customer.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnCustomersRandomly());
    }

    private IEnumerator SpawnCustomersRandomly()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            ActivateRandomCustomer();
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void ActivateRandomCustomer()
    {
        List<GameObject> inactiveCustomers = new List<GameObject>();

        foreach (GameObject customer in customers)
        {
            if (!customer.activeSelf)
            {
                inactiveCustomers.Add(customer);
            }
        }

        if (inactiveCustomers.Count > 0)
        {
            int randomIndex = Random.Range(0, inactiveCustomers.Count);
            inactiveCustomers[randomIndex].SetActive(true);
        }
    }
}
