public class Bill {
    public int BillCycleId {get; set;}
    public int BillId {get; set;}
    public string Name {get; set;}
    public string Description {get; set;}
    public int Amount {get; set;}
    public int DueDay {get; set;}
    public int BillTypeId {get; set;}
    public bool Scheduled {get; set;}
    public bool Inactive 
    {get; set;}
    public int UserId {get; set;}

}