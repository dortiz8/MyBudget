using MyBudget.Models;

public interface IMyBudgetDataAccess
{
     Task<Bill> GetBill(int userId, int id);
     Task<CurrentBillDto> GetCurrentBill(int userId, int id);
     Task<IEnumerable<Bill>> GetBills(int userId);

     Task<bool> UpdateBill(BillDto bill);

     Task<bool> DeleteBill(int userId, int id);

     Task<bool> AddBill(BillDto bill);

     Task<bool> UpdateCurrentBill(CurrentBillDto bill);
}