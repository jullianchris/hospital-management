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
    public class ContactService : IContactService
    {

        private IUnitOfWork _unitOfWork;

        public ContactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteContact(int id)
        {
            var model = _unitOfWork.GenericRepository<Contact>().GetById(id);
            _unitOfWork.GenericRepository<Contact>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<ContactViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new ContactViewModel();
            int totalCount;
            List<ContactViewModel> vmList = new List<ContactViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                var modeList = _unitOfWork.GenericRepository<Contact>()
                                          .GetAll()
                                          .Skip(ExcludeRecords)
                                          .Take(pageSize)
                                          .ToList();

                totalCount = _unitOfWork.GenericRepository<Contact>()
                                        .GetAll()
                                        .ToList()
                                        .Count;

                vmList = ConvertModelToViewModelList(modeList);
            }
            catch (Exception ex)
            {
                throw;
            }

            return new PagedResult<ContactViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public ContactViewModel GetConatcById(int ContactId)
        {
            var model = _unitOfWork.GenericRepository<Contact>().GetById(ContactId);
            var vm = new ContactViewModel(model);
            return vm;
        }

        public void InsertContact(ContactViewModel contact)
        {
            var model = new ContactViewModel().ConvertViewModel(contact);
            _unitOfWork.GenericRepository<Contact>().Add(model);
            _unitOfWork.Save();
        }

        public void UpdateContact(ContactViewModel contact)
        {
            var model = new ContactViewModel().ConvertViewModel(contact);
            var ModelById = _unitOfWork.GenericRepository<Contact>().GetById(model.Id);
            ModelById.Phone = contact.Phone;
            ModelById.Email = contact.Email;
            ModelById.HospitalId = contact.HospitalId;

            _unitOfWork.GenericRepository<Contact>().Update(ModelById);
            _unitOfWork.Save();
        }

        private List<ContactViewModel> ConvertModelToViewModelList(List<Contact> modelList)
        {
            return modelList.Select(x => new ContactViewModel(x)).ToList();
        }
    }
}
