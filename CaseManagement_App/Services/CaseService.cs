using CaseManagement_App.Data;
using CaseManagement_App.Entities;
using CaseManagement_App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement_App.Services
{
    internal interface ICaseService
    {
        int CreateCase(CasesModel c);
        void UpdateCase(CasesModel updateCase, int caseId);


        Cases GetCase(int id);
        IEnumerable<Cases> GetAllCases();
        IEnumerable<Cases> GetLatest();

        IEnumerable<int> Get_Statistics();
    }
    internal class CaseService : ICaseService
    {
        UserService userService = new();
        private readonly SqlContext _context = new();

        #region CREATE
        public int CreateCase(CasesModel c)
        {
            var _duplicateCase = _context.Cases.Where(x => x.Header == c.Header).FirstOrDefault();
            if (_duplicateCase == null)
            {
                var _case = new Cases
                {
                    Header = c.Header,
                    Descriptions = c.Descriptions,
                    CreatedDate = c.CreatedDate,
                    UserId = userService.GetUser(c.User.Id).Id,
                    AdminId = userService.CreateAdmin(c.Admin),
                    CaseStateId = 1,
                    UpdatedDate = DateTime.Now
                };

                _context.Cases.Add(_case);
                _context.SaveChanges();
                return _case.Id;
            }
            return _duplicateCase.Id;
        }

        public void UpdateCase(CasesModel updateCase, int caseId)
        {
            var _case = GetCase(caseId);
            if (_case != null)
            {
                _case.Header = updateCase.Header;
                _case.Descriptions = updateCase.Descriptions;
                _case.CreatedDate = updateCase.CreatedDate;
                _case.UpdatedDate = DateTime.Now;

                _context.Cases.Update(_case);
                _context.SaveChanges();
            }
        }
        #endregion

        #region GET
        public IEnumerable<Cases> GetAllCases()
        {
            return _context.Cases.Include(x => x.CaseState).Include(x => x.User).ThenInclude(x => x.ContactInfo).Include(x => x.Admin).ToList();
        }

        public Cases GetCase(int id)
        {
            return _context.Cases.Find(id);
        }

        public IEnumerable<Cases> GetLatest()
        {
            var _caseList = _context.Cases.ToList();
            int caseCount = _caseList.Count;
            if(caseCount > 0)
            {
                for (int i = caseCount; i > (caseCount - 10) || i > 0; i++)
                {
                    _caseList.Add(_caseList[i]);
                }
                return _caseList;
            };
            return _caseList;
            
        }

        public IEnumerable<int> Get_Statistics()
        {
            List<int> resultList = new List<int>();
            var result = _context.Cases.Where(x => x.CaseStateId == 1).Count();
            var result2 = _context.Cases.Where(x => x.CaseStateId == 2).Count();
            var result3 = _context.Cases.Where(x => x.CaseStateId == 3).Count();
            resultList.Add(result + result2 + result3);
            return resultList;
        }
        #endregion
    }
}
