using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using AzureGetStarted.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AzureGetStarted.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public IndexModel(
            ILogger<IndexModel> logger,
            ApplicationDbContext Db,
            IConfiguration configuration
        )
        {
            _logger = logger;
            _db = Db;
            _configuration = configuration;
        }

        public List<Product> Products { get; set; } = new();

        public string SecretValue { get; set; } = "";

        public async Task OnGetAsync()
        {
            Products = await _db.Products.ToListAsync();
            _db.Products.Add(new Product { Name = "Product1" });
            await _db.SaveChangesAsync();
            SecretValue = _configuration["User:Password"];
        }
    }
}
