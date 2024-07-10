using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Threading;

public class JoyStickHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject character; // 캐릭터 오브젝트.
    public RectTransform touchArea; // Joystick Touch Area 이미지의 RectTransform.
    public Image outerPad; // OuterPad 이미지.
    public Image innerPad; // InnerPad 이미지.

    private Vector2 joystickVector; // 조이스틱의 방향벡터이자 플레이어에게 넘길 방향정보.

    private float speed = 1f; // 캐릭터 스피드
    private float rotateSpeed = 270.0f; // 회전 속도

    private Coroutine runningCoroutine; // 부드러운 회전 코루틴

    private Coroutine cMove;

    void Start()
    {

        //적 움직임 테스트용
        //character = Managers.Game.Enemy;

        cMove = StartCoroutine(C_Move());
    }

    public void StopCoroutine()
    {
        StopCoroutine(cMove);
    }

    IEnumerator C_Move()
    {
        while (true)
         {
            yield return new WaitForSeconds(0.2f);
            C_Move move = new C_Move();
            move.posX = character.transform.position.x;
            move.posY = character.transform.position.y;
            move.angle = character.transform.eulerAngles.z;
            Managers.Network.Send(move.Write());
         }

        
    }


     
    void Update()
    {
        if (Managers.Game.IsPause == true)
        {
            if (Managers.Game.IsLeft)
            {
                character = Managers.Game.Player;
            }
            else
            {
                character = Managers.Game.Player_Right;
            }

        }

        if (character != null)
            character.GetComponent<Rigidbody2D>().velocity = character.transform.up * speed;
        // 캐릭터는 3의 속도로 계속 전진

        if (Managers.Game.PlayerDeadFlag)
        {
            character = null;
            StopCoroutine();
        }
        if (Managers.Game.EnemyDeadFlag)
        {
            speed = 0;
        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        

        try
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(touchArea,
            eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
            {
                character.GetComponent<Rigidbody2D>().velocity = character.transform.up * speed;

                localPoint.x = (localPoint.x / touchArea.sizeDelta.x);
                localPoint.y = (localPoint.y / touchArea.sizeDelta.y);
                // Joystick Touch Area의 비율 구하기 ( -0.5 ~ 0.5 )

                joystickVector = new Vector2(localPoint.x * 2.6f, localPoint.y * 2);
                // 조이스틱 벡터 조절 (2.6과 2를 곱해준 것은 TouchArea의 비율 때문임)

                TurnAngle(joystickVector);
                // Character에게 조이스틱 방향 넘기기

                joystickVector = (joystickVector.magnitude > 0.35f) ? joystickVector.normalized * 0.35f : joystickVector;
                // innerPad 이미지가 outerPad를 넘어간다면 위치 조절해주기

                innerPad.rectTransform.anchoredPosition = new Vector2(joystickVector.x * (outerPad.rectTransform.sizeDelta.x),
                    joystickVector.y * (outerPad.rectTransform.sizeDelta.y));
                // innerPad 이미지 터치한 곳으로 옮기기

            }
        }

        catch (Exception ex)
        {
            //Debug.Log("조이스틱 컨트롤러 OnDrag 에러"); 
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData); // 터치가 시작되면 OnDrag 처리.
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        try
        {
            innerPad.rectTransform.anchoredPosition = Vector2.zero;
        }
        catch
        {
            //Debug.Log("조이스틱 컨트롤러 OnPointerUp 에러");
        }
        
    }

    private void TurnAngle(Vector3 currentJoystickVec)
    {
        Vector3 originJoystickVec = character.transform.up;
        float angle = Vector3.Angle(currentJoystickVec, originJoystickVec);
        int sign = (Vector3.Cross(currentJoystickVec, originJoystickVec).z > 0) ? -1 : 1;

        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }
        runningCoroutine = StartCoroutine(RotateAngle(angle, sign));
    }

    IEnumerator RotateAngle(float angle, int sign)
    {
        float rotateSpeedPerSecond = rotateSpeed; // 초당 회전 각도로 설정
        float remainingAngle = angle;

        while (remainingAngle > 0)
        {
            // 프레임당 회전할 양 계산
            float rotateAmount = rotateSpeedPerSecond * Time.deltaTime;

            // 남은 각도가 이번 프레임에서 회전할 양보다 작으면 남은 각도만큼만 회전
            rotateAmount = Mathf.Min(rotateAmount, remainingAngle);

            // 회전 수행
            character.transform.Rotate(0, 0, sign * rotateAmount);

            // 남은 회전 각도 갱신
            remainingAngle -= rotateAmount;

            yield return null;
        }

    }
}
