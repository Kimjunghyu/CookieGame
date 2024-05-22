using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffect : MonoBehaviour
{
    public GameObject particlePrefab; // ��ƼŬ �ý��� ������
    public Canvas canvas; // ĵ���� ����

    void Update()
    {
        // ��ġ �Է� ����
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane));
                touchPosition.z = canvas.transform.position.z; // Z ���� ĵ������ Z ������ ����
                CreateTouchEffect(touchPosition);
            }
        }

        // ���콺 �Է� ���� (������, PC������ Ȯ�� ����)
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            mousePosition.z = canvas.transform.position.z; // Z ���� ĵ������ Z ������ ����
            CreateTouchEffect(mousePosition);
        }
    }

    void CreateTouchEffect(Vector3 position)
    {
        // ��ƼŬ �ý��� ������Ʈ ����
        GameObject particleEffect = Instantiate(particlePrefab, position, Quaternion.identity, canvas.transform);

        // ���� �ð� �� ��ƼŬ �ý��� ����
        ParticleSystem ps = particleEffect.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            Destroy(particleEffect, ps.main.duration + ps.main.startLifetime.constantMax);
        }
        else
        {
            Destroy(particleEffect, 1f); // �⺻������ 1�� �� ����
        }
    }
}
