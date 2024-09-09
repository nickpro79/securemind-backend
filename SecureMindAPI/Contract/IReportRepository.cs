using SecureMindAPI.DTOs;

namespace SecureMindAPI.Contract
{
    public interface IReportRepository
    {
        public Task<bool> AddReport(ReportDTO report);
        public Task<IEnumerable<ReportDTO>> GetAllReports();
    }
}
