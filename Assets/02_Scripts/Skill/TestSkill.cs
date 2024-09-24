using SkillModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill : SkillBase
{
    public override SkillEnter Enter { get; set; } = new NoneSkillEnter();
    public override SkillStay Stay { get; set; } = new NoneSkillStay();
    public override SkillExit Exit { get; set; } = new NoneSkillExit();
    public override SkillPassive Passive { get; set; } = new NoneSkillPassive();


    // Start is called before the first frame update
}
