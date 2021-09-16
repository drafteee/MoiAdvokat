using LawyerService.Entities.Lawyer;
using LawyerService.ViewModel.Files;
using LawyerService.ViewModel.Lawyers;
using System.Threading.Tasks;

namespace LawyerService.BL.Interfaces.Lawyers
{
    public interface ILawyerManager : IBaseManager<Lawyer, LawyerVM>
    {
        Task<bool> UploadCertificate(AttachFileVM vm);
        Task<bool> CheckIfCertificateExists(LawyerVM vm);
        Task<bool> ConfirmLawyer(LawyerConfirmationVM vm);
    }
}
