using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region Stat
    [Serializable]  // 메모리에서 들고 있는 것을 파일로 변환할 수 있다는 의미
    public class Stat
    {
        // Json에서 쓰고있는 순서 그대로 적어줘야 함
        public int level;
    }

    [Serializable]
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        // 변환하는 작업
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
