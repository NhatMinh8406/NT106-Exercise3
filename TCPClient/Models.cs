using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Windows.Forms;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace TCPClient
{
    public class Request
    {
        public string Action { get; set; }
        public object Data { get; set; }
    }

    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public JObject Data { get; set; }
    }
}
