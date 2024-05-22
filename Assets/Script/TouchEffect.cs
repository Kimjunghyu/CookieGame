using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffect : MonoBehaviour
{
    public GameObject particlePrefab; // 파티클 시스템 프리팹
    public Canvas canvas; // 캔버스 참조

    void Update()
    {
        // 터치 입력 감지
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane));
                touchPosition.z = canvas.transform.position.z; // Z 값을 캔버스의 Z 값으로 설정
                CreateTouchEffect(touchPosition);
            }
        }

        // 마우스 입력 감지 (디버깅용, PC에서도 확인 가능)
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            mousePosition.z = canvas.transform.position.z; // Z 값을 캔버스의 Z 값으로 설정
            CreateTouchEffect(mousePosition);
        }
    }

    void CreateTouchEffect(Vector3 position)
    {
        // 파티클 시스템 오브젝트 생성
        GameObject particleEffect = Instantiate(particlePrefab, position, Quaternion.identity, canvas.transform);

        // 일정 시간 후 파티클 시스템 제거
        ParticleSystem ps = particleEffect.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            Destroy(particleEffect, ps.main.duration + ps.main.startLifetime.constantMax);
        }
        else
        {
            Destroy(particleEffect, 1f); // 기본값으로 1초 후 제거
        }
    }
}
