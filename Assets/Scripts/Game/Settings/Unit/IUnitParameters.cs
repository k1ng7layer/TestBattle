namespace Game.Settings.Unit
{
    public interface IUnitParameters
    {
        float StartHealth { get; }
        float MaxHealth { get; }
        float StartArmor { get; }
        float MaxArmor { get; }
        float StartAttackDamage { get; }
        float MaxAttackDamage { get; }
        float StartVampirism { get; }
        float MaxVampyrism { get; }
    }
}