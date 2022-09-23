using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Airbnb.Domain.Entities.AppUserRelated.CustomFrameworkClasses;
using Airbnb.Domain.Xmls;
using System.Security.Cryptography;

namespace Airbnb.Persistance.Authentication.CustomFrameworkClasses
{
    public class CustomUserManager<TUser> : UserManager<TUser> where TUser : CustomIdentityUser
    {
        public CustomUserManager(IUserStore<TUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<TUser> passwordHasher, IEnumerable<IUserValidator<TUser>> userValidators, IEnumerable<IPasswordValidator<TUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<TUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
//        public async override Task<IdentityResult> CreateAsync(TUser user)
//        {
//            ThrowIfDisposed();
//            await UpdateSecurityStampInternal(user);
//            var result = await ValidateUserAsync(user);
//            if (!result.Succeeded)
//            {
//                return result;
//            }
//            if (Options.Lockout.AllowedForNewUsers && SupportsUserLockout)
//            {
//                await GetUserLockoutStore().SetLockoutEnabledAsync(user, true, CancellationToken);
//            }
//            await UpdateNormalizedUserNameAsync(user);
//            await UpdateNormalizedEmailAsync(user);
//            // added these
//            user.CreatedAt = DateTime.UtcNow;
//            user.ModifiedAt = DateTime.UtcNow;
//            return await Store.CreateAsync(user, CancellationToken);
//        }
//        public override Task<IdentityResult> UpdateAsync(TUser user)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }
//            // added this
//            user.ModifiedAt = DateTime.UtcNow;
//            return UpdateUserAsync(user);
//        }
//        public override async Task<IdentityResult> ResetPasswordAsync(TUser user, string token, string newPassword)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            // Make sure the token is valid and the stamp matches
//            if (!await VerifyUserTokenAsync(user, Options.Tokens.PasswordResetTokenProvider, ResetPasswordTokenPurpose, token))
//            {
//                return IdentityResult.Failed(ErrorDescriber.InvalidToken());
//            }
//            var result = await UpdatePasswordHash(user, newPassword, validatePassword: true);
//            if (!result.Succeeded)
//            {
//                return result;
//            }
//            // added this
//            user.ModifiedAt = DateTime.UtcNow;
//            return await UpdateUserAsync(user);
//        }
//        public async override Task<IdentityResult> ConfirmEmailAsync(TUser user, string token)
//        {
//            ThrowIfDisposed();
//            var store = GetEmailStore();
//            if (user == null)
//                throw new ArgumentNullException(nameof(user));

//            if (!await VerifyUserTokenAsync(user, Options.Tokens.EmailConfirmationTokenProvider, ConfirmEmailTokenPurpose, token))
//            {
//                return IdentityResult.Failed(ErrorDescriber.InvalidToken());
//            }
//            await store.SetEmailConfirmedAsync(user, true, CancellationToken);
//            user.ModifiedAt = DateTime.UtcNow;
//            return await UpdateUserAsync(user);
//        }


//        //Utilities
//        private IUserLockoutStore<TUser> GetUserLockoutStore()
//        {
//            var cast = Store as IUserLockoutStore<TUser>;
//            if (cast == null)
//            {
//                throw new NotSupportedException(Resources.StoreNotIUserLockoutStore);
//            }
//            return cast;
//        }
//        private async Task UpdateSecurityStampInternal(TUser user)
//        {
//            if (SupportsUserSecurityStamp)
//            {
//                await GetSecurityStore().SetSecurityStampAsync(user, NewSecurityStamp(), CancellationToken);
//            }
//        }

//        private IUserSecurityStampStore<TUser> GetSecurityStore()
//        {
//            var cast = Store as IUserSecurityStampStore<TUser>;
//            if (cast == null)
//            {
//                throw new NotSupportedException(Resources.StoreNotIUserSecurityStampStore);
//            }
//            return cast;
//        }
//        private IUserEmailStore<TUser> GetEmailStore(bool throwOnFail = true)
//        {
//            var cast = Store as IUserEmailStore<TUser>;
//            if (throwOnFail && cast == null)
//            {
//                throw new NotSupportedException(Resources.StoreNotIUserEmailStore);
//            }
//            return cast;
//        }
//        private static string NewSecurityStamp()
//        {
//            byte[] bytes = new byte[20];
//#if NETSTANDARD2_0 || NET461
//            _rng.GetBytes(bytes);
//#else
//            RandomNumberGenerator.Fill(bytes);
//#endif
//            return Base32.ToBase32(bytes);
//        }
    }
   
}

