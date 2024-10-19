public class NpcController : Interectable
{
    public enum NpcType
    {
        None,
        DungeonNpc,
        QuestNpc,
        ShopNpc,
    }

    public NpcType _npcType = NpcType.None;

    public override void Dialogues()
    {
        base.Dialogues();
        switch (_npcType)
        {
            case NpcType.DungeonNpc:
                
                break;
            case NpcType.QuestNpc:

                break;
            case NpcType.ShopNpc:

                break;
        }
    }

}
