using EffectiveFeatureFlags.Services;

namespace EffectiveFeatureFlags.BadExamples
{
    public class BadFlagService
    {
        private readonly IFlagService _flagService;
        private readonly IImageRepository _imageRepository;
        private readonly IEmailService _emailService;

        public int[] SortNumbers(int[] input, int size)
        {
            // Don't mix business logic and flag checking.
            // Is new-sorting-feature-1 at all descriptive of what it does?
            if (_flagService.IsEnabled("new-sorting-feature-1"))
            {
                int tmp, min_key;
                for (int j = 0; j < size - 1; j++)
                {
                    min_key = j;

                    for (int k = j + 1; k < size; k++)
                    {
                        if (input[k] < input[min_key])
                        {
                            min_key = k;
                        }
                    }

                    tmp = input[min_key];
                    input[min_key] = input[j];
                    input[j] = tmp;
                }
            }
            else
            {
                int i, j;
                for (i = 1; i < size; i++)
                {
                    int item = input[i];
                    int ins = 0;
                    for (j = i - 1; j >= 0 && ins != 1;)
                    {
                        if (item < input[j])
                        {
                            input[j + 1] = input[j];
                            j--;
                            input[j + 1] = item;
                        }
                        else ins = 1;
                    }
                }
            }
            return input;
        }

        public void SendNotification(string toEmailAddress)
        {
            bool useNewBranding = _flagService.IsEnabled("use-new-branding");

            var subject = useNewBranding ?
                "Welcome to Citrix Workspace"
                : "Welcome to ShareFile";

            var image = _imageRepository.GetEmailHeader(toEmailAddress, useNewBranding);

            _emailService.SendTemplate(toEmailAddress, subject, image);
        }
    }
}