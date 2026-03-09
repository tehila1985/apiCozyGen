using Dto;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AiController : ControllerBase
    {
        private readonly IAiService _aiService;
        private readonly IProductService _productService;
        private readonly IStyleService _styleService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<AiController> _logger;

        public AiController(IAiService aiService, IProductService productService, IStyleService styleService, ICategoryService categoryService, ILogger<AiController> logger)
        {
            _aiService = aiService;
            _productService = productService;
            _styleService = styleService;
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> AnalyzeImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return BadRequest(new { message = "נא להעלות תמונה" });

            try
            {
                var productResult = await _productService.GetProducts(0, 9999, null, null, null, null, null);
                var styles = await _styleService.GetStyles();
                var categories = await _categoryService.GetCategories();

               
                var matchingIds = await _aiService.AnalyzeImageAsync(
                    image,
                    productResult.Products.OrderBy(x => Guid.NewGuid()),
                    styles,
                    categories);

                return Ok(new { productIds = matchingIds });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AI Analysis Error");
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}