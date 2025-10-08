using _1121726Final.Data;
using _1121726Final.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;
using X.PagedList;

namespace _1121726Final.Controllers
{
    public class TicketController : Controller
    {
        private readonly CmsContext _context;

        public TicketController(CmsContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Purchase(int concertId)
        {
            
            var concert = await _context.TableConcerts1121726
                .Include(c => c.Group)
                .Include(c => c.Ticket)

                .FirstOrDefaultAsync(c => c.ConcertId == concertId);

            if (concert == null)
            {
                return NotFound("找不到該演唱會");
            }

            return View(concert);
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmPurchase(int concertId, string buyerName)
        {
            var concert = await _context.TableConcerts1121726
                .Include(c => c.Ticket)
                .FirstOrDefaultAsync(c => c.ConcertId == concertId);

            if (concert == null || concert.TotalSeats <= (concert.Ticket?.Count(t => t.IsPurchased) ?? 0))
            {
                return BadRequest("演唱會已售罄或不存在");
            }

            // 建立新的票務資訊
            var ticket = new Ticket
            {
                ConcertId = concertId,
                Owner = buyerName,
                IsPurchased = true,
                PurchaseDate = DateTime.Now
            };

            _context.TableTickets1121726.Add(ticket);
            await _context.SaveChangesAsync();

            return RedirectToAction("Success", new { ticketId = ticket.TicketId });
        }
        public async Task<IActionResult> Success(int ticketId)
        {
            var ticket = await _context.TableTickets1121726
                .Include(t => t.Concert)
                .FirstOrDefaultAsync(t => t.TicketId == ticketId);

            return View(ticket);
        }
        [HttpPost]
        public async Task<IActionResult> CancelPurchase(int ticketId)
        {
            var ticket = await _context.TableTickets1121726
                .Include(t => t.Concert)
                .FirstOrDefaultAsync(t => t.TicketId == ticketId);

            if (ticket == null || !ticket.IsPurchased)
            {
                return BadRequest("❌ 無法取消，票券不存在或未購買");
            }

            // 更新票券狀態為未購買，釋放座位
            ticket.IsPurchased = false;
            _context.TableTickets1121726.Update(ticket);
            await _context.SaveChangesAsync();

            return RedirectToAction("MyTickets", new { buyerName = ticket.Owner });
        }
        public async Task<IActionResult> MyTickets(string buyerName, int? page = 1)
        {
            const int pagesize = 3;
            var pagedTickets = GetPagedProcess(buyerName, page, pagesize);
            if (pagedTickets == null)
            {
                return NotFound(); 
            }

            ViewBag.usersModel = pagedTickets;
            ViewBag.buyerName = buyerName;

            return View(pagedTickets);
        }
        protected IPagedList<Ticket> GetPagedProcess(string buyerName ,int? page, int pagesize)
        {
            //過濾從cilent傳送過來有問題頁數
            if (page.HasValue && page < 1)
                return null;
            //從資料庫取得資料
            var listUnpaged = GetStuffFormDatabase(buyerName);
            IPagedList<Ticket> pagelist = listUnpaged.ToPagedList(page ?? 1, pagesize);
            //過濾從cilent傳送過來有問題頁數，包含判斷有問題的頁數邏輯
            if (pagelist.PageNumber != 1 && page.HasValue && page > pagelist.PageCount)
                return null;
            return pagelist;
        }
        protected IQueryable<Ticket> GetStuffFormDatabase(string buyerName)
        {
            return _context.TableTickets1121726
                .Include(t => t.Concert)
                .Where(t => t.Owner == buyerName);
        }
    }
}
