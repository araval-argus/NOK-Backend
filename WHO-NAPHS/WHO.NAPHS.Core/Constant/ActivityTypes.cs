// <copyright file="ActivityTypes.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Core.Common
{
    /// <summary>
    /// Types of activities for checking the user is authorized to perform it or not.
    /// </summary>
    public class ActivityTypes
    {
        /// <summary>
        /// Activity of inviting user.
        /// </summary>
        public const string InviteUser = "InviteUser";

        /// <summary>
        /// Activity of approving user account.
        /// </summary>
        public const string ApproveUserAccount = "ApproveUserAccount";

        /// <summary>
        /// Activity of updating another user account.
        /// </summary>
        public const string UpdateUserAccount = "UpdateUserAccount";

        /// <summary>
        /// Activity of creating a new plan.
        /// </summary>
        public const string CreatePlan = "CreatePlan";

        /// <summary>
        /// activity of viewing an existing plan.
        /// </summary>
        public const string ViewPlan = "ViewPlan";

        /// <summary>
        /// Activity of updating an existing plan.
        /// </summary>
        public const string UpdatePlan = "UpdatePlan";

        /// <summary>
        /// Activity of downloading a plan.
        /// </summary>
        public const string DownloadPlan = "DownloadPlan";

        /// <summary>
        /// Activity of uploading a plan.
        /// </summary>
        public const string UploadPlan = "UploadPlan";

        /// <summary>
        /// Activity of deleting an existing plan.
        /// </summary>
        public const string DeletePlan = "DeletePlan";

        /// <summary>
        /// Activity of activating a plan.
        /// </summary>
        public const string ActivatePlan = "ActivatePlan";

        /// <summary>
        /// Activity of updating plan visibility.
        /// </summary>
        public const string UpdatePlanVisibility = "UpdatePlanVisibility";

        /// <summary>
        /// Activity of adding or editing strategic or operational action.
        /// </summary>
        public const string AddOrEditAction = "AddOrEditAction";

        /// <summary>
        /// Activity of approving a new action.
        /// </summary>
        public const string ApproveNewAction = "ApproveNewAction";

        /// <summary>
        /// Activity of updating an existing strategic action.
        /// </summary>
        public const string UpdateStrategicAction = "UpdateStrategicAction";

        /// <summary>
        /// Activity of updating the implementation status of strategic action.
        /// </summary>
        public const string UpdateImplementationStatusOfStrategicAction = "UpdateImplementationStatusOfStrategicAction";

        /// <summary>
        /// Activity of updating strategic action details.
        /// </summary>
        public const string UpdateActionDetails = "UpdateActionDetails";

        /// <summary>
        /// Activity of completing or reviewing plan.
        /// </summary>
        public const string CompleteOrReviewPlan = "CompleteOrReviewPlan";

        /// <summary>
        /// Activity of cancelling a plan.
        /// </summary>
        public const string CancelPlan = "CancelPlan";

        /// <summary>
        /// Activity of cloning a plan.
        /// </summary>
        public const string ClonePlan = "ClonePlan";

        /// <summary>
        /// Activity of seeing summary dashboards.
        /// </summary>
        public const string SummaryDashboards = "SummaryDashboards";

        /// <summary>
        /// Activity of viewing own country active plan report.
        /// </summary>
        public const string ViewOwnCountryActivePlanReport = "ViewOwnCountryActivePlanReport";
    }
}