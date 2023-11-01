// <copyright file="CachedUserService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Infrastructure.CacheImplementation
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Memory;
    using WHO.NAPHS.BusinessLogic.CacheInterfaces;
    using WHO.NAPHS.BusinessLogic.Features.User;
    using WHO.NAPHS.BusinessLogic.ServiceInterfaces;
    using WHO.NAPHS.BusinessLogic.ViewModels.PaginatedResponses;
    using WHO.NAPHS.BusinessLogic.ViewModels.User;
    using WHO.NAPHS.BusinessLogic.ViewModels.UserClaims;

    /// <summary>
    /// Cache Implementation for UserService.
    /// </summary>
    public class CachedUserService : IUserService, ICacheService
    {
        private const string UserListCacheKey = "UserList";
        private const string UserClaimsCacheKey = "UserClaims";
        private readonly IMemoryCache memoryCache;
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CachedUserService"/> class.
        /// </summary>
        /// <param name="memoryCache"><see cref="IMemoryCache"/> interface.</param>
        /// <param name="userService"><see cref="IUserService"/> Service Implementation.</param>
        public CachedUserService(IMemoryCache memoryCache, IUserService userService)
        {
            this.memoryCache = memoryCache;
            this.userService = userService;
        }

        /// <inheritdoc/>
        public async Task<UserViewModel> GetByIdAsync(GetUserCommand model)
        {
            return await this.userService.GetByIdAsync(model);
        }

        /// <inheritdoc/>
        public async Task<UserViewModel> UpdateUserDetailsAsync(UpdateUserDetailsCommand model)
        {
            return await this.userService.UpdateUserDetailsAsync(model);
        }

        /// <inheritdoc/>
        public void ClearCache()
        {
            this.memoryCache.Remove(UserListCacheKey);
        }

        /// <inheritdoc/>
        public async Task<UserViewModel> GetUserByEmailAsync(string email)
        {
            return await this.userService.GetUserByEmailAsync(email);
        }

        /// <inheritdoc/>
        public async Task<UserViewModel> CreateUserAsync(CreateUserCommand model)
        {
            return await this.userService.CreateUserAsync(model);
        }

        /// <inheritdoc/>
        public async Task<UserClaimsViewModel> GetUserByEmailForClaimsAsync(string email)
        {
            return await this.userService.GetUserByEmailForClaimsAsync(email);
        }

        /// <inheritdoc/>
        public async Task<bool> DeactivateUserAsync(DeactivateUserCommand request)
        {
            return await this.userService.DeactivateUserAsync(request);
        }

        /// <inheritdoc/>
        public async Task<bool> ActivateUserAsync(ActivateUserCommand request)
        {
            return await this.userService.ActivateUserAsync(request);
        }

        /// <inheritdoc/>
        public async Task<PaginatedResponse<UserViewModel>> GetAllPaginatedAndFilteredUsers(GetUsersCommand request)
        {
            // var options = new MemoryCacheEntryOptions()
            //     .SetSlidingExpiration(TimeSpan.FromSeconds(10))
            //     .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

            // if (this.memoryCache.TryGetValue(UserListCacheKey, out PaginatedResponse<UserViewModel> result))
            // {
            //     return result;
            // }
            var result = await this.userService.GetAllPaginatedAndFilteredUsers(request);

            // this.memoryCache.Set(UserListCacheKey, result, options);
            return result;
        }

        /// <inheritdoc/>
        public async Task<List<UserViewModel>> GetExportUsersAsync(int userId)
        {
            var result = await this.userService.GetExportUsersAsync(userId);
            return result;
        }

        /// <inheritdoc/>
        public async Task<UserClaimsViewModel> GetUserFromClaimsAsync(UserClaimsViewModel user)
        {
            var options = new MemoryCacheEntryOptions()
               .SetSlidingExpiration(TimeSpan.FromSeconds(10))
               .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

            if (this.memoryCache.TryGetValue(UserClaimsCacheKey, out UserClaimsViewModel result))
            {
                return result;
            }

            result = await this.userService.GetUserFromClaimsAsync(user);

            this.memoryCache.Set(UserListCacheKey, result, options);

            return result;
        }

        /// <inheritdoc/>
        public async Task<string?> UploadUserProfilePicAsync(UploadUserProfilePicCommand request)
        {
            return await this.UploadUserProfilePicAsync(request);
        }

        /// <inheritdoc/>
        public Task<ProfilePictureViewModel> GetProfilePictureAsync(GetProfilePictureCommand request)
        {
            return this.GetProfilePictureAsync(request);
        }

        /// <inheritdoc/>
        public async Task<UserViewModel> UpdateMyProfileAsync(UpdateMyProfileCommand model)
        {
            return await this.userService.UpdateMyProfileAsync(model);
        }

        /// <inheritdoc/>
        public async Task<string?> UploadUserProfilePicForUserManagementAsync(UploadUserProfilePicCommand request)
        {
            return await this.userService.UploadUserProfilePicForUserManagementAsync(request);
        }

        /// <inheritdoc/>
        public async Task<UserViewModel> DeleteUserAsync(DeleteUserCommand request)
        {
            return await this.userService.DeleteUserAsync(request);
        }
    }
}
