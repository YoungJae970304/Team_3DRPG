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

    public NpcController(NpcType npcType)
    {
        _npcType = npcType;
    }
}
