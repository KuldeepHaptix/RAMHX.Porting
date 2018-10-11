using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaptiX.RAMHX.eDoctor.Infra
{
    public class ApiResponse
    {
        /// <summary>
        /// Actual payload in success
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Short Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Status Code
        /// </summary>
        public string Status { get; set; }
    }


}
