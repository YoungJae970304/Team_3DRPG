using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkDamagedState : MonsterBaseState
{
    public OrkDamagedState(Ork ork) : base(ork)
    {
        _ork = ork;
        _player = _ork._player.GetComponent<Player>();
        _pStat = _player._playerStat;
    }
    PlayerStat _pStat;
    public override void OnStateEnter()
    {
        //�˹�, ������ �ޱ�
        //�ӽ÷� ������ �������� �־���� ���� �÷��̾� ������ �� �޾ƿ��� ����
        _ork._nav.enabled = false;
        _ork.GetComponent<Rigidbody>().isKinematic = false;
    }

    public override void OnStateExit()
    {
        //�ڷ�ƾ ���߱�?
        //���´� ���x�ٰ� ���߿� ������ ����ġ ��������Ʈ�� ���ϱ�
        _ork._nav.enabled = true;
        _ork.GetComponent<Rigidbody>().isKinematic = true;
    }

    public override void OnStateUpdate()
    {
        _ork.StartCoroutine(StartDamege(_pStat.ATK, _ork.transform.position, 0.5f, 0.5f)); //��ũ ������ �÷��̾� ���ݷ����� ��ü ����
    }
    public IEnumerator StartDamege(int damage, Vector3 playerPosition, float delay, float pushBack)//�˹�ó�� �߿�!
    {
        yield return new WaitForSeconds(delay);

        try//�̰� �����غ��� ������ ���ٸ� ����
        {

            Vector3 diff = playerPosition - _ork.transform.position;
            diff = diff / diff.sqrMagnitude;
            _ork._nav.isStopped = true;
            _ork.GetComponent<Rigidbody>().
            AddForce((_ork.transform.position - new Vector3(diff.x, diff.y, 0f)) * 50f * pushBack);

        }
        catch (MissingReferenceException e)// ������ �ִٸ� �����޼��� ���
        {
            Debug.Log(e.ToString());
        }
        //����ó����
    }
}
