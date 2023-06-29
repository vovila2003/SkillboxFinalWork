using UnityEngine;

namespace CodeBase.Infrastructure.Factories.Interfaces
{
    public interface IHeroFactory
    {
        Transform Create(Vector3 at);
        void DestroyHero();
    }
}