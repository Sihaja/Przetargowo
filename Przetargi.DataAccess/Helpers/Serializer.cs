using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Xml.Serialization;
using Przetargi.DataAccess.Models.Users;

namespace Przetargi.DataAccess.Helpers
{
    public static class Serializer
    {
        public static string Serialize<T>(T o)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(User), new[] { typeof(TenderOwnerUser), typeof(TenderAttendeeUser) });
                serializer.Serialize(ms, o);

                ms.Seek(0, SeekOrigin.Begin);
                using (StreamReader sr = new StreamReader(ms))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        public static T Deserialize<T>(string s)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(s)))
            {
                ms.Seek(0, SeekOrigin.Begin);

                var serializer = new XmlSerializer(typeof(User), new[] { typeof(TenderOwnerUser), typeof(TenderAttendeeUser) });
                return (T)serializer.Deserialize(ms);
            }
        }
    }
}
