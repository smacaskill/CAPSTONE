#pragma checksum "G:\Meu Drive\Assignment 5\FPSInventory\FPSInventory\Views\StoreInventory\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dfc1b21a334e914ed7ee60d1998e8a0fefe7b8f9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_StoreInventory_Index), @"mvc.1.0.view", @"/Views/StoreInventory/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/StoreInventory/Index.cshtml", typeof(AspNetCore.Views_StoreInventory_Index))]
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
#line 1 "G:\Meu Drive\Assignment 5\FPSInventory\FPSInventory\Views\_ViewImports.cshtml"
using FPSInventory;

#line default
#line hidden
#line 2 "G:\Meu Drive\Assignment 5\FPSInventory\FPSInventory\Views\_ViewImports.cshtml"
using FPSInventory.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dfc1b21a334e914ed7ee60d1998e8a0fefe7b8f9", @"/Views/StoreInventory/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"eaf30d88b4d62d70d07a6d80e4018cf51de1ea22", @"/Views/_ViewImports.cshtml")]
    public class Views_StoreInventory_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<FPSInventory.Models.OutItemOrder>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(53, 1, true);
            WriteLiteral("\n");
            EndContext();
#line 3 "G:\Meu Drive\Assignment 5\FPSInventory\FPSInventory\Views\StoreInventory\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
            BeginContext(92, 163, true);
            WriteLiteral("\n<h1>Index</h1>\n\n<table class=\"tableFPS\">\n    <thead>\n        <tr>\n            <th>\n                Description\n            </th>\n            <th>\n                ");
            EndContext();
            BeginContext(256, 44, false);
#line 16 "G:\Meu Drive\Assignment 5\FPSInventory\FPSInventory\Views\StoreInventory\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Quantity));

#line default
#line hidden
            EndContext();
            BeginContext(300, 52, true);
            WriteLiteral("\n            </th>\n            <th>\n                ");
            EndContext();
            BeginContext(353, 41, false);
#line 19 "G:\Meu Drive\Assignment 5\FPSInventory\FPSInventory\Views\StoreInventory\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Price));

#line default
#line hidden
            EndContext();
            BeginContext(394, 58, true);
            WriteLiteral("\n            </th>\n        </tr>\n    </thead>\n    <tbody>\n");
            EndContext();
#line 24 "G:\Meu Drive\Assignment 5\FPSInventory\FPSInventory\Views\StoreInventory\Index.cshtml"
 foreach (var item in Model)
{
    //if (item.Quantity > 0)
    //{

#line default
#line hidden
            BeginContext(520, 46, true);
            WriteLiteral("        <tr>\n            <td>\n                ");
            EndContext();
            BeginContext(567, 63, false);
#line 30 "G:\Meu Drive\Assignment 5\FPSInventory\FPSInventory\Views\StoreInventory\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.IdProductNavigation.Product1));

#line default
#line hidden
            EndContext();
            BeginContext(630, 52, true);
            WriteLiteral("\n            </td>\n            <td>\n                ");
            EndContext();
            BeginContext(683, 43, false);
#line 33 "G:\Meu Drive\Assignment 5\FPSInventory\FPSInventory\Views\StoreInventory\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Quantity));

#line default
#line hidden
            EndContext();
            BeginContext(726, 53, true);
            WriteLiteral("\n            </td>\n            <td>\n                $");
            EndContext();
            BeginContext(780, 40, false);
#line 36 "G:\Meu Drive\Assignment 5\FPSInventory\FPSInventory\Views\StoreInventory\Index.cshtml"
            Write(Html.DisplayFor(modelItem => item.Price));

#line default
#line hidden
            EndContext();
            BeginContext(820, 35, true);
            WriteLiteral("\n            </td>\n\n\n        </tr>\n");
            EndContext();
#line 41 "G:\Meu Drive\Assignment 5\FPSInventory\FPSInventory\Views\StoreInventory\Index.cshtml"
    //}
}

#line default
#line hidden
            BeginContext(865, 22, true);
            WriteLiteral("    </tbody>\n</table>\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<FPSInventory.Models.OutItemOrder>> Html { get; private set; }
    }
}
#pragma warning restore 1591
