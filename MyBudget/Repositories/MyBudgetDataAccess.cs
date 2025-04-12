using System.Data;
using Dapper;
using MyBudget.Models;

public class MyBudgetDataAccess(IDbConnection dbConnection) : IMyBudgetDataAccess
{
    public async Task<Bill> GetBill(int userId, int id)
    {
        return await DbUtils.GetItem<Bill>(dbConnection, "GetBillById", new { UserId = userId, Id = id }); 
    }

    public async Task<IEnumerable<Bill>> GetBills(int userId)
    {
        return await DbUtils.GetListOfitem<Bill>(dbConnection, "GetCycleBills", new { UserId = userId });
    }

    public async Task<bool> UpdateBill(BillDto bill)
    {
        return await DbUtils.UpdateItemByType<BillDto>(dbConnection, "UpdateBill", bill, "@BillData", "dbo.BillType");
    }

    public async Task<bool> DeleteBill(int userId, int id)
    {
        return await DbUtils.DeleteItem(dbConnection, "DeleteBill", new { UserId = userId, Id = id });
    }

    public async Task<bool> AddBill(BillDto bill)
    {
        return await DbUtils.AddItemByType<BillDto>(dbConnection, "AddBill", bill, "@BillData", "dbo.BillType");
    }

    public async Task<CurrentBillDto> GetCurrentBill(int userId, int id)
    {
        return await DbUtils.GetItem<CurrentBillDto>(dbConnection, "GetCurrentBillById", new { UserId = userId, Id = id }); 
    }

    public Task<bool> UpdateCurrentBill(CurrentBillDto bill)
    {
        return DbUtils.UpdateItemByType<CurrentBillDto>(dbConnection, "UpdateCurrentBill", bill, "@BillData", "dbo.CurrentBillType");
    }
}