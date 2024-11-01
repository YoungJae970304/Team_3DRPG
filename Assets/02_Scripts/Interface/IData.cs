using System.Linq;

public interface IData
{
    //기본 값으로 데이터를 초기화 시켜줌
    void SetDefaultData();
    //데이터 로드
    bool LoadData();
    //데이터 세이브
    void SaveData();
}
