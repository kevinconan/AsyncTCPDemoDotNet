using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsyncTCPDemo.Models
{
    /// <summary>
    /// 通讯包
    /// </summary>
    [Serializable]
    public class Message
    {
        /// <summary>
        /// 内容
        /// </summary>
        public object Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; private set; }

        public Message()
        {
            CreateTime = DateTime.Now;
        }
    }
}
