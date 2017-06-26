using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BCoreMvc.Models
{
    public static class HashTag
    {
        public static string HashTagPrefix { private set; get; }

        private static string _pattern;

        static HashTag()
        {
            HashTagPrefix = "#";
            _pattern = String.Format(@"({0})((?:[а-яА-ЯёЁa-zA-Z0-9-_]*))", HashTagPrefix);
        }

        public static List<string> GetHashTags(string text)
        {
            List<string> res = new List<string>();

            if (String.IsNullOrWhiteSpace(text))
                return res;

            var matches = new Regex(_pattern).Matches(text);
            foreach (Match m in matches)
            {
                var normalize = m.Value.Replace(HashTagPrefix, "").ToUpper();

                if (res.FirstOrDefault(f => f.ToUpper() == normalize) == null)
                    res.Add(normalize);
            }

            return res;
        }

        public static string ReplaceHashTagsToLinks(string clearText)
        {
            return Regex.Replace(clearText, _pattern, new MatchEvaluator(_link));
        }

        private static string _link(Match m)
        {
            string link = String.Format("/Feed/Search?tag={0}", m.Value.Replace("#", "").ToUpper());

            return String.Format("<a href=\"{1}\">{0}</a>", m, link);
        }
    }
}
