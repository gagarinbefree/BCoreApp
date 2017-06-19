using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCoreMvc.Models.ViewModels.Blog
{
    public class TopViewModel
    {
        public PagerViewModel Pager { set; get; }
        public List<PostViewModel> RecentPosts { set; get; }

        public TopViewModel()
        {
            Pager = new PagerViewModel();
        }
    }
}
