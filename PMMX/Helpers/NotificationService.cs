using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Maya.Helpers
{
    public class NotificationService
    {
        public void SendPushNotification(string dispositivo, string body, string title)
        {
            try
            {
                string applicationID = "AAAAhg63LNk:APA91bEJv45R9JTitpVrfYNvtxMzSSCIvaHoNa5W0rHpxFG1cTddPdYqf0WkUjhCwv0SqRSPNV5lh8stqqeC3r8UlXR_Bs64KNX1V45c252E1HkrbRz8Seb7OZxxtNq0aWh2UmAyx0iF";
                string senderId = "575772503257";

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";

                var data = new
                {
                    to = dispositivo,
                    notification = new
                    {
                        body = body,
                        title = title,
                        sound = "Enabled"

                    }
                };


                var json = JsonConvert.SerializeObject(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        public void SendPushNotificationIOS(string dispositivo, string body, string title)
        {


            try
            {

                string applicationID = "AAAAA_NHRtI:APA91bFVo10OHNAM-8uCCVF-R5kU-rhu28Ac4emNDWYW3dnRKC84qDjlUo0avYyjdtikQBpbN9oHBE9gFHsQjxIv4_s6jVcRlvvvZTVgo0adO56L8bk8mCLQUKhTjSHqqPuNz4QlSD_w";

                string senderId = "16966436562";

                //string deviceId = "er1uHQlgl_4:APA91bElcHqz_IhBhrJZM4DLIxORZcKvGnwdqg750UqyrXvdKR6f09bHECpRKBml7ArrJrVMHkf_tzxnPOQ2gOUVDpfu-T5Dh6wbWXQtNtLvHnJgZfWOR_5et2PfZmlOl_jNlUjJChwu";

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";

                var data = new
                {

                    to = dispositivo,
                    notification = new
                    {
                        body = body,
                        title = title,
                        sound = "Enabled"

                    }


                };


                var json = JsonConvert.SerializeObject(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        public void mensaje(string dispositivo, string body, string title) {
            
        }
    }
}