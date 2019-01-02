using BlindDate.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.CognitoIdentity;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;
using System.Configuration;

namespace BlindDate.Api.Helpers
{
    public class AWSHelper
    {

        internal static UploadToS3Response UploadImagetoS3(string AwsAccessKeyId, string AwsSecretAccessKey, Byte[] fileByteArray, string fileName, string bucketName)
        {
            var response = new UploadToS3Response() { Success = false };
            try
            {

                var client = new AmazonS3Client(AwsAccessKeyId, AwsSecretAccessKey, Amazon.RegionEndpoint.EUWest1);

                using (MemoryStream fileToUpload = new MemoryStream(fileByteArray))
                {
                    PutObjectRequest request = new PutObjectRequest()
                    {
                        BucketName = bucketName,
                        Key = fileName,
                        InputStream = fileToUpload,
                        ContentType = "image/png"
                    };
                    request.Timeout = TimeSpan.FromSeconds(60);
                    PutObjectResponse response2 = client.PutObject(request);
                }
                response.Success = true;
                response.S3FilePath = $"{bucketName}/{fileName}";
            }
            catch (AmazonS3Exception s3Exception)
            {
                //s3Exception.ToExceptionless().Submit();
                throw;
            }
            catch (Exception ex)
            {
                //ex.ToExceptionless().Submit();
                throw;
            }
            return response;
        }

        internal static AwsDetails GetAwsDetails()
        {
            var awsDetails = new AwsDetails();
            awsDetails.AwsAccessKeyId = ConfigurationManager.AppSettings["s3:AccessKeyID"];
            awsDetails.AwsSecretAccessKey = ConfigurationManager.AppSettings["s3:SecretAccessKey"];
            return awsDetails;
        }
    }
}