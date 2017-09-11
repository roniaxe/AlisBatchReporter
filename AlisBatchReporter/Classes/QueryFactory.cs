namespace AlisBatchReporter.Classes
{
    internal class QueryFactory
    {
        public static QueryRepo GetQuery(int typeCode)
        {
            switch (typeCode)
            {
                case 1:
                    return QueryRepo.ErrorReport;
                case 2:
                    return QueryRepo.TaskList;
                case 3:
                    return QueryRepo.ProcessingRateReport;
                default:
                    return QueryRepo.ErrorReport;
            }
        }
    }
}