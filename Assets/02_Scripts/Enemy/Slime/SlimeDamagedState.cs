using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDamagedState : MonsterBaseState
{
    public SlimeDamagedState(Slime slime) : base(slime) 
    {
        _slime = slime;
        _pStat = _player._playerStat;
    }
    PlayerStat _pStat;
    public override void OnStateEnter()
    {
        //�˹�, ������ �ޱ�
        //�ӽ÷� ������ �������� �־���� ���� �÷��̾� ������ �� �޾ƿ��� ����
        _slime._nav.enabled = false;
        _slime.GetComponent<Rigidbody>().isKinematic = false;
    }

    public override void OnStateExit()
    {
        //�ڷ�ƾ ���߱�?
        //���´� ���x�ٰ� ���߿� ������ ����ġ ��������Ʈ�� ���ϱ�
        _slime._nav.enabled = true;
        _slime.GetComponent<Rigidbody>().isKinematic = true;
    }

    public override void OnStateUpdate()
    {
        _slime.StartCoroutine(StartDamege(_slime._sStat.Attack, _slime.transform.position, 0.5f, 0.5f));
    }
    public IEnumerator StartDamege(int damage, Vector3 playerPosition, float delay, float pushBack)//�˹�ó�� �߿�!
    {
        yield return new WaitForSeconds(delay);

        try//�̰� �����غ��� ������ ���ٸ� ����
        {

            Vector3 diff = playerPosition - _slime.transform.position;
            diff = diff / diff.sqrMagnitude;
            _slime._nav.isStopped = true;
            _slime.GetComponent<Rigidbody>().
            AddForce((_slime.transform.position - new Vector3(diff.x, diff.y, 0f)) * 50f * pushBack);

        }
        catch (MissingReferenceException e)// ������ �ִٸ� �����޼��� ���
        {
            Debug.Log(e.ToString());
        }
        //����ó����
    }
}
