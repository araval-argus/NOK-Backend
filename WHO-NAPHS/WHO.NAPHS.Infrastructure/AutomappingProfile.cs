// <copyright file="AutomappingProfile.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Infrastructure
{
    using AutoMapper;
    using WHO.NAPHS.BusinessLogic.ViewModels;
    using WHO.NAPHS.BusinessLogic.ViewModels.Country;
    using WHO.NAPHS.BusinessLogic.ViewModels.CountryPlan;
    using WHO.NAPHS.BusinessLogic.ViewModels.DetailedActivity;
    using WHO.NAPHS.BusinessLogic.ViewModels.Excel;
    using WHO.NAPHS.BusinessLogic.ViewModels.Language;
    using WHO.NAPHS.BusinessLogic.ViewModels.StrategicAction;
    using WHO.NAPHS.BusinessLogic.ViewModels.User;
    using WHO.NAPHS.BusinessLogic.ViewModels.UserClaims;
    using WHO.NAPHS.Infrastructure.Models.Assessments;
    using WHO.NAPHS.Infrastructure.Models.Countries;
    using WHO.NAPHS.Infrastructure.Models.Excel;
    using WHO.NAPHS.Infrastructure.Models.Languages;
    using WHO.NAPHS.Infrastructure.Models.Plans;
    using WHO.NAPHS.Infrastructure.Models.Users;

    /// <summary>
    /// AutoMapper Profile Class.
    /// </summary>
    public class AutomappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutomappingProfile"/> class.
        /// </summary>
        public AutomappingProfile()
        {
            this.CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.UserRole.RoleId))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            this.CreateMap<Country, CountryViewModel>()
                    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            this.CreateMap<Country, CountryDropdownViewModel>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            this.CreateMap<Language, LanguageViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            this.CreateMap<CountryPlan, CountryPlanListViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            this.CreateMap<CountryPlan, CountryPlanViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            this.CreateMap<Country, CountryViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            this.CreateMap<Language, LanguageViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            this.CreateMap<Role, RoleViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            this.CreateMap<User, UserClaimsViewModel>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.UserRole.RoleId))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            this.CreateMap<StrategicAction, StrategicActionViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            this.CreateMap<IHRRecommendations, IHRRecommendationsViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            this.CreateMap<NBWRecommendations, NBWRecommendationsViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            this.CreateMap<DetailedActivity, DetailedActivityViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            this.CreateMap<DetailedActivityType, DetailedActivityTypeViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            this.CreateMap<TechnicalArea, BusinessLogic.ViewModels.CountryPlan.TechnicalAreaViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            this.CreateMap<TechnicalAreaIndicator, TechnicalAreaIndicatorViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            this.CreateMap<CountryPlanIndicator, IndicatorsWithScoreViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            this.CreateMap<Currency, CurrencyViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            this.CreateMap<CountryPlanReview, CountryPlanReviewViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}