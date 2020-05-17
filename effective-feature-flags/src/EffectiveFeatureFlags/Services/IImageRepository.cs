namespace EffectiveFeatureFlags.Services
{
    public interface IImageRepository
    {
         string GetEmailHeader(string emailAddress, bool useNewBranding = false);
    }
}