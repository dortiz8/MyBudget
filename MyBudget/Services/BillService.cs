
public class BillService(IMyBudgetDataAccess db) : IBillService
{
    public async Task<bool> DeleteBill(int id)
    {
        return await db.DeleteBill(id);
    }

    public async Task<Bill> GetBill(int id)
    {
        return await db.GetBill(id);
    }
     public async Task<IEnumerable<Bill>> GetBills()
    {
        return await db.GetBills();
    }

    public async Task<bool> UpdateBill(BillDto bill)
    {
        return await db.UpdateBill(bill);
    }
}