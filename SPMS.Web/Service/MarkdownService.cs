using MarkdownSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPMS.Web.Service
{
    public class MarkdownService : IMarkdownService
    {
        protected readonly Markdown _markdown = new Markdown();

        public string Render(string content)
        {
            return _markdown.Transform(content);
        }
    }
}
