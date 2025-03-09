public interface IBillService{
    Task<Bill> GetBill(int id); 
    Task<IEnumerable<Bill>> GetBills();
    Task<bool> UpdateBill(BillDto bill);

    Task<bool> DeleteBill(int id);  
}