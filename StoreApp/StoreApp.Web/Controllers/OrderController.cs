using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;
using StoreApp.Web.Models;

namespace StoreApp.Web.Controllers
{
    public class OrderController : Controller
    {
        Cart _cart;
        IOrderRepository _orderRepository;
        public OrderController(Cart cart, IOrderRepository orderRepository)
        {
            _cart = cart;
            _orderRepository = orderRepository;
        }

        public IActionResult CheckOut()
        {
            return View(new OrderModel()
            {
                Cart = _cart
            });
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut(OrderModel model)
        {

            if (_cart.Items.Count == 0)
            {
                ModelState.AddModelError("", "sepetinide ürün yok");
            }
            if (ModelState.IsValid)
            {
                Order order = new Order()
                {
                    Name = model.Name,
                    Email = model.Email,
                    City = model.City,
                    Phone = model.Phone,
                    AddressLine = model.AddressLine,
                    OrderTime = DateTime.Now,
                    orderItems = _cart.Items.Select(i => new StoreApp.Data.Concrete.OrderItem
                    {
                        ProductId = i.Product.Id,
                        Price = (int)i.Product.Price,
                        Quantity = i.Quantity

                    }).ToList()

                };
                model.Cart = _cart;

                var payment = await ProcessPayment(model);
                if (payment.Status == "success")
                {
                    _orderRepository.SaveOrder(order);
                    _cart.Clear();
                    return RedirectToPage("/Comploted", new { OrderId = order.Id });
                }
                else
                {
                    var message = payment.ErrorMessage;
                    ModelState.AddModelError("", message);
                }



                return View(model);

            }
            model.Cart = _cart;
            return View(model);

        }


        private async Task<Payment> ProcessPayment(OrderModel model)
        {

            Options options = new Options();
            options.ApiKey = "sandbox-rxV4cMfE3tmZZEcZvMQWOh2DTSfi0GHa";
            options.SecretKey = "sandbox-2ye1JbjT9WxLzV55BQawn3F6S2IcKsUo";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = new Random().Next(111111111,999999999).ToString();
            request.Price = model?.Cart?.CalculateTotal().ToString();
            request.PaidPrice =model?.Cart?.CalculateTotal().ToString();
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = model?.Name;
            paymentCard.CardNumber = model?.CartNumber;
            paymentCard.ExpireMonth = model?.expirationMonth;
            paymentCard.ExpireYear = model?.expirationYears;
            paymentCard.Cvc = model?.cvc;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = "Aslan";
            buyer.Surname = "Günel";
            buyer.GsmNumber = "+905350000000";
            buyer.Email = "email@email.com";
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Aslan Günel";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();

            foreach (var item in model?.Cart?.Items ?? Enumerable.Empty<CartItem>())
            {
                BasketItem firstBasketItem = new BasketItem();
               firstBasketItem.Id = item.Product.Id.ToString();
               firstBasketItem.Name = item.Product.Name;
               firstBasketItem.Category1 = "Telefon";
               firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
               firstBasketItem.Price = item.Product.Price.ToString();
               basketItems.Add(firstBasketItem);
            }
            

           
          
            request.BasketItems = basketItems;

            Payment payment =await Payment.Create(request, options);




            return payment;
        }
    }
}