
using MyBudget.Models;

public class BillService(IMyBudgetDataAccess db) : IBillService
{
    public async Task<bool> DeleteBill(int userId, int id)
    {
        return await db.DeleteBill(userId, id);
    }

    public async Task<Bill> GetBill(int userId, int id)
    {
        return await db.GetBill(userId, id);
    }
     public async Task<IEnumerable<Bill>> GetBills(int userId)
    {
        return await db.GetBills(userId);
    }

    public async Task<bool> UpdateBill(BillDto bill)
    {
        return await db.UpdateBill(bill);
    }

    public async Task<bool> AddBill(BillDto bill)
    {
        return await db.AddBill(bill);
    }

    public async Task<CurrentBillDto> GetCurrentBill(int userId, int id)
    {
        return await db.GetCurrentBill(userId, id);
    }

    public async Task<bool> UpdateCurrentBill(CurrentBillDto bill)
    {
        return await db.UpdateCurrentBill(bill);
    }
}