namespace EffectiveFeatureFlags.Services
{
    public interface IFlagService
    {
         bool IsEnabled(string flagName);
    }
}