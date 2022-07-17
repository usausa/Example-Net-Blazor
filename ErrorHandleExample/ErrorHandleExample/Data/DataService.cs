namespace ErrorHandleExample.Data;

public class DataService
{
    public DataEntity? QueryData(int id)
    {
        if (id % 2 == 0)
        {
            return null;
        }

        return new DataEntity { Id = id, Name = $"Data-{id}", IsAdminOnly = id % 3 == 0 };
    }
}
