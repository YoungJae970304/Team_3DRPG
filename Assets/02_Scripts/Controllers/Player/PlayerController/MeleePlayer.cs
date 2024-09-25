using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePlayer : Player
{
    public override void Attack()
    {
        if (_playerInput._atkInput.Count != 0)
        {
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

            AtkOffTimer(0.5f);
        }
    }

    float _curTime = 0;

    void AtkOffTimer(float targetTime)
    {
        _curTime += Time.deltaTime;

        if (_curTime >= targetTime)
        {
            _curTime = 0;
            _attacking = false; // ���� �ִϸ��̼� �̺�Ʈ�� �̵� ����
        }
    }
}
