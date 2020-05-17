namespace EffectiveFeatureFlags.Services
{
    public interface IEmailService
    {
         void SendTemplate(string emailAddress, string subject, string headerImage);
    }
}