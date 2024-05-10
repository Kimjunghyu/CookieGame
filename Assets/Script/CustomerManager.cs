using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public GameObject[] customers;
    public float minSpawnTime = 12f;
    public float maxSpawnTime = 20f;

    private void Start()
    {
        StartCoroutine(SpawnCustomersRandomly());
    }

    private IEnumerator SpawnCustomersRandomly()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            ActivateRandomCustomer();
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