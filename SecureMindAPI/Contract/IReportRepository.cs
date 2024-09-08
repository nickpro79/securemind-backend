using SecureMindAPI.DTOs;

namespace SecureMindAPI.Contract
{
    public interface IReportRepository
    {
        public Task<bool> AddRepository(ReportDTO report);
        public Task<IEnumerable<ReportDTO>> GetAllReports();
    }
}
