
using Microsoft.AspNetCore.Mvc;
using ricanEventos.Helpers;
using ricanEventos.Models;
using ricanEventos.Repositories;

namespace ricanEventos.Controllers;

public class EventController : Controller
{


  UserRepository userRepository = new UserRepository();
  CategoryRepository categoryRepository = new CategoryRepository();
  EventRepository eventRepository = new EventRepository();

  public IActionResult Index()
  {

    return View();
  }

  public IActionResult Create()
  {
    return View();
  }

  [HttpPost]
  public AjaxResponse Save()
  {
    Event evento = new Event();
    Address address = new Address();
    Price price = new Price();
    User user = new User();

    user.Id = int.Parse(Request.Form["user_id"]);

    price.Value = decimal.Parse(Request.Form["price"]);

    evento.Title = Request.Form["title"];
    evento.Photo = Request.Form["img"];
    evento.Online = int.Parse(Request.Form["online"]);
    evento.Description = Request.Form["descricao"];
    evento.DateTime = DateTime.Parse(Request.Form["data_hora"]);

    evento.Prices.Add(price);
    evento.Link = Request.Form["link"];
    address.Cep = Request.Form["cep"];
    address.Logradouro = Request.Form["logradouro"];
    address.Bairro = Request.Form["bairro"];
    address.NrLocal = Request.Form["nr_local"];
    address.idCity = int.Parse(Request.Form["cidade"]);
    address.Complemento = Request.Form["complemento"];
    evento.Address = address;
    evento.User = user;
    var numbers = Request.Form["category_to_add"].ToString()
            .Split(';')
            .Where(x => int.TryParse(x, out _))
            .Select(int.Parse)
            .ToList();
    List<Category> categories = new List<Category>();

    foreach (var idCategory in numbers)
    {
      Category category = new Category();
      category.Id = idCategory;

      categories.Add(
       category
      );
    }

    evento.Categories = categories;

    var resp = eventRepository.Create(evento);
    return resp;
  }

  [HttpPost]
  public AjaxResponse Read(int? Id)
  {
    var resp = eventRepository.Read(userId: Id);
    return resp;
  }


  [HttpPost]
  public AjaxResponse ReadById(int? Id)
  {
    var resp = eventRepository.Read(idEvent: Id);
    return resp;
  }

  [HttpGet]
  public ActionResult Update(int id)
  {
    ViewBag.EventId = id;

    return View();
  }


  [HttpPost]
  public AjaxResponse saveUpdate()
  {
    Console.WriteLine("UPDATE");
    Event evento = new Event();
    Address address = new Address();
    Price price = new Price();
    User user = new User();

    user.Id = int.Parse(Request.Form["user_id"]);

    price.Value = decimal.Parse(Request.Form["price"]);
    evento.Id = int.Parse(Request.Form["id"]);
    evento.Title = Request.Form["title"];
    evento.Photo = Request.Form["img"];
    evento.Online = int.Parse(Request.Form["online"]);
    evento.Description = Request.Form["descricao"];
    evento.DateTime = DateTime.Parse(Request.Form["data_hora"]);

    evento.Prices.Add(price);
    evento.Link = Request.Form["link"];
    address.Cep = Request.Form["cep"];
    address.Logradouro = Request.Form["logradouro"];
    address.Bairro = Request.Form["bairro"];
    address.NrLocal = Request.Form["nr_local"];
    address.idCity = int.Parse(Request.Form["cidade"]);
    address.Complemento = Request.Form["complemento"];
    evento.Address = address;
    evento.User = user;
    var numbers = Request.Form["category_to_add"].ToString()
            .Split(';')
            .Where(x => int.TryParse(x, out _))
            .Select(int.Parse)
            .ToList();
    List<Category> categories = new List<Category>();

    foreach (var idCategory in numbers)
    {
      Category category = new Category();
      category.Id = idCategory;

      categories.Add(
       category
      );
    }

    evento.Categories = categories;
    var resp = eventRepository.Update(evento);

    return resp;
  }

  [HttpPost]
  public AjaxResponse Delete(int Id)
  {
    return eventRepository.Delete(Id);
  }
}