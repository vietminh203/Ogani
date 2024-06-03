using Microsoft.AspNetCore.Identity;
using Ogani.Data;
using Ogani.Models;
using System;
using System.Threading.Tasks;

namespace Ogani.Repository
{
    public interface IAccountRepository
    {
        Task<DateTimeOffset?> GetTimeLooked(string email);

        Task<AppUser> GetUserByEmailAsync(string email);

        Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel);

        Task<SignInResult> PasswordSignInAsync(SignInModel signInModel);

        Task SignOutAsync();

        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model);

        Task<IdentityResult> ConfirmEmailAsync(string uid, string token);

        Task GenerateEmailConfirmationTokenAsync(AppUser user);

        Task GenerateForgotPasswordTokenAsync(AppUser user);

        Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model);
    }
}