using System;

namespace EvaLabs.Common.Models.Interfaces
{
    public interface IBase<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        string Name { get; set; }
    }
}