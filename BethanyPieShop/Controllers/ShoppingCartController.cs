using System.Linq;
using BethanyPieShop.Models;
using BethanyPieShop.ViewModels;
using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BethanyPieShop.Controllers
{
    public class ShoppingCARTController : Controller
    {
        private readonly IPieRepository _pieRepository;

        private readonly ShoppingCart _shoppingCart;

        public ShoppingCARTController(IPieRepository pieRepository, ShoppingCart shoppingCart)
        {
            _pieRepository = pieRepository;
            _shoppingCart = shoppingCart;
        }
        // GET
        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            
            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int pieId)
        {
            var selectedPie = _pieRepository.AllPies.FirstOrDefault(p => p.PieId  == pieId);
            if(selectedPie != null)
                _shoppingCart.AddToCart(selectedPie,1);
            return RedirectToAction("Index");
        }
        
        public RedirectToActionResult RemoveFromShoppingCart(int pieId)
        {
            var selectedPie = _pieRepository.AllPies.FirstOrDefault(p => p.PieId  == pieId);
            if(selectedPie != null)
                _shoppingCart.RemoveFromCart(selectedPie);
            return RedirectToAction("Index");
        }
    }
}