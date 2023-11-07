using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFScholifyApp.DAL.ClassRepository;
using WPFScholifyApp.DAL.DBClasses;

namespace WPFScholifyApp.BLL
{
    public class UserService
    {
        private IGenericRepository<User> _userRepository;

        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public User Authenticate(string email, string password, string role)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == email && u.Password == password && u.Role == role);

            return user;
        }

        public User AuthenticateEmail(string email)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == email);

            return user;
        }

        public User AuthenticatePassword(string password)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Password == password);

            return user;
        }

        public User GetInfoByNameSurname(string name, string surname)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.FirstName == name && u.LastName == surname);

            return user;
        }

    }
}
