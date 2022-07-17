namespace ErrorHandleExample.Data;

public class DataEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public bool IsAdminOnly { get; set; }
}
