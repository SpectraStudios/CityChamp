namespace SpectraStudios.CityChamp
{
    public interface IDamageable
    {
        int Health { get; set; }
        void TakeDamage(int damageAmount);
    }
}