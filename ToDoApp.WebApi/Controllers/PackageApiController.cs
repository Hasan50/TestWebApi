using ToDoApp.Business;
using ToDoApp.Business.Interfaces;
using ToDoApp.Business.Models;
using System.Web.Http;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ToDoApp.WebApi.Controllers
{
    [JwtAuthentication]
    public class PackageApiController : ApiController
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IPeriodTypeRepository _periodTypeRepository;
        private readonly IDayShiftRepository _dayShiftRepository;
        public PackageApiController()
        {
            _packageRepository = FtUnityMapper.GetInstance<IPackageRepository>();
            _periodTypeRepository = FtUnityMapper.GetInstance<IPeriodTypeRepository>();
            _dayShiftRepository = FtUnityMapper.GetInstance<IDayShiftRepository>();
        }

        [HttpGet]
        public IHttpActionResult GetPackageList()
        {
            var data = _packageRepository.GetPackageList(10).OrderBy(r => r.PackageCode).ToList();
            return Ok(data);
        }
        [HttpGet]
        public IHttpActionResult GetDuePackageList(string userId)
        {
            var data = _packageRepository.GetDuePackageList(userId).ToList();
            return Ok(data);
        }
        [HttpGet]
        public IHttpActionResult GetPackageCboList()
        {
            var data = _packageRepository.GetPackageCboList().ToList();
            return Ok(data);
        }
        [HttpGet]
        public IHttpActionResult GetPackageWithTergetCountList()
        {
            var data = _packageRepository.GetPackageWithProductionCountList(DateTime.Now).ToList();
            return Ok(data);
        }
        [HttpGet]
        public IHttpActionResult GetPackageWithTergetCountAndAssignList(string userId,DateTime date)
        {
            var packageProductionCountList = _packageRepository.GetPackageWithProductionCountList(date).ToList();
            var packageTergetCountList = _packageRepository.GetPackageWithTergetCountList(date).ToList();
            var assignList = _packageRepository.GetDailyUserPackageDeliveriesWithUserId(userId,date);
            List<object> datalist = new List<object>();
            foreach (var item in packageProductionCountList)
            {
               var pTerget = packageTergetCountList.Where(r => r.Id == item.Id).FirstOrDefault();
                var assignP = assignList.Where(r => r.PackageId == item.Id && r.UserCrediantialId == userId).FirstOrDefault();
                var ob = new
                {
                    item.Id,
                    item.Name,
                    TotalTergetCount=item.TotalTergetCount-(pTerget.TotalTergetCount != null?pTerget.TotalTergetCount:0),
                    Count= assignP!=null?assignP.PackageTergetCount:0,
                    Selected= assignP != null ? true : false
                };
                datalist.Add(ob);
            }
            return Ok(datalist);
        }
        [HttpGet]
        public IHttpActionResult GetDayShiftCboList()
        {
            var data = _dayShiftRepository.GetDayShiftCboList().ToList();
            return Ok(data);
        }
        [HttpPost]
        public IHttpActionResult Save([FromBody] Package model)
        {
            return Ok(_packageRepository.SavePackage(model));
        }
        [HttpPost]
        public IHttpActionResult Delete([FromBody] Package model)
        {
            return Ok(_packageRepository.DeletePackage(model));
        }
        [HttpPost]
        public IHttpActionResult SavePackageAssign([FromBody] List<DailyUserPackageDeliveryViewModel> model)
        {
            //var httpRequest = HttpContext.Current.Request;
            //var packageAssignList = httpRequest.Params["packageAssignList"];
            //var format = "dd/MM/yyyy"; // your datetime format
            //var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };
            //var packageAssignObList = JsonConvert.DeserializeObject<List<DailyUserPackageDelivery>>(packageAssignList,dateTimeConverter);
            return Ok(_packageRepository.SavePackageAssign(model));
        }
        [HttpPost]
        public IHttpActionResult UpdatePackageDeliveryTerget(PackageDeliveryTergetViewModel model)
        {
            return Ok(_packageRepository.UpdatePackageDeliveryTerget(model));
        }
        [HttpPost]
        public IHttpActionResult UpdatePackageDelivery(PackageDeliveryTergetViewModel model)
        {
            return Ok(_packageRepository.UpdatePackageDelivery(model));
        }
        [HttpGet]
        public IHttpActionResult GetPackageWithAdvanceList()
        {
            var data = _packageRepository.GetPackageList(10).ToList();
            List<object> datalist = new List<object>();
            foreach (var item in data)
            {
                var ob = new
                {
                    item,
                    AdvanceList = _packageRepository.GetPackageWithAdvanceList(item.Id.ToString())
                };
                datalist.Add(ob);
            }
            return Ok(datalist);
        }
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetPackageWithPeriodTypeList()
        {
            var dayShiftList = _dayShiftRepository.GetDayShiftList();
            List<object> oblit = new List<object>();
            foreach (var item in dayShiftList)
            {
                var ob = new
                {
                    ObjctModel=new UserWithPackageSaveViewModel {Id=null,DayShiftActive=false,Amount=null,DayShiftId=null,PackageCount=null,PackageId=null,PackageStartDate=null,PeriodTypeId=null,UserCrediantialId=null },
                    DayShift = item,
                    Package = _dayShiftRepository.GetDayShiftPackageList(10, item.Id),
                    PeriodType= _periodTypeRepository.GetPeriodTypeList(10).ToList()
            };
                oblit.Add(ob);
            }
            return Ok(new { dayShiftPackage=oblit });
        }
        [HttpGet]
        public IHttpActionResult GetPackageWithPeriodTypeListWithUsrId(string userId)
        {
            var dayShiftList = _dayShiftRepository.GetDayShiftList();
            List<UserWithPackage> userWithPackages = _packageRepository.GetUserWithPackageList(userId).ToList();
            List<object> oblit = new List<object>();
            foreach (var item in dayShiftList)
            {
                var shiftPackage = userWithPackages.Where(r => r.DayShiftId == item.Id).FirstOrDefault();
                var ObjctModel = new UserWithPackageSaveViewModel { Id = null, DayShiftActive = false, Amount = null, DayShiftId = null, PackageCount = null, PackageId = null, PackageStartDate = null,PackageStartDateView=null, PeriodTypeId = null, UserCrediantialId = null };
                if (shiftPackage !=null)
                {
                    ObjctModel.Id = shiftPackage.Id;
                    ObjctModel.DayShiftActive = true;
                    ObjctModel.Amount = shiftPackage.Amount;
                    ObjctModel.DayShiftId = shiftPackage.DayShiftId;
                    ObjctModel.PackageCount = shiftPackage.PackageCount;
                    ObjctModel.PackageId = shiftPackage.PackageId;
                    ObjctModel.PackageStartDate = shiftPackage.PackageStartDate;
                    ObjctModel.PackageStartDateView = shiftPackage.PackageStartDate;
                    ObjctModel.PeriodTypeId = shiftPackage.PeriodTypeId;
                    ObjctModel.UserCrediantialId = shiftPackage.UserCrediantialId;
                }
                var ob = new
                {
                    ObjctModel,
                    DayShift = item,
                    Package = _dayShiftRepository.GetDayShiftPackageListWithUserId(10, item.Id,userId),
                    PeriodType = _periodTypeRepository.GetPeriodTypeList(10).ToList()
                };
                oblit.Add(ob);
            }
            return Ok(new { dayShiftPackage = oblit });
        }
        [HttpGet]
        public IHttpActionResult GetDailyUserPackageList(string userId, string selectddate)
        {
            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            //var days = (yearOfMonthLast- yearOfMonth).Days;
            var packageList = _packageRepository.GetUserPackageDetailList(userId).ToList();
            var userPackageList = packageList.Where(r => r.UserPackageId != null).ToList();
            var userPackageAssignList = packageList.Where(r => r.UserCrediantialId != null).ToList();


            var yearOfMonth = Convert.ToDateTime(selectddate); ///userPackageList.First().PackageStartDate;//new DateTime(2019, 11, 01);
            var yearOfMonthLast = userPackageList.First().PackageEndDate != null ? userPackageList.First().PackageEndDate : lastDayOfMonth;//new DateTime(2019, 12, 31);
            List<DateTime> dates = GetDatesBetween((DateTime)yearOfMonth, (DateTime)yearOfMonthLast);
            List<object> datalist = new List<object>();
            decimal UserDue = 0;
            decimal UserPaid = 0;
            for (int i = 0; i <= dates.Count; i++)
            {
                //var gdate = new DateTime(date.Year, date.Month, i);
                var properties = new Dictionary<string, object>();
                properties.Add("Id", i + 1);
                string packageName = "";
                var pList = new List<object>();
                foreach (var item in userPackageAssignList)
                {
                    bool adate = dates[i].ToShortDateString() == item.DeliveryAssignDate.Value.ToShortDateString();
                    if (adate)
                    {
                        packageName += packageName == "" ? item.PackageCode + "(" + item.PackageTergetCount + ")" : "," + item.PackageCode + "(" + item.PackageTergetCount + ")";
                        pList.Add(new { Text = item.Package, Value = item.Id, Count = item.PackageTergetCount, Selected = true });
                        UserDue += Convert.ToDecimal(item.DueAmmount);
                        UserPaid += Convert.ToDecimal(item.PaidAmmount);
                    }
                }
                properties.Add("Package", pList);
                properties.Add("PackageName", packageName);
                properties.Add("StartTime", dates[i]);
                properties.Add("EndTime", dates[i].AddMinutes(1));
                //foreach (var item in packageList)
                //{
                //    var a = item.UserPackageId != null ? 1 : 0;
                //properties.Add(item.Id.ToString(), a);
                //}
                datalist.Add(properties);
                if (dates.Count == datalist.Count) break;
            }
            return Ok(new { packageList, datalist, UserDue,UserPaid });
        }
        public List<DateTime> GetDatesBetween(DateTime startDate, DateTime endDate)
        {
            List<DateTime> allDates = new List<DateTime>();
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                allDates.Add(date);
            return allDates;
        }
        [HttpGet]
        public IHttpActionResult GetUserDueAndPaidList(string userId)
        {
            var data = _packageRepository.GetUserDueAndPaidList(userId).ToList();
            return Ok(data);
        }
        [HttpGet]
        public IHttpActionResult GetUserDueAndPaidWithDateList(string userId,DateTime date)
        {
            var data = _packageRepository.GetUserDueAndPaidWithDateList(userId,date).ToList();
            return Ok(data);
        }
    }
}
