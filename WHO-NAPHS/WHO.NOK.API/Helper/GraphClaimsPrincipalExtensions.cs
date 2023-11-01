// <copyright file="GraphClaimsPrincipalExtensions.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.API.Helper
{
    using System.Security.Claims;
    using WHO.NAPHS.BusinessLogic.ViewModels.UserClaims;
    using WHO.NAPHS.Core.Wrappers;

    /// <summary>
    /// Claims principal extension.
    /// </summary>
    public static class GraphClaimsPrincipalExtensions
    {
        /// <summary>
        /// Get user details from the claims.
        /// </summary>
        /// <param name="claimsPrincipal"> <see cref="ClaimsPrincipal"/> object.</param>
        /// <returns> Returns the user claims.</returns>
        public static UserClaimsViewModel GetUser(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.GetUserId() > 0)
            {
                UserClaimsViewModel user = new UserClaimsViewModel()
                {
                    CountryId = claimsPrincipal.GetCountryId(),
                    Enabled = claimsPrincipal.IsActive(),
                    RoleId = claimsPrincipal.GetRole(),
                    UserId = claimsPrincipal.GetUserId(),
                    PrimaryEmail = claimsPrincipal.GetEmail(),
                    ReadOnly = claimsPrincipal.GetReadOnly(),
                    LanguageId = claimsPrincipal.GetLanguageId(),
                    DisplayName = claimsPrincipal.GetDisplayName(),
                    CountryName = claimsPrincipal.GetCountryName(),
                    FirstName = claimsPrincipal.GetFirstName(),
                    LastName = claimsPrincipal.GetLastName(),
                    Region = claimsPrincipal.GetRegionName(),
                    Currency = claimsPrincipal.GetCurrencySign(),
                    ProfilePicture = claimsPrincipal.GetProfilePicture(),
                };

                return user;
            }

            throw new UnauthorizedAccessException("Invalid user.");
        }

        /// <summary>
        /// Add user details to the claims.
        /// </summary>
        /// <param name="identity"> <see cref="ClaimsIdentity"/> object.</param>
        /// <param name="user"> User.</param>
        public static void AddUserDetails(this ClaimsIdentity identity, UserClaimsViewModel user)
        {
            if (user != null)
            {
                identity.AddClaim(new Claim(GraphClaimTypes.DisplayName, user.FirstName + " " + user.LastName));
                identity.AddClaim(new Claim(GraphClaimTypes.CountryId, (user.CountryId ?? 0).ToString()));
                identity.AddClaim(new Claim(GraphClaimTypes.Role, user.RoleId.ToString()));
                identity.AddClaim(new Claim(GraphClaimTypes.Email, user.PrimaryEmail));
                identity.AddClaim(new Claim(GraphClaimTypes.UserId, user.UserId.ToString()));
                identity.AddClaim(new Claim(GraphClaimTypes.IsActive, user.Enabled.GetValueOrDefault() ? "1" : "0"));
                identity.AddClaim(new Claim(GraphClaimTypes.ReadOnly, user.ReadOnly.GetValueOrDefault() ? "1" : "0"));
                identity.AddClaim(new Claim(GraphClaimTypes.LanguageId, (user.LanguageId ?? 1).ToString()));
                identity.AddClaim(new Claim(GraphClaimTypes.FirstName, user.FirstName));
                identity.AddClaim(new Claim(GraphClaimTypes.LastName, user.LastName));
                identity.AddClaim(new Claim(GraphClaimTypes.Region, user.Region == null ? string.Empty : user.Region));
                identity.AddClaim(new Claim(GraphClaimTypes.CountryName, user.CountryName == null ? string.Empty : user.CountryName));
                identity.AddClaim(new Claim(GraphClaimTypes.Currency, user.Currency == null ? string.Empty : user.Currency));
                identity.AddClaim(new Claim(GraphClaimTypes.ProfilePicture, user.ProfilePicture == null ? string.Empty : user.ProfilePicture));
            }
        }

        /// <summary>
        /// Get the display name.
        /// </summary>
        /// <param name="claimsPrincipal"> <see cref="ClaimsPrincipal"/> object.</param>
        /// <returns> Returns the display name.</returns>
        public static string? GetDisplayName(this ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                return claimsPrincipal.FindFirstValue(GraphClaimTypes.DisplayName);
            }
            catch (System.Exception)
            {
            }

            return string.Empty;
        }

        /// <summary>
        /// Get the user id.
        /// </summary>
        /// <param name="claimsPrincipal"> <see cref="ClaimsPrincipal"/> object.</param>
        /// <returns> Returns the user id.</returns>
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                return Convert.ToInt32(claimsPrincipal.FindFirstValue(GraphClaimTypes.UserId));
            }
            catch (System.Exception)
            {
            }

            return 0;
        }

        /// <summary>
        /// Get user email.
        /// </summary>
        /// <param name="claimsPrincipal"> <see cref="ClaimsPrincipal"/> object.</param>
        /// <returns> Returns the email.</returns>
        public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                return claimsPrincipal.FindFirstValue(GraphClaimTypes.Email);
            }
            catch (System.Exception)
            {
            }

            return string.Empty;
        }

        /// <summary>
        /// Get proffered user name.
        /// </summary>
        /// <param name="claimsPrincipal"> <see cref="ClaimsPrincipal"/> object.</param>
        /// <returns> Returns the user name.</returns>
        public static string GetPreferredUsername(this ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                return claimsPrincipal.FindFirstValue(GraphClaimTypes.PreferredUsername);
            }
            catch (System.Exception)
            {
            }

            return string.Empty;
        }

        /// <summary>
        /// Get the user role.
        /// </summary>
        /// <param name="claimsPrincipal"> <see cref="ClaimsPrincipal"/> object.</param>
        /// <returns> Returns the role.</returns>
        public static int GetRole(this ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                return Convert.ToInt32(claimsPrincipal.FindFirstValue(GraphClaimTypes.Role));
            }
            catch (System.Exception)
            {
            }

            return 1;
        }

        /// <summary>
        /// Get the country Id.
        /// </summary>
        /// <param name="claimsPrincipal"> <see cref="ClaimsPrincipal"/> object.</param>
        /// <returns> Returns the country Id.</returns>
        public static int? GetCountryId(this ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                return Convert.ToInt32(claimsPrincipal.FindFirstValue(GraphClaimTypes.CountryId));
            }
            catch (System.Exception)
            {
            }

            return null;
        }

        /// <summary>
        /// Get language id.
        /// </summary>
        /// <param name="claimsPrincipal"> <see cref="ClaimsPrincipal"/> object.</param>
        /// <returns> Returns the language id.</returns>
        public static int? GetLanguageId(this ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                return Convert.ToInt32(claimsPrincipal.FindFirstValue(GraphClaimTypes.LanguageId));
            }
            catch (System.Exception)
            {
            }

            return null;
        }

        /// <summary>
        /// Check if user is active or not.
        /// </summary>
        /// <param name="claimsPrincipal"> <see cref="ClaimsPrincipal"/> object.</param>
        /// <returns> Returns the user's active status.</returns>
        public static bool IsActive(this ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                return claimsPrincipal.FindFirstValue(GraphClaimTypes.IsActive) == "1";
            }
            catch (System.Exception)
            {
            }

            return false;
        }

        /// <summary>
        /// Check if user is read only or not.
        /// </summary>
        /// <param name="claimsPrincipal"> <see cref="ClaimsPrincipal"/> object.</param>
        /// <returns> Returns the read only status of the user.</returns>
        public static bool GetReadOnly(this ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                return claimsPrincipal.FindFirstValue(GraphClaimTypes.ReadOnly) == "1";
            }
            catch (System.Exception)
            {
            }

            return false;
        }

        /// <summary>
        /// Get country name form the claims.
        /// </summary>
        /// <param name="claimsPrincipal">  Claims principal.</param>
        /// <returns> Returns the country name.</returns>
        public static string GetCountryName(this ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                return claimsPrincipal.FindFirstValue(GraphClaimTypes.CountryName);
            }
            catch (System.Exception)
            {
            }

            return string.Empty;
        }

        /// <summary>
        /// Get first name form the claims.
        /// </summary>
        /// <param name="claimsPrincipal">  Claims principal.</param>
        /// <returns> Returns the First name.</returns>
        public static string GetFirstName(this ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                return claimsPrincipal.FindFirstValue(GraphClaimTypes.FirstName);
            }
            catch (System.Exception)
            {
            }

            return string.Empty;
        }

        /// <summary>
        /// Get last name form the claims.
        /// </summary>
        /// <param name="claimsPrincipal">  Claims principal.</param>
        /// <returns> Returns the last name.</returns>
        public static string GetLastName(this ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                return claimsPrincipal.FindFirstValue(GraphClaimTypes.LastName);
            }
            catch (System.Exception)
            {
            }

            return string.Empty;
        }

        /// <summary>
        /// Get region name form the claims.
        /// </summary>
        /// <param name="claimsPrincipal">  Claims principal.</param>
        /// <returns> Returns the region name.</returns>
        public static string GetRegionName(this ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                return claimsPrincipal.FindFirstValue(GraphClaimTypes.Region);
            }
            catch (System.Exception)
            {
            }

            return string.Empty;
        }

        /// <summary>
        /// Get currency sign name form the claims.
        /// </summary>
        /// <param name="claimsPrincipal">  Claims principal.</param>
        /// <returns> Returns the currency sign name.</returns>
        public static string GetCurrencySign(this ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                return claimsPrincipal.FindFirstValue(GraphClaimTypes.Currency);
            }
            catch (System.Exception)
            {
            }

            return string.Empty;
        }

        /// <summary>
        /// Get currency profile picture the claims.
        /// </summary>
        /// <param name="claimsPrincipal">  Claims principal.</param>
        /// <returns> Returns the currency sign name.</returns>
        public static string GetProfilePicture(this ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                return claimsPrincipal.FindFirstValue(GraphClaimTypes.ProfilePicture);
            }
            catch (System.Exception)
            {
            }

            return string.Empty;
        }
    }
}
