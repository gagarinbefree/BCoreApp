using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCoreMvc.Models.ViewModels.Blog
{
    public class FeedViewModel
    {
        public PagerViewModel Pager { set; get; }
        public List<PostViewModel> RecentPosts { set; get; }

        public FeedViewModel()
        {
            Pager = new PagerViewModel();
        }
    }
}
