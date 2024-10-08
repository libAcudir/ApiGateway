using Common.Interfaces;

namespace Repository
{
    public class DataProcessor
    {
        private readonly IDataProcessingStrategy _strategy;

        public DataProcessor(IDataProcessingStrategy strategy)
        {
            _strategy = strategy;
        }
    }
}