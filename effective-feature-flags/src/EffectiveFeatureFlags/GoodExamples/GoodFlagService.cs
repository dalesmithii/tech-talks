using EffectiveFeatureFlags.Services;

namespace EffectiveFeatureFlags.GoodExamples
{
    public class GoodFlagService
    {
        public interface INumberSorter
        {
            int[] SortNumbers(int[] input, int size);
        }

        public class SelectionNumberSorter : INumberSorter
        {
            public int[] SortNumbers(int[] input, int size)
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

                return input;
            }
        }

        public class InsertionNumberSorter : INumberSorter
        {
            public int[] SortNumbers(int[] input, int size)
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

                return input;
            }
        }

        public class NumberSorterFactory
        {
            private readonly IFlagService _flagService;
            private readonly INumberSorter _insertionSorter;
            private readonly INumberSorter _selectionSorter;

            public INumberSorter GetNumberSorter()
            {
                return _flagService.IsEnabled("replace-selectionnumbersort-with-insertionnumbersort")
                    ? _insertionSorter
                    : _selectionSorter;
            }
        }
    }
}