using Backload.Contracts.Context;
using Backload.Contracts.FileHandler;
using Backload.Contracts.Status;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using Backload.Helper;

namespace BCoreMvc.Controllers
{
    public class FileHandlerController : Controller
    {        
        private IHostingEnvironment _hosting;
        
        public FileHandlerController(IHostingEnvironment hosting)
        {
            _hosting = hosting;
        }

        [HttpPost]
        public async Task<ActionResult> Upload()
        {
            try
            {
                IFileHandler handler = Backload.FileHandler.Create();

                handler.Events.IncomingRequestStarted += Events_IncomingRequestStarted;

                handler.Init(this.HttpContext, _hosting);
                IBackloadResult result = await handler.Execute();
                                
                return ResultCreator.Create(result);
            }
            catch
            {                
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        void Events_IncomingRequestStarted(IFileHandler sender, Backload.Contracts.Eventing.IIncomingRequestEventArgs e)
        {                                    
        }
    }
}
