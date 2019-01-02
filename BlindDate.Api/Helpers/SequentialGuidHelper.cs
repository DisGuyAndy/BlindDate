using System;
using System.Runtime.InteropServices;
using System.Text;

namespace BlindDate.Api.Helpers
{
    public class SequentialGuidHelper
    {
        [DllImport("rpcrt4.dll", SetLastError = true)]
        static extern int UuidCreateSequential(out Guid guid);

        public static Guid CreateSequentialGuid()
        {
            const int RPC_S_OK = 0;

            Guid guid;
            int result = UuidCreateSequential(out guid);
            if (result == RPC_S_OK)
                return guid;
            else
                return Guid.NewGuid();
        }

        public string GenerateOTP()
        {
            string NewOtp = "";
            Random rnd = new Random();
            int intOTP = rnd.Next(0, 9999);
            var otp = intOTP.ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append(otp);
            for (int i = 4 - otp.Length; i > 0; i--)
            {
                sb.Insert(0, "0");
            }

            NewOtp = sb.ToString();
            return NewOtp;
        }
    }
}