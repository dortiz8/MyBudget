using System;

namespace MyBudget.Models;

public class CurrentBillDto
{
    public int BillsId { get; set; }
    public int UserId { get; set; }
    public int BillId { get; set; }
    public bool Paid { get; set; }
}
