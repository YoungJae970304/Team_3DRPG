using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePlayer : Player
{
    public override void Attack()
    {
        if (_playerInput._atkInput.Count > 0)
        {
            switch (_playerInput._atkInput.Dequeue())
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

            CanAtkInputOffTimer(0.5f);
            AtkOffTimer(1.5f);
        }
    }

    float _curCAITime = 0;

    // ���� �ִϸ��̼� �̺�Ʈ�� �̵� ����
    void CanAtkInputOffTimer(float targetTime)
    {
        _curCAITime += Time.deltaTime;

        if (_curCAITime >= targetTime)
        {
            _curCAITime = 0;
            _canAtkInput = true;
        }
    }

    float _curATime = 0;
    void AtkOffTimer(float targetTime)
    {
        _curATime += Time.deltaTime;

        if (_curATime >= targetTime)
        {
            _curATime = 0;
            _attacking = false;
        }
    }
}
