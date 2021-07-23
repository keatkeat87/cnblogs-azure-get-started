using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using AzureGetStarted.Entity;
using Microsoft.EntityFrameworkCore;

namespace AzureGetStarted.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _db;

        public IndexModel(
            ILogger<IndexModel> logger,
            ApplicationDbContext Db
        )
        {
            _logger = logger;
            _db = Db;
        }

        public async Task OnGetAsync()
        {
            var products = await _db.Products.ToListAsync();
            _db.Products.Add(new Product { Name = "Product1" });
            await _db.SaveChangesAsync();
        }
    }
}
