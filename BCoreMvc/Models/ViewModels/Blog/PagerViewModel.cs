using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCoreMvc.Models.ViewModels.Blog
{
    public class PagerViewModel
    {
        public static int ItemsOnPage { get { return 2; } }
        public int ItemsCount { set; get; }        
        public int Page { set; get; }        

        public PagerViewModel()
        {
         
        }

        public PagerViewModel(int itemsCount, int page)
        {         
            ItemsCount = itemsCount;
            Page = page;
        }

        public bool IsLastPage()
        {
            return PageCount() <= Page;
        }
        public int PageCount()
        {
            return (int)Math.Ceiling((decimal)ItemsCount / (decimal)Page);
        }

        public void NextPage()
        {
            if (!IsLastPage())
                Page++;
        }
    }
}
