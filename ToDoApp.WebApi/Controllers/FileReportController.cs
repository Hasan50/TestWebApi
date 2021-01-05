using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.Linq;
using FileTracker.Business.Interfaces;
using FileTracker.Business;

namespace FileTracker.WebApi.Controllers
{
    public class FileReportController : BaseReportController
    {
        private readonly IFileInformation _iFileInformation;
        public FileReportController()
        {
            _iFileInformation = FtUnityMapper.GetInstance<IFileInformation>();
        }
        public ActionResult PatientMoneyReceipt(int id)
        {
            var localReport = new LocalReport { ReportPath = Server.MapPath("~/Reports/PatientMoneyReceipt.rdlc") };
            var prescriptionModel = _iFileInformation.GetAll(id).ToList();
            var reportDataSource = new ReportDataSource("PrescriptionDataSet", prescriptionModel);
            localReport.DataSources.Add(reportDataSource);
            return PrintReportFormat(localReport, "50");
        }
    }
}
