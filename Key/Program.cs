using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Key
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Options:");
            Console.WriteLine("[1]: Get Key!!! ");
            Console.WriteLine("[2]: Try Key!!!");
            int option = Convert.ToInt32(Console.ReadLine());
            var usernamePc = Environment.UserName;
            var userDomain = Environment.UserDomainName;
            var getIp = GetIp();
            var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
            ManagementObjectCollection mbsList = mbs.Get();
            string id = "";
            foreach(ManagementObject mo in mbsList)
            {
                id = mo["ProcessorId"].ToString();
                break;
            }
            string hash = usernamePc + userDomain + getIp + id.ToString();
            string createHash = CreateMD5Hash(hash);
            if (option == 1)
            {
                Console.WriteLine($"Key:[{createHash.ToString()}]");
            }
            else if(option == 2)
            {
                Console.WriteLine("Enter Key: ");
                string key = Console.ReadLine().ToString();
                if (key == createHash)
                {
                    Console.WriteLine("You Entered Key Successfully!!!");
                }
                else if (key != createHash)
                {
                    Console.WriteLine("You Entered Key Wrong!!!");
                }
            }
            Console.ReadLine();
        }
        public static string CreateMD5Hash(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString());
            }
            return sb.ToString();
        }
        public static string GetIp()
        {
            string ip = new WebClient().DownloadString("http://ipv4bot.whatismyipaddress.com/");
            return ip;
        }
    }
}
