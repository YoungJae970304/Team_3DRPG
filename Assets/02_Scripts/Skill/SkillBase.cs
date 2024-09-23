using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SkillBase//���� ���� ���
{
    public abstract SkillModule.SkillEnter Enter { get; set;} //��ų ������ ȿ��
    public abstract SkillModule.SkillStay Stay { get; set; } //��ų ������ ȿ��
    public abstract SkillModule.SkillExit Exit { get; set; } //��ų ����� ȿ��
    public abstract SkillModule.SkillPassive Passive { get; set; }//��ų ����� ����Ǵ� ȿ��

    public Define.SkillType skillType;  //��ų�� Ÿ��ex) Ű�ٿ�or�⺻��ų

    protected Sprite _icon;             //��ų ������

    public float delay;                 //��ų�� �ĵ�����
    public virtual void SkillEnter(Stat stat) {
        Enter.Enter(stat);
    }//��ų ������
    public virtual void SkillStay(Stat stat) {
        Stay.Stay(stat);
    }//��ų ��������

    public virtual void SkillExit(Stat stat) {
        Stay.End(stat);
        Exit.Exit(stat);
    }//��ų ���������

    public virtual void PassiveEffect(Stat stat) {
        Passive.Passive(stat);
    }//�нú� ȿ��

}

public class Stat { 

}

namespace SkillModule {//����
    public interface SkillEnter {
        public void Enter(Stat stat);
    }
    public interface SkillStay
    {
        public void Stay(Stat stat);
        public void End(Stat stat);

    }
    public interface SkillExit
    {
        public void Exit(Stat stat);
    }
    public interface SkillPassive
    {
        public void Passive(Stat stat);
    }


}