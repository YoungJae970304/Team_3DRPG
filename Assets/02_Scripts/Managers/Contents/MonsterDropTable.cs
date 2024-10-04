using UnityEngine;

public class MonsterDropTable
{

    const string DATA_PATH = "CSVData";

    const string MONSTER_DROP_DATA_TABLE = "Monster_Drop_Data_Table";

    void DropDataTable(string dataPath, string monsterDropTable)
    {
        var parsedDropdataTable = CSVReader.Read($"{dataPath}/{monsterDropTable}");
        foreach(var data in parsedDropdataTable)
        {
            ItemData itemData = null;
            //itemData =
        }
    }

}
