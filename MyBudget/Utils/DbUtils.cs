using System.Data;
using System.Reflection;
using Dapper;

public static class DbUtils{
    public static async Task<IEnumerable<T>> GetListOfitem<T>(IDbConnection dbConnection,  string storedProcedureName, object parameters = null) where T : class{

        DynamicParameters dynamicParameters = GetDynamicParameters(parameters);

        return await dbConnection.QueryAsync<T>(
                    storedProcedureName,
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure
                );
    }

    public static async Task<T> GetItem<T>(IDbConnection dbConnection, string storedProcedureName, object parameters = null) where T : class
    {
        DynamicParameters dynamicParameters = GetDynamicParameters(parameters);
        var item = await dbConnection.QueryFirstOrDefaultAsync<T>(
                        storedProcedureName,
                        dynamicParameters,
                        commandType: CommandType.StoredProcedure
                    );
        return item;
    }

    private static DynamicParameters GetDynamicParameters(object parameters, bool returnValue = false)
    {
        var dynamicParameters = new DynamicParameters();
        if (parameters != null)
        {
            Type paramType = parameters.GetType();
            PropertyInfo userIdProperty = paramType.GetProperty("UserId");
            PropertyInfo Id = paramType.GetProperty("Id");


            if (userIdProperty != null && Id != null)
            {
                var userIdValue = userIdProperty.GetValue(parameters);
                dynamicParameters.Add("@UserId", userIdValue, DbType.Int32, ParameterDirection.Input);
                var idValue = Id.GetValue(parameters);
                dynamicParameters.Add("@Id", idValue, DbType.Int32, ParameterDirection.Input);
            }

            if (returnValue)
            {
                dynamicParameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            }
        }

        return dynamicParameters;
    }

    public static async Task<bool> UpdateItemByType<T>(IDbConnection dbConnection, string storedProcedureName, T item, string inputTableName, string tableValueName )
    {
       var dataTable = ConvertToDataTable<T>(item);

        DynamicParameters dynamicParameters = SetDynamicParameters(dataTable, inputTableName, tableValueName );

        await dbConnection.ExecuteAsync(
                   storedProcedureName,
                   dynamicParameters,
                   commandType: CommandType.StoredProcedure
               );
        int result = dynamicParameters.Get<int>("ReturnValue");
        if (result == 0) Console.WriteLine("Update ran successfully but no lines were updated");
        return result == -1 ? false : true;

    }

    public static async Task<bool> DeleteItem(IDbConnection dbConnection, string storedProcedureName, object parameters = null)
    {
        DynamicParameters dynamicParameters = GetDynamicParameters(parameters, true);
        await dbConnection.ExecuteAsync(
                    storedProcedureName,
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure
                );
        int result = dynamicParameters.Get<int>("ReturnValue");
        if (result == 0) Console.WriteLine("Update ran successfully but no lines were updated");
        return result == -1 ? false : true;
    }

    public static async Task<bool> AddItemByType<T>(IDbConnection dbConnection, string storedProcedureName, T item, string inputTableName, string tableValueName)
    {
        var dataTable = ConvertToDataTable<T>(item);

        DynamicParameters dynamicParameters = SetDynamicParameters(dataTable, inputTableName, tableValueName);

        await dbConnection.ExecuteAsync(
                   storedProcedureName,
                   dynamicParameters,
                   commandType: CommandType.StoredProcedure
               );
        int result = dynamicParameters.Get<int>("ReturnValue");
        if (result == 0) Console.WriteLine("Update ran successfully but no lines were updated");
        return result == -1 ? false : true;
    }

    private static DynamicParameters SetDynamicParameters(DataTable dataTable, string inputTableName, string tableValueName)
    {
        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add(inputTableName, dataTable.AsTableValuedParameter(tableValueName));
        dynamicParameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
        return dynamicParameters;
    }

    private static DataTable ConvertToDataTable<T>(List<T> items)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);

        // Get properties of the type T
        PropertyInfo[] properties = typeof(T).GetProperties();

        // Create columns dynamically based on properties
        foreach (var prop in properties)
        {
            dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        }

        // Populate rows
        foreach (var item in items)
        {
            var values = new object[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                values[i] = properties[i].GetValue(item, null);
            }
            dataTable.Rows.Add(values);
        }

        return dataTable;
    }

    private static DataTable ConvertToDataTable<T>(T item)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);

        // Get properties of the type T
        PropertyInfo[] properties = typeof(T).GetProperties();

        // Create columns dynamically based on properties
        foreach (var prop in properties)
        {
            dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        }

        // Populate a single row
        var values = new object[properties.Length];
        for (int i = 0; i < properties.Length; i++)
        {
            values[i] = properties[i].GetValue(item, null);
        }
        dataTable.Rows.Add(values);

        return dataTable;
    }


}