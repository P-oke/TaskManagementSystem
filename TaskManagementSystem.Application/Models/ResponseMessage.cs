using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.Models
{
    public class ResponseMessage
    {
        public const string ErrorMessage000 = "USER NOT FOUND!";
        public const string ErrorMessage507 = "INCORRECT USERNAME OR PASSWORD";
        public const string ErrorMessage503 = "ACCOUNT IS LOCKED, CONTACT ADMIN";
        public const string ErrorMessage502 = "EMAIL NOT CONFIRMED, KINDLY CONFIRM YOUR ACCOUNT";
        public const string ErrorMessage504 = "INCORRECT USERNAME";
        public const string ErrorMessage510 = "ACCESS DENIED";
        public const string ErrorMessage511 = "ACCOUNT CONFIRMATION FAILED";
        public const string ErrorMessage512 = "CONFIRMATION TOKEN INVALID";
        public const string ErrorMessage513 = "CONFIRMATION EMAIL INVALID";
        public const string ErrorMessage500 = "REFRESH TOKEN NOT FOUND!";
        public const string ErrorMessage505 = "ACCOUNT ALREADY LOCKED";
        public const string ErrorMessage506 = "ACCOUNT LOCKED";
        public const string ErrorMessage508 = "ACCOUNT IS NOT LOCKED";

        public const string AccountUnlocked = "ACCOUNT UNLOCKED!";
        public const string SuccessfullyUpdatedClaim = "PERMMISSION UPDATED";
        public const string PasswordChanged = "PASSWORD CHANGED SUCCESSFULLY!";
        public const string PasswordChangedFailure = "PASSWORD CHANGED FAILED";
        public const string AccountAlreadyConfirmed = "ACCOUNT HAS ALREADY BEEN CONFIRMED, YOU CAN NOW LOG IN!";
        public const string AccountConfirmed = "ACCOUNT SUCCESSFULLY CONFIRMED!";
        public const string AccountCreationFailure = "ACCOUNT CREATION FAILED";
        public const string AccountCreationSuccess = "ACCOUNT CREATED SUCCESSFULLY";
        public const string PasswordResetCodeSent = "PASSWORD RESET CODE SENT!";
        public const string RemovePermmissionFailure = "ERROR REMOVING PERMISSION";
        public const string AddPermmissionFailure = "ERROR ADDING PERMISSION";
        public const string UserTypeFailure = "USER TYPE NOT ADMIN";
        public const string AddToRoleFailure = "FAILED TO ADD USER TO ROLE";
        public const string SuccessMessage000 = "SUCCESSFUL";
        public const string FailureMessage000 = "FAILED";
        public const string SuccessfullyDeletedAUser = "USER HAS BEEN SUCCESSFULLY DELETED!";


        public const string TaskDoesNotExist = "TASK DOESN'T EXISTS";
        public const string TaskDoesNotExistForThisUser = "TASK DOESN'T EXISTS FOR THIS USER";
        public const string TaskSuccessfullyCreated = "TASK SUCCESSFULLY CREATED";
        public const string TaskSuccessfullyUpdated = "TASK SUCCESSFULLY UPDATED";
        public const string TaskExistWithTitle = "TASK WITH THIS TITLE ALREADY EXISTS";
        public const string TaskSuccessfullyDeleted = "TASK SUCCESSFULLY DELETED";
        public const string TaskAlreadyAssignedToAProject = "TASK ALREADY ASSIGNED TO A PROJECT";
        public const string TaskSuccessfullyAssignedToAProject = "SUCCESSFULLY ASSIGNED TASK TO A PROJECT";
        public const string TaskDoesNotHaveProject = "THIS PROJECT DOESN'T HAVE THIS TASK ";
        public const string TaskSuccessfullyRemovedFromAProject = "SUCCESSFULLY REMOVED TASK FROM A PROJECT";

        public const string ProjectDoesNotExist = "PROJECT DOESN'T EXISTS";
        public const string ProjectSuccessfullyCreated = "PROJECT SUCCESSFULLY CREATED";
        public const string ProjectSuccessfullyUpdated = "PROJECT SUCCESSFULLY UPDATED";
        public const string ProjectWithNameExist = "PROJECT WITH THAT NAME ALREADY EXIST";
        public const string ProjectSuccessfullyDeleted = "PROJECT SUCCESSFULLY DELETED";


        public const string NotificationDoesNotExist = "NOTIFICATION DOESN'T EXISTS";
        public const string NotificationSuccessfullyCreated = "NOTIFICATION SUCCESSFULLY CREATED";
        public const string NotificationSuccessfullyUpdated = "NOTIFICATION SUCCESSFULLY UPDATED";
        public const string NotificationSuccessfullyDeleted = "NOTIFICATION SUCCESSFULLY DELETED";
    }
}
