using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BCoreMvc.Models.TagHelpers
{
    /// <summary>
    /// Replace text (for example #xxx to <a...>#xxx</a>)
    /// </summary>
    [HtmlTargetElement("text", Attributes = HashingTextAttributeName)]
    public class HashTagHelper : TagHelper
    {
        private const string HashingTextAttributeName = "hashing-text";

        [HtmlAttributeName(HashingTextAttributeName)]
        public string HashingText { get; set; }

        /// <summary>
        /// Append <а> to text
        /// </summary>
        /// <param name="context">Current context</param>
        /// <param name="output">Output replasing text</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.AppendHtml(HashTag.ReplaceHashTagsToLinks(HashingText));
        }
    }
}
