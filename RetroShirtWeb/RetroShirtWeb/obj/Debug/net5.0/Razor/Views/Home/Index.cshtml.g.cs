#pragma checksum "D:\C# Projects\RetroShirtWeb\RetroShirtWeb\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d18c5d2b678774a942eb07a6d313f9a2313a2ce1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\C# Projects\RetroShirtWeb\RetroShirtWeb\Views\_ViewImports.cshtml"
using RetroShirtWeb;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\C# Projects\RetroShirtWeb\RetroShirtWeb\Views\_ViewImports.cshtml"
using RetroShirtWeb.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\C# Projects\RetroShirtWeb\RetroShirtWeb\Views\_ViewImports.cshtml"
using RetroShirtEntities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\C# Projects\RetroShirtWeb\RetroShirtWeb\Views\_ViewImports.cshtml"
using RetroShirtDAL;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\C# Projects\RetroShirtWeb\RetroShirtWeb\Views\_ViewImports.cshtml"
using RetroShirtDtos;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\C# Projects\RetroShirtWeb\RetroShirtWeb\Views\_ViewImports.cshtml"
using RetroShirtDtos.Responses;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\C# Projects\RetroShirtWeb\RetroShirtWeb\Views\_ViewImports.cshtml"
using RetroShirtDtos.Requests;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d18c5d2b678774a942eb07a6d313f9a2313a2ce1", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ff8c96f2d9bf7c3c00b1a73aa066a0816c0c1db7", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<ProductListResponse>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_ProductCard", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\C# Projects\RetroShirtWeb\RetroShirtWeb\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class="" forjumbo jumbotron jumbotron-fluid "" style=""background-image: url(http://beingcovers.com/download.php?id=3551); background-size: 100%;"">

  <div class=""container"">
      <div class=""alignjumbo"">
          <h1 class=""display-4"">Retro Football Shop</h1>
          <p class=""lead text-muted"">Here is the heart of old-school sports stuff.</p>
      </div>
    
  </div>
");
#nullable restore
#line 16 "D:\C# Projects\RetroShirtWeb\RetroShirtWeb\Views\Home\Index.cshtml"
     if (ViewBag.visits != null)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("         <div>\r\n  <div class=\"alert alert-muted p-1 my-2 my-lg-0 mr-3 ml-2\" style=\"opacity:0.8;\" role=\"alert\">\r\n        <p class=\"m-0 p-0 text-muted \" style=\"opacity:0.8;\">Page visits: ");
#nullable restore
#line 20 "D:\C# Projects\RetroShirtWeb\RetroShirtWeb\Views\Home\Index.cshtml"
                                                                    Write(ViewBag.visits);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>    \r\n    </div>\r\n</div>\r\n");
#nullable restore
#line 23 "D:\C# Projects\RetroShirtWeb\RetroShirtWeb\Views\Home\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n  </div>\r\n\r\n<div class=\"row\">\r\n\r\n");
#nullable restore
#line 29 "D:\C# Projects\RetroShirtWeb\RetroShirtWeb\Views\Home\Index.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"col-3\">\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "d18c5d2b678774a942eb07a6d313f9a2313a2ce16145", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 32 "D:\C# Projects\RetroShirtWeb\RetroShirtWeb\Views\Home\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = item;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n            </div>\n");
#nullable restore
#line 34 "D:\C# Projects\RetroShirtWeb\RetroShirtWeb\Views\Home\Index.cshtml"
        }    

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@" 

<script>
    $(document).ready(function () {
        $('.shopCart').on('click', function () {
            let id = $(this).data('id');
            $.ajax({
                url: '/Cart/Add/' + id,
                type:'GET',
                dataType: 'json',               
                success: function (data) {
                    alertify.success(data);
                }
            });
        });
    });
</script>

");
            }
            );
            WriteLiteral("\r\n\r\n\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<ProductListResponse>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
