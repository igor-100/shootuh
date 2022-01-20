public interface ISaveManager
{
    void AddToSaveRegistry<T>(T saveObject) where T : ISaveable;
    void Save();
    void Load();
}
