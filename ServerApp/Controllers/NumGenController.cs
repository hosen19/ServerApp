using ServerApp.Helper;
using ServerApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ServerApp.Controllers
{
    [RoutePrefix("api/NumGen")]
    public class NumGenController : ApiController
    {
        private string commonData;

        [Route("counter")]
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var model = new CounterModel();
            var inst = NumberCounter.Instance();
            model.counter1 = inst.counter1;
            model.counter2 = inst.counter2;
            model.counter3 = inst.counter3;

            return Ok(model);
        }

        [Route("Command")]
        [HttpPost]
        public async Task<IHttpActionResult> Command(CommandModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                model.inputSize = model.inputSize * 1024;

               var rand =  new CustomRandomGenerator(model);
                if (model.cType == 1)
                    rand.StartGenerate();
                else
                    rand.EndGenerate();

                CounterHub.BroadcastCountDataStatic(commonData);

                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex.InnerException);
            }
        }

        [Route("Stop")]
        [HttpPost]
        public async Task<IHttpActionResult> Stop(CommandModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var rand = new CustomRandomGenerator(model);
                 rand.EndGenerate();

                CounterHub.BroadcastCountDataStatic(commonData);

                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex.InnerException);
            }
        }

        [Route("Reports")]
        [HttpGet]
        public async Task<IHttpActionResult> GetReports()
        {
            try
            {
                var memContainer = MemoryStreamContainer.Instance();
                string numbers = memContainer.getNumbers();

                int totalNumbers = 0;
                int totalInt = 0;
                int totalDouble = 0;
                int totalAlph = 0;

                var model = new ReportModel();

                if (!string.IsNullOrEmpty(numbers))
                {
                    string[] numArr = numbers.Split('#');
                    if (numArr != null)
                    {
                        if (model.list == null)
                            model.list = new List<string>();

                        foreach (var ln in numArr)
                        {
                            string[] lnArr = ln.Split(',');
                            foreach (var vl in lnArr)
                            {
                                if (vl.Contains(" "))
                                    totalAlph += 1;
                                else if (vl.Contains('.'))
                                    totalDouble += 1;
                                else
                                    totalInt += 1;

                                totalNumbers += 1;
                                if (model.list.Count < 20)
                                    model.list.Add(vl.Trim());
                            }
                        }
                    }

                    model.totalNumbers = totalNumbers;
                    model.totalInt = ((totalInt * 100 )/ totalNumbers) ;
                    model.totalDouble = ((totalDouble * 100) / totalNumbers);
                    model.totalAlph = ((totalAlph * 100) / totalNumbers);
                }
                return Ok(model);
            }
            catch(Exception ex)
            {
                return InternalServerError();
            }
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               // dispose any servicess
            }
            base.Dispose(disposing);
        }

    }


}
