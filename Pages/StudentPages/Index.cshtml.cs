using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EfFuncCallSK.Data;
using EfFuncCallSK.Models;

namespace EfFuncCallSK.Pages_StudentPages
{
    public class IndexModel : PageModel
    {
        private readonly EfFuncCallSK.Data.ApplicationDbContext _context;

        public IndexModel(EfFuncCallSK.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Student = await _context.Students.ToListAsync();
        }
    }
}
