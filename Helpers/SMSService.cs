using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConvertIPAddressToLocation.Helpers
{
    public class SMSService
    {
        public void sendSMS()
        {

            AmazonSimpleNotificationServiceClient snsClient = new AmazonSimpleNotificationServiceClient("AKIAQ67HQRIEW6GN7B5P", "RPROUsDiJMFJYFI4QhjJ1S8k8KGC5QV5gniI+TWj", Amazon.RegionEndpoint.USEast2);
            PublishRequest pubRequest = new PublishRequest();
            pubRequest.Message = "My SMS message";
            pubRequest.PhoneNumber = "9172755343";
            // add optional MessageAttributes, for example:
            //   pubRequest.MessageAttributes.Add("AWS.SNS.SMS.SenderID", new MessageAttributeValue
            //      { StringValue = "SenderId", DataType = "String" });
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            var pubResponse = snsClient.PublishAsync(pubRequest, token);
        

        }
    }
}
