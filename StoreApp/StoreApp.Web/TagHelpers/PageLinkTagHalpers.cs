using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StoreApp.Web.Models;


namespace StoreApp.Web.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHalpers : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        // Constructor
        public PageLinkTagHalpers(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        // ViewContext'yi almak için
        [ViewContext]
        public ViewContext? ViewContext { get; set; }

        // PageModel ve PageAction değerlerini alıyoruz
        public Pageinfo? PageModel { get; set; }
        public string PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // viewContext ve PageModel null değilse işlem yapılır
            if (ViewContext != null && PageModel != null)
            {
                IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

                // Div TagBuilder oluşturuyoruz
                TagBuilder div = new TagBuilder("div");
                TagBuilder ul = new TagBuilder("ul");
                ul.AddCssClass("pagination");

                // Sayfalama için linkler oluşturuluyor
                for (int i = 1; i <= PageModel.Totalpage; i++)
                {
                    TagBuilder li = new TagBuilder("li");
                    li.AddCssClass("page-item");


                    TagBuilder link = new TagBuilder("a");
                    link.AddCssClass("page-link");

                    if (i == PageModel.CurrentPage)
                    {
                        li.AddCssClass("active ");
                    }


                    // Link URL'si ayarlanıyor
                    link.Attributes["href"] = urlHelper.Action(PageAction, new { page = i });
                    link.AddCssClass("m-1");
                    // Sayfa numarasını link içinde gösteriyoruz
                    link.InnerHtml.Append(i.ToString());

                    // Linki div içerisine ekliyoruz
                    li.InnerHtml.AppendHtml(link);
                    ul.InnerHtml.AppendHtml(li);
                   
                }
              div.InnerHtml.AppendHtml(ul);
                // Div içeriğini output'a ekliyoruz
                output.Content.AppendHtml(div.InnerHtml);
            }
        }
    }
}
