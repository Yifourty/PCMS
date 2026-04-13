using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PCMS.Application.Common.Dtos;
using PCMS.Application.Common.Interfaces;
using PCMS.Application.Common.Validators;
using PCMS.Application.Common.Extensions;
using PCMS.Domain.Entities;

namespace PCMS.Presentation.Controllers
{
    public class ProductController : Controller
    {
        [HttpPost("manual")]
        public async Task<IActionResult> CreateManual()
        {
            using var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();

            var dto = JsonSerializer.Deserialize<CreateProductDto>(body);

            if (dto is null || !ProductValidator.IsValid(dto))
                return BadRequest();

            return Ok(dto);
        }


        [HttpGet("{id}/custom")]
        public IActionResult GetCustom(IInMemoryProductRepository<Product> _repo,Guid id)
        {
            var product = _repo.GetById(id);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return new ContentResult
            {
                Content = JsonSerializer.Serialize(product, options),
                ContentType = "application/json"
            };
        }


        [HttpGet]
        public IActionResult Get(
            IInMemoryProductRepository<Product> _repo,
            [FromQuery] string? search,
            [FromQuery] Guid? categoryId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var products = _repo.GetAll()
                .FilterByCategory(categoryId)
                .SearchByName(search);

            var result = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            return Ok(result);
        }

        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
