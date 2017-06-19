using BCoreMvc.Models.ViewModels.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCoreMvc.Models.Commands
{
    public class UpdateViewModel
    {
        public WhatsNewViewModel WhatsNew { set; get; }
        public PostViewModel PreviewPost { set; get; }
        public List<PostViewModel> RecentPosts { set; get; }
        public PagerViewModel Pager { set; get; }

        public UpdateViewModel()
        {
            WhatsNew = new WhatsNewViewModel();
            PreviewPost = new PostViewModel();
            Pager = new PagerViewModel();
        }    
    }
}
