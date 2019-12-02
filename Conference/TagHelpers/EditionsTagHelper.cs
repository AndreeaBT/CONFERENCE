using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conference.Service;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Conference.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("tag-name")]
    public class EditionsTagHelper : TagHelper
    {
        private readonly IEditionService editionsServices;
        private const string ForAttributeName = "asp-for";
        public EditionsTagHelper(IEditionService editionsServices)
        {
            this.editionsServices = editionsServices;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var allEditions = editionsServices.GetAllEditions();
            output.TagName = "select";
            output.Attributes.Add("class", "form-group");
            foreach (var edition in allEditions)
            {
                TagBuilder myOption = new TagBuilder("option")
                {
                    TagRenderMode = TagRenderMode.Normal
                };
                myOption.Attributes.Add("value", edition.Name);
                myOption.InnerHtml.Append(edition.Name);
                output.Content.AppendHtml(myOption);
            }
        }
    }
}
