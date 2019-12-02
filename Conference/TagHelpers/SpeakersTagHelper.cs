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
    public class SpeakersTagHelper : TagHelper
    {
        private readonly ISpeakerService speakerServices;
        private const string ForAttributeName = "asp-for";
        public SpeakersTagHelper(ISpeakerService speakerServices)
        {
            this.speakerServices = speakerServices;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var allSpeakers = speakerServices.GetAllSpeakers();
            output.TagName = "select";
            output.Attributes.Add("class", "form-group");
            foreach (var speaker in allSpeakers)
            {
                TagBuilder myOption = new TagBuilder("option")
                {
                    TagRenderMode = TagRenderMode.Normal
                };
                myOption.Attributes.Add("value", speaker.FullName);
                myOption.InnerHtml.Append(speaker.FullName);
                output.Content.AppendHtml(myOption);
            }
        }
    }
}
