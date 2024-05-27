using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffect : MonoBehaviour
{
    public GameObject particlePrefab;
    public Camera mainCamera;
    public Camera particleCamera;
    public int poolSize = 10;

    private List<GameObject> particlePool = new List<GameObject>();

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (particleCamera == null)
        {
            Debug.LogError("Particle Camera is not assigned.");
        }

        for (int i = 0; i < poolSize; i++)
        {
            GameObject particleEffect = Instantiate(particlePrefab);
            particleEffect.SetActive(false);
            particlePool.Add(particleEffect);
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, mainCamera.nearClipPlane));
                touchPosition.z = 0;
                CreateTouchEffect(touchPosition);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane));
            mousePosition.z = 0;
            CreateTouchEffect(mousePosition);
        }
    }

    void CreateTouchEffect(Vector3 position)
    {
        GameObject particleEffect = GetParticleFromPool();
        particleEffect.transform.position = position;
        particleEffect.SetActive(true);

        ParticleSystem ps = particleEffect.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            ps.Play();
            StartCoroutine(ReturnToPool(particleEffect, ps.main.duration + ps.main.startLifetime.constantMax - 2.5f));
        }
        else
        {
            StartCoroutine(ReturnToPool(particleEffect, 1f));
        }
    }

    GameObject GetParticleFromPool()
    {
        foreach (GameObject particle in particlePool)
        {
            if (!particle.activeInHierarchy)
            {
                return particle;
            }
        }

        GameObject newParticle = Instantiate(particlePrefab);
        newParticle.SetActive(false);
        particlePool.Add(newParticle);
        return newParticle;
    }

    IEnumerator ReturnToPool(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }
}
