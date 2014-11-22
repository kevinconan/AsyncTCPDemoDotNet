using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace AsyncTCPDemo.Common
{
    /// <summary>
    /// 实体类序列化工具
    /// </summary>
    public static class ModelSerializationHelper
    {
        private static readonly BinaryFormatter formatter = new BinaryFormatter();

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns>Base64字符串</returns>
        public static string Serialize(object model)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, model);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="base64">Base64字符串</param>
        /// <returns>实体对象</returns>
        public static T Deserialize<T>(string base64)
        {
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(base64)))
            {
                return (T)formatter.Deserialize(ms);
            }
        }
    }
}
