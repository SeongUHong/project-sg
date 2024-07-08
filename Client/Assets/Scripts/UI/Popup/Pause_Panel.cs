using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Panel : UIBase
{

    public override void Init()
    {

    }
    public void Awake()
    {
        transform.gameObject.SetActive(false); // ������ ���۵Ǹ� �˾� â�� ������ �ʵ��� �Ѵ�.
    }

    public void Show()
    {
        new WaitForSeconds(Define.NEXT_DELAY_TIME);
        transform.gameObject.SetActive(true);
    }


}
