using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCoreMvc.Models.ViewModels.Blog
{
    public class CommentViewModel
    {
        public Guid Id { set; get; }
        public Guid PostId { set; get; }
        public CommentStatusLineViewModel StatusLine { set; get; }       
        public string UserId { set; get; }
        public DateTime DateTime { set; get; }
        public string Text { set; get; }

        public CommentViewModel()
        {
            StatusLine = new CommentStatusLineViewModel();
        }
    }
}
