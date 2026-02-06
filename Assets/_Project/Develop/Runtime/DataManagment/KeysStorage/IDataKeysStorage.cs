namespace _Project.Develop.Runtime.DataManagment.KeyStorage
{
    public interface IDataKeysStorage
    {
        string GetKeyFor<TData>() where TData : ISaveData;
    }
}