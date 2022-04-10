using System;
using UnityEngine;

namespace KomeijiRai.ContingencyProtocol.Controllers
{
    public interface IFactory<T>
    {
        T Create(Type type, Transform parent = null);
    }
}
