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
        public const string CARTKEY = "cart";
        public ItemController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Item
        public async Task<IActionResult> Index()
        {
            return View(await _context.Item.ToListAsync());
        }
        [Route("food/{id}", Name = "food")]
        public IActionResult IsFoodAction(int id)
        {
            var isFood = _context.Item.Where(x => x.isFood == id).ToList();
            System.Console.WriteLine(isFood);
            if (isFood == null)
            {
                return NotFound("Không có sản phẩm");
            }


            return View(isFood);
        }



        // Hiện thị giỏ hàng
        [Route("/cart", Name = "cart")]
        public IActionResult Cart(DiscountModel discount)
        {

            ViewBag.Discount = new SelectList(_context.Discount.Where(x => x.Status == 1).ToList(), "DiscountId", "DiscountName");
            return View(GetCartItems());
        }

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
        public IActionResult UpdateCart([FromForm] int itemid, [FromForm] int quantity, [FromForm] int discountId, ItemModel items, OrderModel order)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems();

            var cartitem = cart.Find(p => p.Items.ItemId == itemid);



            if (cartitem != null)
            {
                cartitem.Quantity = quantity;
            }
            SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();


        }



        /// Cập nhật
        [Route("/updateorderall", Name = "updateorderall")]
        [HttpPost]
        public IActionResult UpdateOrderAll([FromForm] int quantity, [FromForm] int discountId, OrderModel order)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems();
            var orderitem = cart.Find(x => x.Items.Status == 1);

            foreach (var item in cart)
            {
                if (orderitem != null)
                {
                    if (discountId == 1)
                    {
                        orderitem.Items.DiscountPrice = 10;
                    }
                    else
                    if (discountId == 2)
                    {
                        orderitem.Items.DiscountPrice = 20;
                    }
                    else if (discountId == 3)
                    {
                        orderitem.Items.DiscountPrice = 5;

                    }
                    else if (discountId == 5)
                    {
                        orderitem.Items.DiscountPrice = 50;

                    }

                }
            }



            SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();


        }


        [Route("/createorder")]
        public IActionResult CreateOrder(OrderModel orders)
        {
            var isExistId = 1;
            var orderitem = GetCartItems();
            var isExist = _context.Order.Where(x => x.IdToGetListOrder == isExistId).FirstOrDefault();

            foreach (var orderItem in orderitem)
            {

                var addItem = new OrderModel
                {
                    IdToGetListOrder = isExistId,
                    CreateDate = DateTime.Now,
                    OrderItemId = orderItem.Items.ItemId,
                    Quantity = orderItem.Quantity

                };

                _context.Add(addItem);



            }

            _context.SaveChanges();
            ClearCart();
            return RedirectToAction(nameof(Index));
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
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemName,Description,Price,DiscountPrice,Status,isFood")] ItemModel item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Item/Edit/5
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

        // POST: Item/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,Description,Price,DiscountPrice,Status,isFood")] ItemModel item)
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

        // GET: Item/Delete/5
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

        // POST: Item/Delete/5
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
    }
}
