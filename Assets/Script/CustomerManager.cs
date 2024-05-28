using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public GameObject[] customers;
    public float minSpawnTime = 5f;
    public float maxSpawnTime = 8f;
    private Coroutine spawnCoroutine;

    private void OnDisable()
    {
        foreach (var customer in customers)
        {
            customer.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (GameManager.instance.isPlaying)
        {
            StartSpawning();
        }
    }

    private void Update()
    {
        if (GameManager.instance.isPlaying)
        {
            int currentRepute = GameManager.instance.repute;
            ReputeData reputeData = ReputeDataLoad.instance.GetReputeData(currentRepute);

            if (reputeData != null)
            {
                minSpawnTime = reputeData.CusVisitTimerStart;
                maxSpawnTime = reputeData.CusVisitTimerEnd;
            }
        }

        if (GameManager.instance.isPlaying && spawnCoroutine == null)
        {
            StartSpawning();
        }
        else if (!GameManager.instance.isPlaying && spawnCoroutine != null)
        {
            StopSpawning();
        }
    }

    private void StartSpawning()
    {
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnCustomersRandomly());
        }
    }

    private void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnCustomersRandomly()
    {
        while (GameManager.instance.isPlaying)
        {
            yield return new WaitForSeconds(3);
            ActivateRandomCustomer();
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            Debug.Log(minSpawnTime);
            Debug.Log(maxSpawnTime);
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
