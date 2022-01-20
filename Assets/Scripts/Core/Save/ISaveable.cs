using Newtonsoft.Json.Linq;

public interface ISaveable
{
    void PrepareSaveData();
    void LoadData(JToken jToken);
}
