using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffect : MonoBehaviour
{
    public GameObject particlePrefab;
    public Canvas canvas;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane));
                touchPosition.z = canvas.transform.position.z;
                CreateTouchEffect(touchPosition);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            mousePosition.z = canvas.transform.position.z;
            CreateTouchEffect(mousePosition);
        }
    }

    void CreateTouchEffect(Vector3 position)
    {
        GameObject particleEffect = Instantiate(particlePrefab, position, Quaternion.identity, canvas.transform);
        ParticleSystem ps = particleEffect.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            Destroy(particleEffect, ps.main.duration + ps.main.startLifetime.constantMax);
        }
        else
        {
            Destroy(particleEffect, 1f);
        }
    }
}
