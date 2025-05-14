namespace eStoreCA.Shared.Interfaces;

public interface ISessionWrapper
{
    public T GetFromSession<T>(string key);
    public void SetInSession<T>(string key, T value);
    public void RemoveFromSession(string key);
}