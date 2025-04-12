using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


[ApiController]
[Route("api/{userId}/[controller]")]  // Route will be: /api/hello
public class BillsController(IBillService billService):ControllerBase
{
    [HttpGet()]
    public async Task<IActionResult> GetBillsAsync(int userId)
    {
        try
        {
            var bills = await billService.GetBills(userId);
            if (bills == null)
            {
                return NotFound();
            }
            return Ok(bills);
            
        }
        catch (System.Exception)
        {
            throw new System.Exception("An error occurred while trying to get bills");
        }
    }

     [HttpGet("{id}")]
    public async Task<IActionResult> GetBillAsync(int userId, int id)
    {
        try
        {
            var bill = await billService.GetBill(userId, id);
            if (bill == null)
            {
                return NotFound();
            }
            return Ok(bill);
        }
        catch (System.Exception)
        {
            throw new System.Exception("An error occurred while trying to get bills");
        }
       
    }
    [HttpPost] 
    public async Task<IActionResult> AddBillAsync([FromBody] BillDto bill)
    {
        try
        {
            var result = await billService.AddBill(bill);
            if (!result)
            {
                throw new System.Exception("An error occurred while trying to add bill");
            }
            return Ok(bill);
        }
        catch (System.Exception)
        {
            throw new System.Exception("An error occurred while trying to add bill");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBillAsync([FromBody] BillDto bill)
    {
        try
        {
            var billToUpdate = await billService.GetBill(bill.UserId, bill.BillId);
            if (billToUpdate == null)
            {
                return NotFound();
            }

            var result = await billService.UpdateBill(bill);

            if (!result)
            {
                throw new System.Exception("An error occurred while trying to update bill");
            }
            return Ok(bill);
        }
        catch (System.Exception)
        {
            throw new System.Exception("An error occurred while trying to update bill");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PayBillAsync(int userId, int id, [FromQuery(Name = "$pay")] bool pay = true)
    {
        try
        {
            var billToUpdate = await billService.GetCurrentBill(userId, id);
            if (billToUpdate == null)
            {
                return NotFound();
            }

            if(pay == true && billToUpdate.Paid) return Ok();

            if(pay == true && !billToUpdate.Paid) billToUpdate.Paid = true; 

            if(!pay && billToUpdate.Paid) billToUpdate.Paid = false; 

            var result = await billService.UpdateCurrentBill(billToUpdate);

            if (!result)
            {
                throw new System.Exception("An error occurred while trying to update bill");
            }
            return Ok();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw new System.Exception("An error occurred while trying to update bill");
        }
    }

     

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBillAsync(int userId, int id)
    {
        try
        {
            var bill = await billService.GetBill(userId, id);
            if (bill == null)
            {
                return NotFound();
            }

            var result = await billService.DeleteBill(userId, id);

            if (!result)
            {
                throw new System.Exception("An error occurred while trying to delete bill");
            }
            return Ok();
        }
        catch (System.Exception)
        {
            throw new System.Exception("An error occurred while trying to delete bill");
        }
    }
}