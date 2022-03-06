using System;
using System.IO;
using LibOrbisPkg.PKG;
using LibOrbisPkg.SFO;
using LibOrbisPkg.Util;
using Newtonsoft.Json;

namespace pkginfo
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: pkginfo.exe \"path/to/pkg\" \"developer1,developer2\" \"http://server.com/pkgs/\" \"category1,category2\" \"Description,line2,line3\" \"path/to/pkgs.json\"");
            }
            else
            {
                string pkgpath = args[0];
                string[] devs = args[1].Split(',');
                string url = args[2];
                string[] categories = args[3].Split(',');
                string[] description = args[4].Split(',');
                jsonentry new_entry = new jsonentry();
                jsonentry[] json;
                string jsonoutput;

                if (args.Length < 6)
                {
                    Console.WriteLine("No existing JSON File specified! Creating new one");
                    json = new jsonentry[1];
                    jsonoutput = @"pkgs.json";
                    File.WriteAllText(jsonoutput, "[]");
                }
                else
                {
                    json = JsonConvert.DeserializeObject<jsonentry[]>(File.ReadAllText(args[5]));
                    Array.Resize(ref json, json.Length + 1);
                    jsonoutput = args[5];
                }

                extract(pkgpath, EntryId.PARAM_SFO, "param.sfo");
                new_entry.titleid = sfo_get("param.sfo", "TITLE_ID");
                new_entry.name = sfo_get("param.sfo", "TITLE");
                new_entry.contentid = sfo_get("param.sfo", "CONTENT_ID");
                new_entry.devs = devs;
                new_entry.description = description;
                new_entry.categories = categories;
                new_entry.version = sfo_get("param.sfo", "VERSION");
                new_entry.type = sfo_get("param.sfo", "CATEGORY");
                switch (sfo_get("param.sfo", "CATEGORY"))
                {
                    case "gd":
                        new_entry.type = "game";
                        break;
                    case "ac":
                        new_entry.type = "additional content";
                        break;
                    case "gp":
                        new_entry.type = "patch";
                        break;
                    default:
                        new_entry.type = "game";
                        break;
                }

                FileInfo info = new FileInfo(pkgpath);
                new_entry.size = Convert.ToString(Math.Ceiling(Convert.ToDecimal(info.Length / 1024) / 1024)) + "MB";
                new_entry.links = new string[] { url + info.Name };

                extract(pkgpath, EntryId.ICON0_PNG, info.Name + ".icon0.png");
                new_entry.icon = url + info.Name + ".icon0.png";

                json[json.Length - 1] = new_entry;
                Console.WriteLine(JsonConvert.SerializeObject(json[json.Length - 1], Formatting.Indented));
                File.WriteAllText(jsonoutput, JsonConvert.SerializeObject(json, Formatting.Indented));
            }
        }

        public static void extract(string pkgPath,EntryId entry,string outPath)
        {

            Pkg pkg;
            using (var s = File.OpenRead(pkgPath))
            {
                pkg = new PkgReader(s).ReadPkg();

                foreach (var meta in pkg.Metas.Metas)
                {
                    if (meta.id == entry)
                    {
                        using (var outFile = File.Create(outPath))
                        {
                            outFile.SetLength(meta.DataSize);
                            var totalEntrySize = meta.Encrypted ? (meta.DataSize + 15) & ~15 : meta.DataSize;
                            new SubStream(s, meta.DataOffset, totalEntrySize).CopyTo(outFile);
                        }
                        break;
                    }
                }
            }
        }
        public static string sfo_get(string path, string Name)
        {
            using (var f = File.OpenRead(path))
            {
                var sfo = ParamSfo.FromStream(f);
                foreach (var x in sfo.Values)
                {
                    if (Name == x.Name)
                    {
                        return x.ToString();
                    }
                }
                return "Not found!";
            }
        }
    }
}
