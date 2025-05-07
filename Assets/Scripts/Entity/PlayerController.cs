using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerController는 BaseController를 상속받아 플레이어의 입력 및 조작을 처리
public class PlayerController : BaseController
{
    // 메인 카메라를 참조할 변수 (마우스 위치를 월드 좌표로 변환할 때 사용)
    private Camera camera;

    // Start는 게임이 시작될 때 한 번 실행됨
    protected override void Start()
    {
        base.Start();               // 부모 클래스의 Start() 실행 (확장 가능)
        camera = Camera.main;       // 메인 카메라 참조 가져오기
    }

    // 플레이어의 입력을 매 프레임마다 처리
    protected override void HandleAction()
    {
        // 수평, 수직 키보드 입력 받기 (A/D, W/S 혹은 방향키)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // 입력 벡터를 정규화하여 이동 방향 설정
        movementDirection = new Vector2(horizontal, vertical).normalized;

        // 마우스 커서 위치를 가져와 화면(Screen) 좌표에서 월드(World) 좌표로 변환
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition);

        // 현재 위치에서 마우스 위치까지의 방향 계산
        lookDirection = (worldPos - (Vector2)transform.position);

        // 너무 가까우면 방향을 무시 (오작동 방지)
        if (lookDirection.magnitude < .9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            // 정규화하여 방향만 저장
            lookDirection = lookDirection.normalized;
        }
    }
}
