using BlindDate.Api.Data;
using BlindDate.Api.Data.Models;
using BlindDate.Common.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BlindDate.Api.Providers
{
    public class UserManager
    {
        ApplicationDbContext DbContext = new ApplicationDbContext();

        public async Task<UserDetailsExistResponse> CheckIfUserExistsDetailsExistsAsync(RegistrationRequest registrationRequest)
        {
            var doesUserExist = new UserDetailsExistResponse() { Success = true, EmailExists = false, IdNumberExists = false, MSISDNExists = false };
            var user = await GetUserByEmailAddressAsync(registrationRequest.Email);
            if (user != null)
            {
                doesUserExist.EmailExists = true;
                doesUserExist.Success = false;
            }

            var ContactNumberUser = await GetUserByContactNumberAsync(registrationRequest.MSISDN);
            if (ContactNumberUser != null)
            {
                doesUserExist.MSISDNExists = true;
                doesUserExist.Success = false;
            }

            var UsernameUser = await GetUserByUsernameAsync(registrationRequest.DisplayUsername);
            if (UsernameUser != null)
            {
                doesUserExist.EmailExists = true;
                doesUserExist.Success = false;
            }

            var UsernameIdUser = await GetUserByUsernameIdAsync(registrationRequest.DisplayUsername);
            if (UsernameIdUser != null)
            {
                doesUserExist.EmailExists = true;
                doesUserExist.Success = false;
            }
            return doesUserExist;
        }


        public async Task<ApplicationUserProfile> GetUserByUsernameIdAsync(string UserNameId)
        {
            return await DbContext.ApplicationUserProfiles.FirstOrDefaultAsync(x => x.DisplayUsernameId == UserNameId);
        }
        public async Task<ApplicationUserProfile> GetUserByProfileIdAsync(string profileId)
        {
            return await DbContext.ApplicationUserProfiles.FirstOrDefaultAsync(x => x.Id == profileId);
        }

        public async Task<ApplicationUserProfile> GetUserByEmailAddressAsync(string emailAddress)
        {
            return await DbContext.ApplicationUserProfiles.FirstOrDefaultAsync(x => x.Email.Equals(emailAddress, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<ApplicationUserProfile> GetUserByContactNumberAsync(string contactNumber)
        {
            return await DbContext.ApplicationUserProfiles.FirstOrDefaultAsync(x => x.MSISDN.Equals(contactNumber, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<ApplicationUserProfile> GetUserByUsernameAsync(string displayUsername)
        {
            return await DbContext.ApplicationUserProfiles.FirstOrDefaultAsync(x => x.DisplayUsername.ToLower().Contains(displayUsername.ToLower()));
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ResponseBase> SaveImage(string s3ImageLocation, int sequenceId, string profileId, bool isMainProfileImage)
        {
            var res = new ResponseBase() { Success = false };
            var newImage = new ProfileImage()
            {
                IsMainProfileImage = isMainProfileImage,
                ProfileId = profileId,
                S3ImageLocation = s3ImageLocation,
                SequenceId = sequenceId
            };

            DbContext.ProfileImages.Add(newImage);
            await DbContext.SaveChangesAsync();
            res.Success = true;

            return res;
        }
    }
}