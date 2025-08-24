using Microsoft.AspNetCore.Mvc;
using MVCExample1.Controllers;
using MVCExample1.Models;

public class PatientsController : Controller
{
    private readonly ApplicationDbContext _context;
    public PatientsController(ApplicationDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    public ApplicationDbContext Get_context()
    {
        return _context;
    }

    [HttpPost]
    public IActionResult Create(string Name, string Allergies, ApplicationDbContext _context)
    {
        Patient p = new Patient();
        p.Allergies = Allergies;
        p.Name = Name;
        object value = _context.patients.Add(p);
        _context.SaveChanges();
        return RedirectToAction("index");
    }
}