using DefenderServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DLJ_UniversalApp.Areas.Danil.TagHelpers
{
    public class Sidemenu : TagHelper
    {
        IMenuBuilderService _menuBuilderService;
        IHttpContextAccessor _httpContextAccessor;
        IConfiguration _configuration;

        public Sidemenu(IMenuBuilderService menuBuilderService, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _menuBuilderService = menuBuilderService;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("id", "mySidenav");
            output.Attributes.SetAttribute("class", "sidenav");
            output.TagMode = TagMode.StartTagAndEndTag;
            var menuItemsResult = await _menuBuilderService.GetAvailableMunuItems();

            StringBuilder outputHtml = new StringBuilder();

            if (menuItemsResult.IsSuccess)
            {
                outputHtml.Append($"<a href='javascript: void(0)' class='closebtn' onclick='closeNav()'>&times;</a>");

                foreach (var item in menuItemsResult.GetData)
                {
                    var host = _httpContextAccessor.HttpContext.Request.Host.Value;
                    var scheme = _httpContextAccessor.HttpContext.Request.Scheme;
                    var betaImage = _configuration["Menu:BetaItemImage"];

                    var ifBeta = item.IsBeta ? $"<img class='beta_icon' src = '{scheme}://{host}/icons/{betaImage}' />" : string.Empty;
                    if (!string.IsNullOrEmpty(item.Link.Area))
                    {
                        item.Link.Area = item.Link.Area + "/";
                    }
                    var href = $"/{item.Link.Area}{item.Link.Controller}/{item.Link.Action}";
                    outputHtml.Append($"<a class='nav-link text-light' href='{href}'>{item.Name}  {ifBeta}</a>");
                }
            }

            output.Content.SetHtmlContent(outputHtml.ToString());
        }
    }
}
