using System.Web.Mvc;
using WebTestApp.Models;

namespace WebTestApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomer _customer;
        private readonly IRepository _repository;

        public HomeController(ICustomer customer, IRepository repository)
        {
            _customer = customer;
            _repository = repository;
        }

        public ActionResult Index()
        {
            var id = Session.SessionID;

            ViewBag.Title = _customer.Name;
            ViewBag.GenValue = _repository.Get();

            return View();
        }
    }
}