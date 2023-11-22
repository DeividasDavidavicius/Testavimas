namespace Models
{
    public interface IJsonConvertFacade
    {
        public T? Deserialize<T>(string stringToDeserialize);
        public string Serialize(object objectToSerialize);
    }
}