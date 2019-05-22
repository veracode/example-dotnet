using ICSharpCode.SharpZipLib.Zip;
using Ionic.Zip;
using Recurly;
using SharpCompress.Archives.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using example_dotnet_utils;

namespace example_dotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            Say.hello("example");
            Console.WriteLine("Hello World!");

	    // Usage of BCrypt.Net API

            // The following API call is meant to demonstrate
            // vulnerable methods (the simplest case).
              
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("my-secret-password");
            BCrypt.Net.BCrypt.Verify("wrong-password", hashedPassword);

            // The next API calls to four libraries are meant to
            // demonstrate update advisor and auto pull request
            // features.
            //
            // The first two are the cases where the update versions
            // are of the same frameworks as the currently-used
            // version.
            // +--------------------+--------------+----------------+-----------+-----------------+----------+
            // | Dependency         | Current      | Current        | Update    | Update          | Breaking |
            // |                    | Version      | Framework(s)   | Version   | Framework(s)    | Update   |
            // +====================+==============+================+===========+=================+==========+
            // | recurly-api-client | 1.8.0        | net35          | 1.11.4    | net35           | No       |
            // +--------------------+--------------+----------------+-----------+-----------------+----------+
            // | DotNetZip          | 1.10.0       | net20          | 1.11.0    | net20           | Yes      |
            // +--------------------+--------------+----------------+-----------+-----------------+----------+
            //
            // The other two are the cases where the update versions
            // have different frameworks than the current versions.
            // +--------------------+--------------+----------------+-----------+-----------------+----------+
            // | Dependency         | Current      | Current        | Update    | Update          | Breaking |
            // |                    | Version      | Framework(s)   | Version   | Framework(s)    | Update   |
            // +====================+==============+================+===========+=================+==========+
            // | SharpCompress      | 0.17.1       | net35, net45,  | 0.21.0    | net35, net45,   | No       |
            // |                    |              | netstandard1.0 |           | netstandard1.0, |          |
            // |                    |              |                |           | netstandard2.0  |          |
            // +--------------------+--------------+----------------+-----------+-----------------+----------+
            // | SharpZipLib        | 1.0.0-alpha1 | netstandard1.3 | 1.0.0-rc1 | net45,          | Yes      |
            // |                    |              |                |           | netstandard2.0  |          |
            // +--------------------+--------------+----------------+-----------+-----------------+----------+

            // Usage of recurly-api-client API

            // This does not cause a breaking update from 1.8.0 to
            // 1.11.4 (safe version).
            FilterCriteria criteria = FilterCriteria.Instance;
            if (criteria.BeginTime != null) {
	        Console.WriteLine("Begin time: " + criteria.BeginTime);
            }

            // Usage of DotNetZip API
      
	    // This is necesssary to prevent DotNetZip complaining
	    // about IBM437 encoding.
	    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
	    
            Console.WriteLine("Unzipping example ZIP file using DotNetZip");

            // From https://dotnetfiddle.net/U1pPeD
            using (Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(@"resources" + Path.DirectorySeparatorChar + "example.zip")) {
                Console.WriteLine("Files found in zip:");
                foreach (Ionic.Zip.ZipEntry e in zip) {
                    Console.WriteLine(e.FileName);
                }
            }

            // Usage of SharpZipLib API

            // This API is vulnerable; see
            // https://github.com/icsharpcode/SharpZipLib/issues/232

            Console.WriteLine("Unzipping example ZIP file using SharpZipLib");
            new FastZip().ExtractZip(@"resources" + Path.DirectorySeparatorChar + "example.zip", @".", null);

            // Usage of SharpCompress API

            // This calls an API method that does not break update
            ZipArchive zipArchive = ZipArchive.Create();
        }
    }
}
