namespace CodeBase.Hero.Interfaces
{
    public interface IHeroAnimator
    {
        void Run(bool isRun);
        void Attack();
        void Reload();
        // void Damage();
        void Weapon(int weapon);
        void Die();
        void Interact();
    }
}