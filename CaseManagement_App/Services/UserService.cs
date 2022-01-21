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
    internal interface IUserService
    {
        int CreateUser(UserModel user);
        int CreateAdmin(AdminModel admin);
        int CreateAddress(AddressModel address);
        int CreateContactInfo(ContactInfoModel contactInfo);
        int CreateRole(RoleModel role);


        User GetUser(int id);
        IEnumerable<User> GetAllUsers();
        IEnumerable<Admin> GetAllAdmins();
        IEnumerable<User> GetLatest();
        Admin GetAdmin(int id);
    }
    internal class UserService : IUserService
    {
        private readonly SqlContext _context = new();

        #region CREATE
        public int CreateRole(RoleModel role)
        {
            var _duplicateRole = _context.Roles.Where(x => x.Name == role.Name).FirstOrDefault();
            if(_duplicateRole == null)
            {
                var _role = new Role { Name = role.Name };
                _context.Roles.Add(new Role { Name = role.Name });
                _context.SaveChanges();
                return _role.Id;
            }
            return _duplicateRole.Id;
        }

        public int CreateAddress(AddressModel address)
        {
            var _duplicateAddress = _context.Addresses.Where(x => x.StreetName == address.StreetName && x.PostalCode == address.PostalCode && x.City == address.City && x.Country == address.Country).FirstOrDefault();
            if(_duplicateAddress == null)
            {
                var _address = new Address
                {
                    StreetName = address.StreetName,
                    PostalCode = address.PostalCode,
                    City = address.City,
                    Country = address.Country
                };

                _context.Addresses.Add(_address);
                _context.SaveChanges();
                return _address.Id;
            }
            return _duplicateAddress.Id;
        }

        public int CreateContactInfo(ContactInfoModel contactInfo)
        {
            var _duplicateContactInfo = _context.ContactInfos.Where(x => x.Email == contactInfo.Email).FirstOrDefault();
            if(_duplicateContactInfo == null)
            {
                var _contactInfo = new ContactInfo
                {
                    Email = contactInfo.Email,
                    PhoneNumber = contactInfo.PhoneNumber
                };

                _context.ContactInfos.Add(_contactInfo);
                _context.SaveChanges();
                return _contactInfo.Id;
            }
            return _duplicateContactInfo.Id;
        }

        public int CreateUser(UserModel user)
        {
            var _duplicateUser = _context.Users.Where(x => x.ContactInfo.Email == user.ContactInfo.Email).FirstOrDefault();
            if(_duplicateUser == null)
            {
                var _user = new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    AddressId = CreateAddress(user.Address),
                    ContactInfoId = CreateContactInfo(user.ContactInfo),
                    RoleId = CreateRole(user.Role) 
                };

                _context.Users.Add(_user);
                _context.SaveChanges();
                return _user.Id;
            }
            return _duplicateUser.Id;
        }

        public int CreateAdmin(AdminModel admin)
        {
            var _duplicateAdmin = _context.Admins.Where(x => x.FirstName == admin.FirstName).FirstOrDefault();
            if (_duplicateAdmin == null)
            {
                var _admin = new Admin
                {
                    FirstName = admin.FirstName,
                    LastName= admin.LastName,
                    RoleId = CreateRole(admin.Role)
                };

                _context.Admins.Add(_admin);
                _context.SaveChanges();
                return _admin.Id;
            }
            return _duplicateAdmin.Id;
        }

        #endregion

        #region GET
        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.Include(x => x.Address)
                .Include(x => x.Role)
                .Include(x => x.ContactInfo);
        }

        public IEnumerable<User> GetLatest()
        {
            var _userList = _context.Users.ToList();
            int usersCount = _userList.Count;
            if(usersCount > 0)
            {
                for (int i = usersCount; i > (usersCount - 10) || i > 0; i++)
                {
                    _userList.Add(_userList[i]);
                }
                return _userList;
            }
            return _userList;
        }

        public User GetUser(int id)
        {
            return _context.Users.Find(id);
        }

        public Admin GetAdmin(int id)
        {
            return _context.Admins.Find(id);
        }

        public IEnumerable<Admin> GetAllAdmins()
        {
            return _context.Admins.Include(x => x.Role);
        }
        #endregion

    }
}
