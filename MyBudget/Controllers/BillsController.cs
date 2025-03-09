using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


[ApiController]
[Route("api/[controller]")]  // Route will be: /api/hello
public class BillsController(IBillService billService):ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetBillsAsync()
    {
        try
        {
            var bills = await billService.GetBills();
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
    public async Task<IActionResult> GetBillAsync(int id)
    {
        try
        {
            var bill = await billService.GetBill(id);
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
    public async Task<IActionResult> UpdateBillAsync([FromBody] BillDto bill)
    {
        try
        {
            var billToUpdate = await billService.GetBill(bill.BillId);
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBillAsync(int id)
    {
        try
        {
            var bill = await billService.GetBill(id);
            if (bill == null)
            {
                return NotFound();
            }

            var result = await billService.DeleteBill(id);

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