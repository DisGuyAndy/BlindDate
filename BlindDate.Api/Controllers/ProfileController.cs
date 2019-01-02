using BlindDate.Api.Data;
using BlindDate.Api.Data.Models;
using BlindDate.Api.Helpers;
using BlindDate.Api.Providers;
using BlindDate.Common.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BlindDate.Api.Controllers
{
    [RoutePrefix("profile")]
    public class ProfileController : ApiController
    {
        ApplicationDbContext DbContext = new ApplicationDbContext();
        UserManager userManager = new UserManager();
        MailHelper mailHelper = new MailHelper();
        SequentialGuidHelper sequentialGuidHelper = new SequentialGuidHelper();

        [Route("registeruser")]
        [HttpPost]
        public async Task<UserDetailsExistResponse> RegisterUser([FromBody]RegistrationRequest registrationRequest)
        {
            var res = new UserDetailsExistResponse() { Success = false };
            try
            {
                if (!(userManager.IsValidEmail(registrationRequest.Email)))
                {
                    res.Success = false;
                    res.Message = "Email not Valid";
                    return res;
                }

                if (bool.Parse(ConfigurationManager.AppSettings["DummyProfileCheck"]))
                {
                    if (registrationRequest.Email.Contains("@mailanator") || registrationRequest.Email.Contains("@yopmail"))
                    {
                        res.Success = false;
                        res.Message = "Cannot use Temporary emails";
                        return res;
                    }
                }

                var CheckifUserExists = await userManager.CheckIfUserExistsDetailsExistsAsync(registrationRequest);
                if (CheckifUserExists.Success == false)
                {
                    res = CheckifUserExists;
                    return res;
                }
                var emailOtp = sequentialGuidHelper.GenerateOTP();
                var msisdnOtp = sequentialGuidHelper.GenerateOTP();
                if (string.IsNullOrEmpty(emailOtp) || string.IsNullOrEmpty(msisdnOtp))
                {
                    res.Message = "Error generating OTP. Please try to register again.";
                    return res;
                }
                var profile = DbContext.ApplicationUserProfiles.Add(new ApplicationUserProfile()
                {
                    DisplayUsername = registrationRequest.DisplayUsername,
                    DisplayUsernameId = registrationRequest.DisplayUsernameId,
                    Id = SequentialGuidHelper.CreateSequentialGuid().ToString(),
                    DateCreated = DateTimeOffset.Now,
                    UserType = ProfileType.Trial,
                    MSISDN = registrationRequest.MSISDN,
                    FirstName = registrationRequest.FirstName,
                    LastName = registrationRequest.LastName,
                    Email = registrationRequest.Email,
                    ContactNumberValidated = false,
                    RegistrationStatus = Models.RegistrationStatus.Unvalidated,
                    EmailOneTimePin = emailOtp,
                    MSISDNOneTimePin = msisdnOtp
                });

                await DbContext.SaveChangesAsync();
                mailHelper.SendOTPMail(registrationRequest.Email, registrationRequest.DisplayUsername, profile.EmailOneTimePin);

                res.Success = true;
                res.Message = $"User created with id: {profile.Id}";
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
            }
            return res;
        }

        [Route("validate/otp/for/profileid")]
        [HttpPost]
        public async Task<ResponseBase> ValidateEmailOtpForProfileId([FromBody] OtpValidationRequest otpValidationRequest)
        {
            var res = new ResponseBase() { Success = false };

            try
            {
                if (otpValidationRequest.ProfileId == null)
                {
                    res.Message = "Invalid Profile";
                    return res;
                }
                if (otpValidationRequest.Otp == null)
                {
                    res.Message = "No Otp Given";
                    return res;
                }
                var profile = await DbContext.ApplicationUserProfiles.FirstOrDefaultAsync(x => x.Id == otpValidationRequest.ProfileId);

                if (profile == null)
                {
                    res.Message = "Could not find Profile";
                    return res;
                }

                if (profile.EmailOneTimePin == otpValidationRequest.Otp)
                {
                    profile.EmailValidated = true;
                    await DbContext.SaveChangesAsync();
                    res.Success = true;
                }
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                throw;
            }

            return res;
        }

        [Route("upload/profile/image")]
        [HttpPost]
        public async Task<ResponseBase> UploadProfileImage(UploadImageRequest uploadImageRequest)
        {
            //blinddateapi/UserImages
            var res = new ResponseBase() { Success = false };
            try
            {
                var awsdetails = AWSHelper.GetAwsDetails();
                var imageBytes = Convert.FromBase64String(uploadImageRequest.Base64);
                var filename = $"{uploadImageRequest.ProfileId}-{uploadImageRequest.ImageSequenceId}.png";
                var uploadResponse = AWSHelper.UploadImagetoS3(awsdetails.AwsAccessKeyId, awsdetails.AwsSecretAccessKey, imageBytes, filename, "blinddateapi/UserImages");
                if (uploadResponse.Success)
                {
                    var saveResponse = await userManager.SaveImage(uploadResponse.S3FilePath, uploadImageRequest.ImageSequenceId, uploadImageRequest.ProfileId, uploadImageRequest.IsMainProfileImage);
                    if (saveResponse.Success)
                    {
                        res.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                throw;
            }
            return res;
        }

        //[Route("get/images")]
        //[HttpGet]
        //public async Task<List<string>> GetAllImagesForProfile(string profileId)
        //{

        //}
    }
}