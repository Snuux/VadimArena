namespace _Project.Develop.Runtime.DataManagment.DataProviders
{
    public interface IDataReader<TData> where TData : ISaveData
    {
        void ReadFrom(TData data);
    }
}