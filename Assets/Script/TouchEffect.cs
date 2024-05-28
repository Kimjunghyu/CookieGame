using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffect : MonoBehaviour
{
    public GameObject particlePrefab;
    public Camera mainCamera;
    public Camera particleCamera;
    public int poolSize = 10;
    public float minTouchMovement = 10f;

    private List<GameObject> particlePool = new List<GameObject>();
    private Vector3 lastTouchPosition;
    private Vector3 lastMousePosition;

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
        ProcessTouch();
        ProcessMouse();
    }

    void ProcessTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, mainCamera.nearClipPlane));
            touchPosition.z = 0;

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPosition = touchPosition;
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if (Vector3.Distance(touchPosition, lastTouchPosition) >= minTouchMovement)
                {
                    CreateTouchEffect(touchPosition);
                    lastTouchPosition = touchPosition;
                }
            }
        }
    }

    void ProcessMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane));
            lastMousePosition.z = 0;
            CreateTouchEffect(lastMousePosition);
        }
    }

    void CreateTouchEffect(Vector3 position)
    {
        GameObject particleEffect = GetParticleFromPool();
        if (particleEffect != null)
        {
            particleEffect.transform.position = position;
            particleEffect.SetActive(true);

            ParticleSystem ps = particleEffect.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                ps.Play();
                StartCoroutine(ReturnToPool(particleEffect, ps.main.duration + ps.main.startLifetime.constantMax - 2f));
            }
            else
            {
                StartCoroutine(ReturnToPool(particleEffect, 1f));
            }
        }
        else
        {
            Debug.LogWarning("No available particles in the pool.");
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

        return null;
    }

    IEnumerator ReturnToPool(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }
}
