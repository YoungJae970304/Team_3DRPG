using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region Stat
    [Serializable]  // �޸𸮿��� ��� �ִ� ���� ���Ϸ� ��ȯ�� �� �ִٴ� �ǹ�
    public class Stat
    {
        // Json���� �����ִ� ���� �״�� ������� ��
        public int level;
    }

    [Serializable]
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        // ��ȯ�ϴ� �۾�
        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();

            foreach (Stat stat in stats)
            {
                dict.Add(stat.level, stat);
            }

            return dict;
        }
    }
    #endregion
}
