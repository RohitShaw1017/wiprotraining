using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorApp1.Data;
using RazorApp1.Models;

namespace RazorApp1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly RazorApp1.Data.ApplicationDbContext _context;

        public IndexModel(RazorApp1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Patient> Patient { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Patient = await _context.patients.ToListAsync();
        }
    }
}
