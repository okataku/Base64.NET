using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace base64
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length > 0 && args[0] == "encode")
                {
                    if (args.Length == 4 && args[1] == "-t")
                    {
                        var enc = Encoding.GetEncoding(args[2]);
                        var txt = args[3];
                        Console.WriteLine(EncodeFromText(txt, enc));
                    }
                    else if (args.Length == 3 && args[1] == "-f")
                    {
                        Console.WriteLine(EncodeFromFile(args[2]));
                    }
                    else
                    {
                        ShowUsage();
                    }
                }
                else if (args.Length > 0 && args[0] == "decode")
                {
                    if (args.Length == 4 && args[1] == "-t")
                    {
                        var enc = Encoding.GetEncoding(args[2]);
                        var b64 = args[3];
                        Console.WriteLine(DecodeToText(b64, enc));
                    }
                    else if (args.Length == 4 && args[1] == "-f")
                    {
                        DecodeToFile(args[2], args[3]);
                        Console.WriteLine("Decode to {0}", args[3]);
                    }
                    else
                    {
                        ShowUsage();
                    }
                }
                else
                {
                    ShowUsage();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
            }
        }

        static string EncodeFromText(string text, Encoding encoding)
        {
            return Convert.ToBase64String(encoding.GetBytes(text));
        }

        static string EncodeFromFile(string path)
        {
            var base64 = "";
            using (var fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            using (var ms = new System.IO.MemoryStream())
            {
                var buf = new byte[1024];
                var len = 0;
                do
                {
                    len = fs.Read(buf, 0, buf.Length);
                    ms.Write(buf, 0, len);
                } while (len > 0);
                base64 = Convert.ToBase64String(ms.ToArray());
            }
            return base64;
        }

        static void DecodeToFile(string src, string dst)
        {
            byte[] bin = null;
            using (var fs = new System.IO.StreamReader(src))
            {
                bin = Convert.FromBase64String(fs.ReadToEnd());
            }

            using (var fs = new System.IO.FileStream(dst, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            using (var ms = new System.IO.MemoryStream(bin))
            {
                var buf = new byte[1024];
                var len = 0;
                do
                {
                    len = ms.Read(buf, 0, buf.Length);
                    fs.Write(buf, 0, len);
                } while (len > 0);
            }
        }

        static string DecodeToText(string base64, Encoding encoding)
        {
            return encoding.GetString(Convert.FromBase64String(base64));
        }

        static void ShowUsage()
        {
            Console.WriteLine("encode -t charset text");
            Console.WriteLine("encode -f path/to/file");
            Console.WriteLine("decode -t charset base64");
            Console.WriteLine("decode -f (src)path/to/file (dst)path/to/file");
        }
    }
}
