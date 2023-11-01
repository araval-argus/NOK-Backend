// <copyright file="Enums.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Core.Common
{
    /// <summary>
    /// Plan type Enum.
    /// </summary>
    public enum PlanType
    {
        /// <summary>
        /// Represents a Strategic plan.
        /// </summary>
        Strategic = 1,

        /// <summary>
        /// Represents a Operational plan.
        /// </summary>
        Operational = 2,
    }

    /// <summary>
    /// Assessment type enum.
    /// </summary>
    public enum AssessmentType
    {
        /// <summary>
        /// Represents JEE assessment.
        /// </summary>
        JEE = 1,

        /// <summary>
        /// Represents SPAR assessment.
        /// </summary>
        ESPAR = 2,
    }

    /// <summary>
    /// Plan status Enum.
    /// </summary>
    public enum PlanStatus
    {
        /// <summary>
        /// Represents Cancelled.
        /// </summary>
        Cancelled = 1,

        /// <summary>
        /// Represents Active.
        /// </summary>
        Active = 2,

        /// <summary>
        /// Represents Draft.
        /// </summary>
        Draft = 3,

        /// <summary>
        /// Represents Complete.
        /// </summary>
        Complete = 4,
    }

    /// <summary>
    /// Plan stage enum.
    /// </summary>
    public enum PlanStage
    {
        /// <summary>
        /// Represents Not started.
        /// </summary>
        NotStarted = 1,

        /// <summary>
        /// Represents Just started.
        /// </summary>
        JustStarted = 2,

        /// <summary>
        /// Represents On going.
        /// </summary>
        OnGoing = 3,

        /// <summary>
        /// Represents advanced stage.
        /// </summary>
        AdvancedStage = 4,

        /// <summary>
        /// Represents completed.
        /// </summary>
        Completed = 5,
    }

    /// <summary>
    /// Roles enum.
    /// </summary>
    public enum Roles
    {
        /// <summary>
        /// Represents System admin.
        /// </summary>
        SystemAdmin = 1,

        /// <summary>
        /// Represents Regional admin.
        /// </summary>
        RegionalAdmin = 2,

        /// <summary>
        /// Represents Country Admin.
        /// </summary>
        CountryAdmin = 3,

        /// <summary>
        /// Represents Country User.
        /// </summary>
        CountryUser = 4,

        /// <summary>
        /// Represents Secretariat.
        /// </summary>
        Secretariat = 5,

        /// <summary>
        /// Represents Global viewer.
        /// </summary>
        GlobalViewer = 6,

        /// <summary>
        /// Represents Country viewer.
        /// </summary>
        CountryViewer = 7,
    }

    /// <summary>
    /// Languages enum.
    /// </summary>
    public enum Languages
    {
        /// <summary>
        /// Represents Arabic.
        /// </summary>
        Arabic = 1,

        /// <summary>
        /// Represents Chinese.
        /// </summary>
        Chinese = 2,

        /// <summary>
        /// Represents English.
        /// </summary>
        English = 3,

        /// <summary>
        /// Represents French.
        /// </summary>
        French = 4,

        /// <summary>
        /// Represents Russian.
        /// </summary>
        Russian = 5,

        /// <summary>
        /// Represents Spanish.
        /// </summary>
        Spanish = 6,
    }

    /// <summary>
    /// Strategic plan creation status.
    /// </summary>
    public enum StrategicPlanCreationStatus
    {
        /// <summary>
        /// Represents Date overlapping.
        /// </summary>
        DateOverlapping = 0,

        /// <summary>
        /// Represents Other exception.
        /// </summary>
        OtherException = 1,
    }

    /// <summary>
    /// Profile Status.
    /// </summary>
    public enum ProfileStatus
    {
        /// <summary>
        /// Represents pending request from admin side.
        /// </summary>
        PendingFromAdmin = 1,

        /// <summary>
        /// Represents pending request from user side.
        /// </summary>
        PendingFromUser = 2,

        /// <summary>
        /// Represents rejected request by admin.
        /// </summary>
        RejectedByAdmin = 3,

        /// <summary>
        /// Represents rejected request by user.
        /// </summary>
        RejectedByUser = 4,
    }

    /// <summary>
    /// Source type enum.
    /// </summary>
    public enum SourceType
    {
        /// <summary>
        /// Represents custom source type.
        /// </summary>
        Custom = 0,

        /// <summary>
        /// Represents JEE assessments.
        /// </summary>
        JEEAssessments = 1,

        /// <summary>
        /// Represents ESPAR assessments.
        /// </summary>
        ESPARAssessments = 2,

        /// <summary>
        /// Represents JEE IHR Benchmarks.
        /// </summary>
        IHRBenchmarks = 3,

        /// <summary>
        /// Represents NBW assessments.
        /// </summary>
        NBWAssessments = 4,
    }

    /// <summary>
    /// Indicator Types enum.
    /// </summary>
    public enum IndicatorType
    {
        /// <summary>
        /// Represents JEE assessments.
        /// </summary>
        JEEAssessments = 1,

        /// <summary>
        /// Represents ESPAR assessments.
        /// </summary>
        ESPARAssessments = 2,

        /// <summary>
        /// Represents JEE IHR Benchmarks.
        /// </summary>
        IHRBenchmarks = 3,

        /// <summary>
        /// Represents NBW assessments.
        /// </summary>
        NBWAssessments = 4,
    }

    /// <summary>
    /// Excel import types enum.
    /// </summary>
    public enum ExcelTypes
    {
        /// <summary>
        /// Represents Custom uploads.
        /// </summary>
        Custom = 0,

        /// <summary>
        /// Represents JEE assessments.
        /// </summary>
        JEEAssessments = 1,

        /// <summary>
        /// Represents ESPAR assessments.
        /// </summary>
        ESPARAssessments = 2,

        /// <summary>
        /// Represents JEE IHR Benchmarks.
        /// </summary>
        IHRBenchmarks = 3,

        /// <summary>
        /// Represents NBW assessments.
        /// </summary>
        NBWAssessments = 4,
    }

    /// <summary>
    /// Mapping type enum.
    /// </summary>
    public enum MappingTypes
    {
        /// <summary>
        /// Represents TechnicalArea.
        /// </summary>
        TechnicalArea = 0,

        /// <summary>
        /// Represents indicators.
        /// </summary>
        Indicators = 1,
    }

    /// <summary>
    /// Enum for strategic action impact.
    /// </summary>
    public enum ActionImpact
    {
        /// <summary>
        /// Represents the high impact.
        /// </summary>
        High = 1,

        /// <summary>
        /// Represents medium impact.
        /// </summary>
        Medium = 2,

        /// <summary>
        /// Represents low impact.
        /// </summary>
        Low = 3,
    }

    /// <summary>
    /// Enum for action feasibility.
    /// </summary>
    public enum ActionFeasibility
    {
        /// <summary>
        /// Represents easy feasibility.
        /// </summary>
        Easy = 1,

        /// <summary>
        /// Represents medium feasibility.
        /// </summary>
        Medium = 2,

        /// <summary>
        /// Represents difficult feasibility.
        /// </summary>
        Difficult = 3,
    }

    /// <summary>
    /// Enum for action priority.
    /// </summary>
    public enum ActionPriority
    {
        /// <summary>
        /// Represents very high priority.
        /// </summary>
        VeryHigh = 1,

        /// <summary>
        /// Represents high priority.
        /// </summary>
        High = 2,

        /// <summary>
        /// Represents medium priority.
        /// </summary>
        Medium = 3,

        /// <summary>
        /// Represents low priority.
        /// </summary>
        Low = 4,

        /// <summary>
        /// Represents very low priority.
        /// </summary>
        VeryLow = 5,
    }

    /// <summary>
    /// Enum to set the strategic action source.
    /// </summary>
    public enum ActionSource
    {
        /// <summary>
        /// Represents jee recommendation actions.
        /// </summary>
        JEERecommendation = 1,

        /// <summary>
        /// Represents IHR recommendation actions.
        /// </summary>
        IHR = 2,

        /// <summary>
        /// Represents NBW detailed actions.
        /// </summary>
        NBW = 3,
    }

    /// <summary>
    /// Enum for detailed activity feasibility.
    /// </summary>
    public enum DetailedActivityFeasibility
    {
        /// <summary>
        /// Represents easy feasibility.
        /// </summary>
        Easy = 1,

        /// <summary>
        /// Represents medium feasibility.
        /// </summary>
        Medium = 2,

        /// <summary>
        /// Represents difficult feasibility.
        /// </summary>
        Difficult = 3,
    }

    /// <summary>
    /// Enum for detailed activity Impact.
    /// </summary>
    public enum DetailedActivityImpact
    {
         /// <summary>
        /// Represents the high impact.
        /// </summary>
        High = 1,

        /// <summary>
        /// Represents medium impact.
        /// </summary>
        Medium = 2,

        /// <summary>
        /// Represents low impact.
        /// </summary>
        Low = 3,
    }

    /// <summary>
    /// Enum for  detailed activity priority.
    /// </summary>
    public enum DetailedActivityPriority
    {
        /// <summary>
        /// Represents very high priority.
        /// </summary>
        VeryHigh = 1,

        /// <summary>
        /// Represents high priority.
        /// </summary>
        High = 2,

        /// <summary>
        /// Represents medium priority.
        /// </summary>
        Medium = 3,

        /// <summary>
        /// Represents low priority.
        /// </summary>
        Low = 4,

        /// <summary>
        /// Represents very low priority.
        /// </summary>
        VeryLow = 5,
    }

    /// <summary>
    /// Enum for country plan frequency.
    /// </summary>
    public enum CountryPlanFrequency
    {
        /// <summary>
        /// Represents the Quarterly review frequency.
        /// </summary>
        Quarterly = 1,

        /// <summary>
        /// Represents the semi annually review frequency.
        /// </summary>
        SemiAnnually = 2,

        /// <summary>
        /// Represents the annually review frequency.
        /// </summary>
        Annually = 3,
    }

    /// <summary>
    /// Audit log row status.
    /// </summary>
    public enum AuditLogRowStatus
    {
        /// <summary>
        /// Represents new added row.
        /// </summary>
        Added = 1,

        /// <summary>
        /// Represents updated existing row.
        /// </summary>
        Edited = 2,

        /// <summary>
        /// Represents the deleted row.
        /// </summary>
        Deleted = 3,
    }

    /// <summary>
    /// Represents status of review.
    /// </summary>
    public enum ReviewStatus
    {
        /// <summary>
        /// Represents due status.
        /// </summary>
        Due = 1,

        /// <summary>
        /// Represents In Progress status.
        /// </summary>
        InProgress = 2,

        /// <summary>
        /// Represents Completed status.
        /// </summary>
        Completed = 3,
    }
}