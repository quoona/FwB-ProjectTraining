using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FwB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ItemModel = FwB.Models.Item;
using OrderModel = FwB.Models.Order;
using DiscountModel = FwB.Models.Discount;

namespace FwB.Item.Controllers
{
    public class ItemController : Controller
    {
        private readonly AppDbContext _context;
        // Key lưu chuỗi json của Cart
        public const string CARTKEY = "cart";
        public ItemController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {

            return View(await _context.Item.ToListAsync());
        }


        // Hiện thị giỏ hàng
        [Route("/cart", Name = "cart")]
        public IActionResult Cart(DiscountModel discount)
        {

            ViewBag.Discount = new SelectList(_context.Discount, "DiscountId", "DiscountName", "Content");
            return View(GetCartItems());
        }

        // [Route("/cart", Name = "cart")]
        // public IActionResult Cart()
        // {
        //     ViewBag.Discount = new SelectList(_context.Discount, "DiscountId", "DiscountName", "Content").Where()
        //     return View(GetCartItems());
        // }

        /// Thêm sản phẩm vào cart
        [Route("addcart/{itemid:int}", Name = "addcart")]
        public IActionResult AddToCart([FromRoute] int itemid)
        {

            var item = _context.Item
        .Where(p => p.ItemId == itemid)
        .FirstOrDefault();
            if (item == null)
                return NotFound("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Items.ItemId == itemid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.Quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new OrderModel() { Quantity = 1, Items = item });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            return RedirectToAction(nameof(Cart));
        }

        /// xóa item trong cart
        [Route("/removecart/{itemid:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] int itemid)
        {

            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Items.ItemId == itemid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));

        }

        /// Cập nhật
        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int itemid, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Items.ItemId == itemid);

            if (cartitem != null)
            {



                // Đã tồn tại, tăng thêm 1
                cartitem.Quantity = quantity;
            }
            SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();


        }


        [Route("/checkout")]
        public IActionResult CheckOut()
        {
            // Xử lý khi đặt hàng
            return View();
        }

        // GET: Item/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Item/Create
        public IActionResult Create()
        {
            ViewBag.MenuId = new SelectList(_context.Menu, "MenuId", "MenuName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemName,Price,Description, MenuId")] ItemModel item)
        {
            if (ModelState.IsValid)
            {
                ViewBag.MenuId = new SelectList(_context.Menu, "MenuId", "MenuName", item.MenuId);
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,Price,MenuId,Description")] ItemModel item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Item.FindAsync(id);
            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.ItemId == id);
        }

        // Lấy cart từ Session (danh sách CartItem)
        List<OrderModel> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<OrderModel>>(jsoncart);
            }
            return new List<OrderModel>();
        }
        // Xóa cart khỏi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }
        // Xóa cart khỏi session
        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession(List<OrderModel> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }
    }
}
