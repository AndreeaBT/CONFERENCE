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
    public class TalksTagHelper : TagHelper
    {
        private readonly ITalkService talkServices;
        private const string ForAttributeName = "asp-for";
        public TalksTagHelper(ITalkService talksServices)
        {
            this.talkServices = talksServices;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var allTalks = talkServices.GetAllTalks();
            output.TagName = "select";
            output.Attributes.Add("class", "form-group");
            foreach (var talk in allTalks)
            {
                TagBuilder myOption = new TagBuilder("option")
                {
                    TagRenderMode = TagRenderMode.Normal
                };
                myOption.Attributes.Add("value", talk.Name);
                myOption.InnerHtml.Append(talk.Name);
                output.Content.AppendHtml(myOption);
            }
        }
    }
}
