using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblemDamagedState : MonsterBaseState
{
    public GoblemDamagedState(Goblem goblem) : base(goblem)
    {
        _goblem = goblem;
        _pStat = _player._playerStat;
    }
    PlayerStat _pStat;
public override void OnStateEnter()
    {
        //�˹�, ������ �ޱ�
        //�ӽ÷� ������ �������� �־���� ���� �÷��̾� ������ �� �޾ƿ��� ����
        _goblem._nav.enabled = false;
        _goblem.GetComponent<Rigidbody>().isKinematic = false;
    }

    public override void OnStateExit()
    {
        //�ڷ�ƾ ���߱�?
        //���´� ���x�ٰ� ���߿� ������ ����ġ ��������Ʈ�� ���ϱ�
        _goblem._nav.enabled = true;
        _goblem.GetComponent<Rigidbody>().isKinematic = true;
    }

    public override void OnStateUpdate()
    {
        _goblem.StartCoroutine(StartDamege(_pStat.ATK, _goblem.transform.position, 0.5f, 0.5f)); //��ũ ������ �÷��̾� ���ݷ����� ��ü ����
    }
    public IEnumerator StartDamege(int damage, Vector3 playerPosition, float delay, float pushBack)//�˹�ó�� �߿�!
    {
        yield return new WaitForSeconds(delay);

        try//�̰� �����غ��� ������ ���ٸ� ����
        {

            Vector3 diff = playerPosition - _goblem.transform.position;
            diff = diff / diff.sqrMagnitude;
            _goblem._nav.isStopped = true;
            _goblem.GetComponent<Rigidbody>().
            AddForce((_goblem.transform.position - new Vector3(diff.x, diff.y, 0f)) * 50f * pushBack);

        }
        catch (MissingReferenceException e)// ������ �ִٸ� �����޼��� ���
        {
            Debug.Log(e.ToString());
        }
        //����ó����
    }
}
