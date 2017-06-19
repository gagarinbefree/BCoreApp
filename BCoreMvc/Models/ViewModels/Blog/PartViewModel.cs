using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCoreMvc.Models.ViewModels.Blog
{
    public class PartViewModel
    {
        public Guid Id { set; get; }
        public string Value { set; get; }
        public int PartType { set; get; }
        public DateTime CreatedOn { set; get; }
        public bool IsStrong { set; get; }

        public PartViewModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
