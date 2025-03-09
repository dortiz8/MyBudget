using System.Data;
using Dapper;

public class MyBudgetDataAccess(IDbConnection dbConnection) : IMyBudgetDataAccess
{
    public async Task<Bill> GetBill(int id)
    {
        return await DbUtils.GetItem<Bill>(dbConnection, "GetBillById", id); 
    }

    public async Task<IEnumerable<Bill>> GetBills()
    {
        return await DbUtils.GetListOfitem<Bill>(dbConnection, "GetCycleBills");
    }

    public async Task<bool> UpdateBill(BillDto bill)
    {
        return await DbUtils.UpdateItemByType<BillDto>(dbConnection, "UpdateBill", bill, "@BillData", "dbo.BillType");
    }

    public async Task<bool> DeleteBill(int id)
    {
        return await DbUtils.DeleteItem(dbConnection, "DeleteBill", id);
    }
}