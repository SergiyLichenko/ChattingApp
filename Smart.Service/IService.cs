namespace Smart.Service
{
    public interface IService<T>
    {
        T Get(string id);
        T Remove(T instance);
        T Update(T instance);
        //IdentityResult Add(T instance);
    }
}