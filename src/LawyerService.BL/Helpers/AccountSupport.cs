using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Helpers
{
    public static class AccountSupport
    {
        public static object GetFieldValue<T>(T obj, string fieldName)
        {
            return obj.GetType().GetProperty(fieldName).GetValue(obj);
        }

        public static void SetFieldValue<T>(ref T obj, string fieldName, object value)
        {
            obj.GetType().GetProperty(fieldName).SetValue(obj, value);
        }


        public static void CheckUserName(string userName, string errorMessage)
        {
            var pattern = new Regex(@"^(?=.*[a-zA-Z])[a-zA-Z0-9]{2,32}$");

            if (string.IsNullOrEmpty(userName) || !pattern.IsMatch(userName))
                throw new Exception(HttpStatusCode.BadRequest+ errorMessage);
        }

        public static void CheckPassword(string password, string errorMessage)
        {
            var pattern = new Regex(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z#$^+=!*()@%&]{8,}$");

            if (string.IsNullOrEmpty(password) || !pattern.IsMatch(password))
                throw new Exception(HttpStatusCode.Forbidden+errorMessage);
        }

        public static async Task<string> PostRequestAsync(string url, string data, string contentType, string token = null)
        {
            WebRequest request = WebRequest.Create(url);

            if (!string.IsNullOrEmpty(token))
                request.Headers.Add("Authorization", "Bearer " + token);

            request.Method = "POST"; // для отправки используется метод Post
                                     // данные для отправки
                                     //string data = "sName=Hello world!";
                                     // преобразуем данные в массив байтов
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
            // устанавливаем тип содержимого - параметр ContentType
            request.ContentType = contentType;
            // Устанавливаем заголовок Content-Length запроса - свойство ContentLength
            request.ContentLength = byteArray.Length;

            //записываем данные в поток запроса
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            string res;
            WebResponse response = await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    res = reader.ReadToEnd();
                }
            }
            response.Close();
            return res;
        }

    }
}
