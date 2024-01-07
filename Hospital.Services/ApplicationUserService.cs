using Hospital.Models;
using Hospital.Repositories.Interfaces;
using Hospital.Utilities;
using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private IUnitOfWork _unitOfWork;

        public ApplicationUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PagedResult<ApplicationUserViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new ApplicationUserViewModel();
            int totalCount;
            List<ApplicationUserViewModel> vmList = new List<ApplicationUserViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                var modeList = _unitOfWork.GenericRepository<ApplicationUser>()
                                          .GetAll()
                                          .Skip(ExcludeRecords)
                                          .Take(pageSize)
                                          .ToList();

                totalCount = _unitOfWork.GenericRepository<ApplicationUser>()
                                        .GetAll()
                                        .ToList()
                                        .Count;

                vmList = ConvertModelToViewModelList(modeList);
            }
            catch (Exception ex)
            {
                throw;
            }

            return new PagedResult<ApplicationUserViewModel>
            {
                Data       = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize   = pageSize
            };

        }

        private List<ApplicationUserViewModel> ConvertModelToViewModelList(List<ApplicationUser> modeList)
        {
            return modeList.Select(x => new ApplicationUserViewModel(x)).ToList();
        }

        public PagedResult<ApplicationUserViewModel> GetAllDoctor(int pageNumber, int pageSize)
        {
            var vm = new ApplicationUserViewModel();
            int totalCount;
            List<ApplicationUserViewModel> vmList = new List<ApplicationUserViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                var modeList = _unitOfWork.GenericRepository<ApplicationUser>()
                                          .GetAll(x=>x.IsDoctor == true)
                                          .Skip(ExcludeRecords)
                                          .Take(pageSize)
                                          .ToList();

                totalCount = _unitOfWork.GenericRepository<ApplicationUser>()
                                        .GetAll(x => x.IsDoctor == true)
                                        .ToList()
                                        .Count;

                vmList = ConvertModelToViewModelList(modeList);
            }
            catch (Exception ex)
            {
                throw;
            }

            return new PagedResult<ApplicationUserViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public PagedResult<ApplicationUserViewModel> GetAllPatient(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public PagedResult<ApplicationUserViewModel> SearchDoctor(int pageNumber, int pageSize, string spicility = null)
        {
            throw new NotImplementedException();
        }
    }
}
