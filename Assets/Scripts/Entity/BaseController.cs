using UnityEngine;

// 캐릭터의 이동, 회전, 넉백 등의 기본 컨트롤을 담당하는 기반 클래스
public class BaseController : MonoBehaviour
{
    // Rigidbody2D 컴포넌트를 캐시해 물리 계산에 사용
    protected Rigidbody2D _rigidbody;

    // 캐릭터의 시각적인 방향(좌우 뒤집기)을 위한 SpriteRenderer
    [SerializeField] private SpriteRenderer characterRenderer;
    //SerializeField통해 인스펙터에 공개
    

    // 무기 회전을 위한 피벗 위치
    [SerializeField] private Transform weaponPivot;

    // 현재 이동 방향 (플레이어나 AI가 결정)
    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }
    //movementDirection이동하는 방향지정 바라보는 방향지정
    // 마우스 또는 목표를 향한 방향
    protected Vector2 lookDirection = Vector2.zero;

    //넉백에 대한 방향을 가져온다
    public Vector2 LookDirection { get { return lookDirection; } }

    // 넉백 힘을 저장하는 벡터
    private Vector2 knockback = Vector2.zero;

    // 넉백이 유지되는 시간
    private float knockbackDuration = 0.0f;

    // 오브젝트가 활성화될 때 호출됨 (컴포넌트 초기화용)
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>(); // Rigidbody2D 가져오기
    }

    // 게임 시작 직후 한 번만 호출됨
    protected virtual void Start()
    {
        // 상속받는 쪽에서 필요한 초기화 작성 가능
    }

    // 매 프레임마다 호출됨 (입력 처리 및 회전)
    protected virtual void Update()
    {
        HandleAction();              // 입력 처리 또는 AI 행동 처리
        Rotate(lookDirection);       // 무기 및 캐릭터 시각적 방향 회전
    }

    // 고정된 시간 간격마다 호출됨 (물리 계산용)
    protected virtual void FixedUpdate()
    {
        Movement(movementDirection); // 이동 처리
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime; // 넉백 시간 감소
        }
    }

    // 입력 또는 AI 행동을 처리할 수 있는 가상 함수
    protected virtual void HandleAction()
    {
        // PlayerController 등에서 override해서 사용
    }

    // 이동 처리 (넉백 고려 포함)
    private void Movement(Vector2 direction)
    {
        Vector2 aa;
        aa = direction * 5;
        direction = direction * 5; // 기본 이동 속도 설정

        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f;           // 넉백 중이면 속도 감소
            direction += knockback;      // 넉백 방향 추가
        }

        _rigidbody.velocity = direction; // 물리적으로 이동 적용
    }

    // 캐릭터 회전 처리 (마우스 방향 또는 목표 방향)
    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 회전 각도 계산
        bool isLeft = Mathf.Abs(rotZ) > 90f; // 좌우 판단

        characterRenderer.flipX = isLeft; // 좌우 뒤집기 적용

        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ); // 무기 회전 적용
        }
    }

    // 넉백 적용 함수
    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration; // 지속 시간 설정
        knockback = -(other.position - transform.position).normalized * power; // 방향 계산 후 넉백 설정
    }
}
