using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Threading;

public class JoyStickHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject character; // ĳ���� ������Ʈ.
    public RectTransform touchArea; // Joystick Touch Area �̹����� RectTransform.
    public Image outerPad; // OuterPad �̹���.
    public Image innerPad; // InnerPad �̹���.

    private Vector2 joystickVector; // ���̽�ƽ�� ���⺤������ �÷��̾�� �ѱ� ��������.

    private float speed = 1f; // ĳ���� ���ǵ�
    private float rotateSpeed = 3f; // ȸ�� �ӵ�

    private Coroutine runningCoroutine; // �ε巯�� ȸ�� �ڷ�ƾ


    void Start()
    {

        if(Managers.Game.Player != null)
        {
            character = Managers.Game.Player;
        }
        else
        {
            character = Managers.Game.Player_Right;
        }



        //�� ������ �׽�Ʈ��
        //character = Managers.Game.Enemy;

        StartCoroutine(C_Move());
    }


    IEnumerator C_Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            C_Move move = new C_Move();
            move.posX = character.transform.position.x;
            move.posY = character.transform.position.y;
            move.angle = character.transform.eulerAngles.z;
            Managers.Network.Send(move.Write());
        }
    }

    void Update()
    {
        if (character != null)
            character.GetComponent<Rigidbody2D>().velocity = character.transform.up * speed;
        // ĳ���ʹ� 3�� �ӵ��� ��� ����
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
                // Joystick Touch Area�� ���� ���ϱ� ( -0.5 ~ 0.5 )

                joystickVector = new Vector2(localPoint.x * 2.6f, localPoint.y * 2);
                // ���̽�ƽ ���� ���� (2.6�� 2�� ������ ���� TouchArea�� ���� ������)

                TurnAngle(joystickVector);
                // Character���� ���̽�ƽ ���� �ѱ��

                joystickVector = (joystickVector.magnitude > 0.35f) ? joystickVector.normalized * 0.35f : joystickVector;
                // innerPad �̹����� outerPad�� �Ѿ�ٸ� ��ġ �������ֱ�

                innerPad.rectTransform.anchoredPosition = new Vector2(joystickVector.x * (outerPad.rectTransform.sizeDelta.x),
                    joystickVector.y * (outerPad.rectTransform.sizeDelta.y));
                // innerPad �̹��� ��ġ�� ������ �ű��

            }
        }

        catch (Exception ex)
        {
            //Debug.Log("���̽�ƽ ��Ʈ�ѷ� OnDrag ����"); 
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData); // ��ġ�� ���۵Ǹ� OnDrag ó��.
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        try
        {
            innerPad.rectTransform.anchoredPosition = Vector2.zero;
        }
        catch
        {
            //Debug.Log("���̽�ƽ ��Ʈ�ѷ� OnPointerUp ����");
        }
        
    }


    private void TurnAngle(Vector3 currentJoystickVec)
    {
        Vector3 originJoystickVec = character.transform.up;
        // character�� �ٶ󺸰� �ִ� ����
        
        float angle = Vector3.Angle(currentJoystickVec, originJoystickVec);
        int sign = (Vector3.Cross(currentJoystickVec, originJoystickVec).z > 0) ? -1 : 1;
        // angle: ���� �ٶ󺸰� �ִ� ���Ϳ�, ���̽�ƽ ���� ���� ������ ����
        // sign: character�� �ٶ󺸴� ���� ��������, ����:+ ������:-

        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }
        runningCoroutine = StartCoroutine(RotateAngle(angle, sign));
        // �ڷ�ƾ�� �������̸� ���� ���� �ڷ�ƾ �ߴ� �� �ڷ�ƾ ���� 
        // �ڷ�ƾ�� �� ���� �����ϵ���.
        // => ȸ�� �߿� ���ο� ȸ���� ������ ���, ȸ�� ���̴� ���� ���߰� ���ο� ȸ���� ��.
    }


    IEnumerator RotateAngle(float angle, int sign)
    {
        float mod = angle % rotateSpeed; // ���� ���� ���
        for (float i = mod; i < angle; i += rotateSpeed)
        {
            character.transform.Rotate(0, 0, sign * rotateSpeed); // ĳ���� rotateSpeed��ŭ ȸ��
            yield return new WaitForSeconds(0.01f); // 0.01�� ���
        }
        //character.transform.Rotate(0, 0, sign * mod); // ���� ���� ȸ��
    }

    public void OnConnectedToServer()
    {
        //������ġ



    }
}
