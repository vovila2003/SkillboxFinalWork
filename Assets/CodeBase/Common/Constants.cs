namespace CodeBase.Common
{
    public static class Constants
    {
        public const float Threshold = 0.001f;
        public const int GunShotDeltaTimeMs = 133;
        public const string DefaultLayerName = "Default";
        public const string EnemyLayerName = "Enemy";
        public const string HeroLayerName = "Player";
        public const string EnemyTag = "Enemy";
        public const string HeroTag = "Player";
        public const int PistolReloadTimeMs = 2500;
        public const int GunReloadTimeMs = 3500;
        public const float SwitchDelay = 0.5f;
        public const int KnifeAttackDelayMs = 267;
        public const int ChangeLevelDelayMs = 500;
        public const float KnifeHeight = 0.82f;
        public const float StartPointXOffset = 0.6f;
        public const float DirectionXOffset = 0.5f;
        public const int HeroDieDelayMs = 3000;
        public const string EventBulletsHit = "event:/Bullets/Hit";
        public const string EventKnifeHit = "event:/KnifeAndWrench/KnifeAttack";
        public const string EventKnifeMiss = "event:/KnifeAndWrench/Miss";
        public const string EventWrenchHit = "event:/KnifeAndWrench/WrenchAttack";
        public const float ReloadDelay = 0.4f;
        public const string FirstLevel = "FirstLevelScene";
        // public const string FirstLevel = "TestScene";
        public const int MinPriority = 0;
        public const int WaitPriority = 1;
        public const int PursePriority = 2;
        public const int AttackPriority = 3;

    }
}