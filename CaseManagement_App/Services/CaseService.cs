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
        void UpdateCase(string uHeader, string uDescription, int caseStateId, int caseId);
        public void CreateData();


        Cases GetCase(int id);
        IEnumerable<Cases> GetAllCases();
        public IEnumerable<CaseState> GetStates();
        public CaseState GetState(int id);
        int Get_Statistics(int id);
        public IEnumerable<Cases> GetLastCases();
    }
    internal class CaseService : ICaseService
    {
        UserService userService = new();
        private readonly SqlContext _context = new();

        #region CREATE

        //CreateCase tar in en CaseModel och med den infon skapar den en entity och sparar i databasen.
        public int CreateCase(CasesModel c)
        {
            var _duplicateCase = _context.Cases.Include(x => x.User).Where(x => x.Header == c.Header && x.User.Id == c.User.Id).FirstOrDefault();
            if (_duplicateCase == null)
            {
                Cases _case;
                if (c.Admin != null)
                {
                    _case = new Cases
                    {
                        Header = c.Header,
                        Descriptions = c.Descriptions,
                        CreatedDate = c.CreatedDate,
                        UserId = userService.GetUser(c.User.Id).Id,
                        AdminId = userService.CreateAdmin(c.Admin),
                        CaseStateId = 1,
                        UpdatedDate = DateTime.Now
                    };
                }
                else
                {
                    _case = new Cases
                    {
                        Header = c.Header,
                        Descriptions = c.Descriptions,
                        CreatedDate = c.CreatedDate,
                        UserId = userService.GetUser(c.User.Id).Id,
                        CaseStateId = 1,
                        UpdatedDate = DateTime.Now
                    };
                }
                _context.Cases.Add(_case);
                _context.SaveChanges();
                return 0;
            }
            return _duplicateCase.Id;
        }

        //UpdateCase tar in uppdaterad info och uppdaterar databasen med detta.
        public void UpdateCase(string uHeader, string uDescription, int caseStateId, int caseId)
        {
            var _updatedCaseState = _context.CaseStates.Find(caseStateId);
            var _case = GetCase(caseId);
            if (_case != null)
            {
                _case.Header = uHeader;
                _case.Descriptions = uDescription;
                _case.CaseState = _updatedCaseState;
                _case.CreatedDate = _case.CreatedDate;
                _case.UpdatedDate = DateTime.Now;

                _context.Cases.Update(_case);
                _context.SaveChanges();
            }
        }

        //CreateData skapar standard värden i databasen när man startar programmet
        public void CreateData()
        {
            if(_context.CaseStates.Find(1) == null)
            {
                CaseState _caseStateCr = new CaseState { Name = "Created" };
                _context.CaseStates.Add(_caseStateCr);
            }
            if (_context.CaseStates.Find(2) == null)
            {
                CaseState _caseStateIn = new CaseState { Name = "In-progress" };
                _context.CaseStates.Add(_caseStateIn);
            }
            if (_context.CaseStates.Find(3) == null)
            {
                CaseState _caseStateCl = new CaseState { Name = "Closed" };
                _context.CaseStates.Add(_caseStateCl);
            }
            if (_context.Roles.Find(1) == null)
            {
                Role _roleU = new Role { Name = "User" };
                _context.Roles.Add(_roleU);
            }
            if (_context.Roles.Find(2) == null)
            {
                Role _roleA = new Role { Name = "Admin" };
                _context.Roles.Add(_roleA);
            }
            _context.SaveChanges();

        }
        #endregion

        #region GET

        //GetAllCases hämtar alla Cases från databasen
        public IEnumerable<Cases> GetAllCases()
        {
            return _context.Cases.Include(x => x.CaseState).Include(x => x.User).ThenInclude(x => x.ContactInfo).Include(x => x.Admin).ToList();
        }

        //GetCase hämtar enskilt Cases från databasen
        public Cases GetCase(int id)
        {
            return  _context.Cases.Include(x => x.CaseState).Include(x => x.User).ThenInclude(x => x.ContactInfo).Include(x => x.Admin).Where(x => x.Id == id).FirstOrDefault();
        }

        //GetStates hämtar alla CaseStates från databasen
        public IEnumerable<CaseState> GetStates()
        {
            return _context.CaseStates.ToList();
        }

        //GetState hämtar enskilt CaseState från databasen
        public CaseState GetState(int id)
        {
            return _context.CaseStates.Find(id);
        }

        //Get_Statistics hämtar antalet Cases med ett specifikt CaseState
        public int Get_Statistics(int id)
        {
            return _context.Cases.Where(x => x.CaseStateId == id).Count();
        }

        //GetLastCases hämtar de senaste 10 Cases om det finns så många annars returnerar den alla som finns
        public IEnumerable<Cases> GetLastCases()
        {
            List<Cases> _lastCasesList = new();
            List<Cases> _caseList = (List<Cases>)GetAllCases();
            _caseList.Reverse();
            if(_caseList.Count > 10 && _caseList.Count != 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    _lastCasesList.Add(_caseList[i]);
                }
                return _lastCasesList;
            }
            else
            {
                return _caseList;
            }
        }
        #endregion
    }
}
