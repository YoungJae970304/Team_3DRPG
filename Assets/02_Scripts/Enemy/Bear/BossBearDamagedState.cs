using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBearDamagedState : MonsterBaseState
{
    public BossBearDamagedState(BossBear bossBear) : base(bossBear)
    {
        _bossBear = bossBear;
    }
    PlayerStat _pStat;
    public override void OnStateEnter()
    {
        //�˹�, ������ �ޱ�
        //�ӽ÷� ������ �������� �־���� ���� �÷��̾� ������ �� �޾ƿ��� ����
        _bossBear._nav.enabled = false;
        _bossBear.GetComponent<Rigidbody>().isKinematic = false;
    }

    public override void OnStateExit()
    {
        //�ڷ�ƾ ���߱�?
        //���´� ���x�ٰ� ���߿� ������ ����ġ ��������Ʈ�� ���ϱ�
        _bossBear._nav.enabled = true;
        _bossBear.GetComponent<Rigidbody>().isKinematic = true;
    }

    public override void OnStateUpdate()
    {
        _bossBear.StartCoroutine(StartDamege(_bossBear._bStat.Attack, _bossBear.transform.position, 0.5f, 0.5f)); //��ũ ������ �÷��̾� ���ݷ����� ��ü ����
    }
    public IEnumerator StartDamege(int damage, Vector3 playerPosition, float delay, float pushBack)//�˹�ó�� �߿�!
    {
        yield return new WaitForSeconds(delay);

        try//�̰� �����غ��� ������ ���ٸ� ����
        {

            Vector3 diff = playerPosition - _bossBear.transform.position;
            diff = diff / diff.sqrMagnitude;
            _bossBear._nav.isStopped = true;
            _bossBear.GetComponent<Rigidbody>().
            AddForce((_bossBear.transform.position - new Vector3(diff.x, diff.y, 0f)) * 50f * pushBack);

        }
        catch (MissingReferenceException e)// ������ �ִٸ� �����޼��� ���
        {
            Debug.Log(e.ToString());
        }
        //����ó����
    }
}
