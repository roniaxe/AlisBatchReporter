using System.Data;

namespace AlisBatchReporter.DALs
{
    public interface IGenericQueryDao
    {
        DataTable GetData(string selectCommand);

        DataTable DoQuery();        
    }
}