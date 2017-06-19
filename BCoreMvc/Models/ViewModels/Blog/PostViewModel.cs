using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCoreMvc.Models.ViewModels.Blog
{
    public class PostViewModel
    {
        public Guid Id { set; get; }            
        public string UserId { set; get; }         
        public bool IsPreview { set; get; }        
        public DateTime CreatedOn { set; get; }
        public PostStatusLineViewModel StatusLine { set; get; }
        public List<PartViewModel> Parts { set; get; }
        public WhatsThinkViewModel Comment { set; get; }
        public List<CommentViewModel> Comments { set; get; }
        public List<HashViewModel> Hashes { set; get; }       
        public int? TopNumber { set; get; }
                       
        public PostViewModel()
        {            
            Parts = new List<PartViewModel>();
        }
    }
}
