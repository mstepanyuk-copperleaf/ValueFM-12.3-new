using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

public class S3ScreenshotSender
{
	protected const string bucketName = "valueframework-test-screenshots";
    protected static readonly RegionEndpoint bucketRegion = RegionEndpoint.USWest2;
    protected static readonly string buildNumber = Environment.GetEnvironmentVariable("BUILD_NUMBER") == null ?
        "dev" : Environment.GetEnvironmentVariable("BUILD_NUMBER");
    protected static IAmazonS3 client;

	public S3ScreenshotSender()
	{
		client = new AmazonS3Client(bucketRegion);
    }

	public async Task SaveScreenshot(string screenshotFileLocation, string filename, string testName)
	{
        var bucketPath = bucketName + @"/" + buildNumber;
        var screenshotRequest = new PutObjectRequest
        {
            BucketName = bucketPath,
            Key = filename,
            FilePath = screenshotFileLocation,
            ContentType = "image/png",
            CannedACL = S3CannedACL.PublicRead
        };

        PutObjectResponse screenshotResponse = await client.PutObjectAsync(screenshotRequest);
        Console.WriteLine($"Screenshot for {testName}: http://s3-us-west-2.amazonaws.com/{bucketPath}/{filename}");
    }
}
