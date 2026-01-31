namespace _Project.Develop.Runtime.DataManagment.Serializers
{
    public interface IDataSerializer
    {
        string Serialize<TData>(TData data);
        TData Deserialize<TData>(string serializedData);
    }
}