using Blazored.LocalStorage;
using EduTrack.Components.Enums;
using EduTrack.Models;
using EduTrack.Services;
using System.Threading;


namespace EduTrack.Services
{
    public class UserStateService
    {
        public static Action<CancellationToken?> OnUnauthorized { get; set; }

        public static Action<CancellationToken?> OnSignOut { get; set; }
        public static Action<CancellationToken?> OnSignIn { get; set; }

        public static bool IsAuthenticated => _user != null;
        public static bool IsAdmin => _user?.IsAdmin ?? false;

        public static RoleTypes Role => _user?.role ?? default;

        public static ILocalStorageService _localStorageService;

        private static Student _user { get; set; }


        public static async Task InitUserStateAsync()
        {
            _user = await GetUserAsync(default);
            OnSignIn?.Invoke(default);
        }

        public static async Task<Student> GetUserAsync(CancellationToken cancellationToken)
        {
            var user = await _localStorageService.GetItemAsync<Student>("user");
            return user;
        }

        public static async Task SignInAsync(Student user, CancellationToken cancellationToken = default)
        {
            await _localStorageService.SetItemAsync("user", user);
            _user = user;
            OnSignIn?.Invoke(cancellationToken);
        }

        public static async Task SignOutAsync(CancellationToken cancellationToken = default)
        {
            _user = null;
            await _localStorageService.RemoveItemAsync("user", cancellationToken);
            OnSignOut?.Invoke(cancellationToken);
        }

        public static int GetUserId()
        {
            return _user?.Id ?? 0; // Возвращает Id пользователя или 0, если пользователь не аутентифицирован
        }

        public static Student GetCurrentUser()
        {
            return _user; // Возвращает текущего пользователя
        }
        public static async Task<Student> GetCurrentUserAsync()
        {
            return _user; // Возвращает текущего пользователя
        }

    }
}