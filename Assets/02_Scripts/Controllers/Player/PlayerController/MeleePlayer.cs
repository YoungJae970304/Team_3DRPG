using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePlayer : Player
{
    public override IEnumerator Attack()
    {
        Debug.Log("�ٰŸ� ĳ���� ����");
        switch (AtkCount)
        {
            case 0:
                Logger.Log("������");
                break;
            case 1:
                Logger.Log("�⺻���� 1Ÿ");
                break;
            case 2:
                Logger.Log("�⺻���� 2Ÿ");
                break;
            case 3:
                Logger.Log("�⺻���� 3Ÿ");
                break;
            default:
                Logger.LogError("������ ������ �ƴ�");
                break;
        }

        yield return new WaitForSeconds(0.5f);
        _attacking = false;
    }
}
