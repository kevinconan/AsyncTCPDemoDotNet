using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsyncTCPDemo.Models
{
    /// <summary>
    /// 传输用模型
    /// </summary>
    [Serializable]
    public class People
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 问候
        /// </summary>
        /// <returns></returns>
        public string SayHello()
        {
            return string.Format("Hello! My name is {0}.", Name);
        }

        /// <summary>
        /// 询问年龄
        /// </summary>
        /// <returns></returns>
        public string HowOldAreYou()
        {
            return string.Format("I'm {0} years old", Age);
        }
    }
}
