using System;

namespace KomeijiRai.ContingencyProtocol.Controllers
{
    public interface IPool<T>
    {
        T Get(Type type);
        void Collect(T obj);
    }
}
