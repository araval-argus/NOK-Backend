// <copyright file="UserService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

// to suppress possibly null reference warnings.
#pragma warning disable CS8600,CS8604

namespace WHO.NOK.Infrastructure.ServiceImplementation
{
    using System.Collections.Generic;
    using System.Data;
    using AutoMapper;
    using Microsoft.AspNetCore.StaticFiles;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.BusinessLogic.Helper;
    using WHO.NOK.BusinessLogic.ServiceInterfaces;
    using WHO.NOK.BusinessLogic.ViewModels.PaginatedResponses;
    using WHO.NOK.BusinessLogic.ViewModels.User;
    using WHO.NOK.BusinessLogic.ViewModels.UserClaims;
    using WHO.NOK.Core.Common;
    using WHO.NOK.Core.Common.Resources;
    using WHO.NOK.Core.Constant;
    using WHO.NOK.Core.Wrappers;
    using WHO.NOK.Infrastructure.Helper;
    using WHO.NOK.Infrastructure.Models.DatabaseContext;
    using WHO.NOK.Infrastructure.Models.Users;

    /// <summary>
    /// Implement the <see cref="IUserService"/> interface.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;
        private readonly IStringLocalizer<Resources> localizer;
        private readonly IMapper mapper;
       
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="context"> <see cref="ApplicationDbContext"/> Database context.</param>
        /// <param name="mapper"> <see cref="IMapper"/> Auto mapper.</param>
        /// <param name="localizer"> <see cref="IStringLocalizer"/> Localizer.</param>
        /// <param name="commonService"> <see cref="ICommonService"/> Common service. </param>
        /// <param name="emailService"> <see cref="IEmailService"/> Email service. </param>
        public UserService(
            ApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<Resources> localizer)
        {
            this.context = context;
            this.mapper = mapper;
            this.localizer = localizer;
        }

        /// <inheritdoc/>
        public async Task<UserClaimsViewModel> GetUserByEmailForClaimsAsync(string email)
        {
            UserClaimsViewModel? model = new ();

            List<SqlParameter> parameters = new ()
            {
                new () { ParameterName = "@UserEmail", Value = email },
            };

            await this.context.ExecuteProcedureAsync(
                CommandType.StoredProcedure,
                "sp_GetUserClaimsDetails",
                (ds) =>
                {
                    model = ds.Tables[0].ConvertToList<UserClaimsViewModel>().FirstOrDefault();
                },
                parameters,
                1800);

            return model;
        }
    }
}