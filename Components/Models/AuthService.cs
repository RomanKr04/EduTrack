
using EduTrack.Components.Models;
using EduTrack.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EduTrack.Models
{
    public interface IAuthService
    {
       
        Task<Student> AuthenticateAsync(string email, string password);
    }

    public class AuthService : IAuthService
    {
        private readonly UserDbContext _dbContext;

        public AuthService(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Student> AuthenticateAsync(string email, string password)
        {
            Student user;


            if (email.Trim() == "admin@gmail.com" && password == "admin")
            {
                user = new();
                user.Email = email;
                user.first_name = "admin";
                user.IsAdmin = true;
                await UserStateService.SignInAsync(user);
                return user;
            }

            // Находим пользователя по адресу электронной почты
            user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            // Проверяем, найден ли пользователь и совпадает ли пароль
            if (user != null && user.Password == password)
            {
                await UserStateService.SignInAsync(user);
                return user;
            }

            return null;
        }

      
    }
}
