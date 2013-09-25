/* 
 * Copyright 2008-2013 Mohawk College of Applied Arts and Technology
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you 
 * may not use this file except in compliance with the License. You may 
 * obtain a copy of the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations under 
 * the License.
 * 
 * User: Justin Fyfe
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using MohawkCollege.EHR.gpmr.COR;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.Deki.Article;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.Deki.TemplateCore;
using System.IO;
using System.Collections.Specialized;
using System.Xml.Serialization;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.Deki.Communications;
using System.Threading;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Deki
{
    /// <summary>
    /// Render MIF files into HTML documentation and post them to a deki wiki system
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Renderer"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Deki")]
    public class DekiRenderer : RendererBase
    {

        private DocTemplate docTemplate;

        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        string[][] helpText = new string[][] 
        {
            new string[] { "--deki-url","The url of the deki server" } ,
            new string[] { "--deki-path","The path of the uppermost parent. This is where the TOC of the \r\n\t\tdocumentation will be placed" } ,
            new string[] { "--deki-user","The name of the user account to post to the wiki with" } ,
            new string[] { "--deki-password", "The password to use to log into the deki server" } ,
            new string[] { "--deki-htmlpath","If set, enables saving the contents of each page to a \r\n\t\tlocal directory" },
            new string[] { "--deki-template", "The name of the template to use" }, 
            new string[] { "--deki-nopub", "Generate only HTML files and quit" }, 
            new string[] { "--deki-nopub-root", "The root of the HTML files when not publishing" }, 
            new string[] { "--deki-prop","Properties for deki pages in key=value format" }
        };
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override string Name
        {
            get { return "Deki Wiki Renderer"; }
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override string Identifier
        {
            get { return "DEKI"; }
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override int ExecutionOrder
        {
            get { return 99; }
        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Context"></param>
        public override void Init(Pipeline Context)
        {
            base.Init(Context);
            System.Diagnostics.Trace.WriteLine("Mohawk College DEKI Article / HTML Renderer\r\nCopyright(C) 2008-2013 Mohawk College of Applied Arts and Technology", "information");

        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "articleRep"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public override void Execute()
        {

            if (!(hostContext.Data["EnabledRenderers"] as StringCollection).Contains(this.Identifier)) return;
            
            // Get parameters
            Dictionary<string, StringCollection> parameters = hostContext.Data["CommandParameters"] as Dictionary<string, StringCollection>;
            
            System.Diagnostics.Trace.WriteLine("\r\nStarting DEKI Renderer", "information");

            // Get our repository ready
            ClassRepository classRep = hostContext.Data["SourceCR"] as ClassRepository;
            ArticleCollection articleRep = new ArticleCollection();

            Stream tplS = null;

            // Load template
            try
            {
                tplS = parameters.ContainsKey("deki-template") && parameters["deki-template"][0] != "default" ? 
                            File.OpenRead(parameters["deki-template"][0]) as Stream : 
                            parameters.ContainsKey("deki-nopub") ? new MemoryStream(System.Text.Encoding.ASCII.GetBytes(Templates.__Default_nopub)) as Stream
                            : new MemoryStream(System.Text.Encoding.ASCII.GetBytes(Templates.Clean)) as Stream;
                XmlSerializer xsz = new XmlSerializer(typeof(DocTemplate));
                docTemplate = xsz.Deserialize(tplS) as DocTemplate;

                ArticleCollection acoll = docTemplate.Process(classRep);

                // Save the article collection to the hard disk
                if (parameters.ContainsKey("deki-htmlpath"))
                {
                    System.Diagnostics.Trace.Write(string.Format("Saving generated files to '{0}'...", parameters["deki-htmlpath"][0]), "debug");

                    foreach (Article.Article art in acoll)
                        PersistArticle(art, parameters["deki-htmlpath"][0]);

                    System.Diagnostics.Trace.WriteLine("Done", "debug");
                }

                // Publish
                if(!parameters.ContainsKey("deki-nopub"))
                    Publish(acoll);

            }
            catch (Exception e)
            {
                throw new InvalidDataException("Could not apply the specified DEKI article template", e);
            }
            finally
            {
                if(tplS != null) tplS.Close();
            }

        }

        /// <summary>
        /// Publish to deki-wiki
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        private void Publish(ArticleCollection acoll)
        {

            Dictionary<string, StringCollection> parameters = hostContext.Data["CommandParameters"] as Dictionary<string, StringCollection>;
            string url, path, user, password;
            Dictionary<String, String> properties = new Dictionary<string, string>();

            try
            {
                url = parameters["deki-url"][0];
                path = parameters["deki-path"][0];
                user = parameters["deki-user"][0];
                password = parameters.ContainsKey("deki-password") ? parameters["deki-password"][0] : null;

                if (password == null)
                {
                    Console.Write(string.Format("deki: {0}'s password:", user));
                    ConsoleColor oldFg = Console.ForegroundColor;
                    Console.ForegroundColor = Console.BackgroundColor;
                    password = Console.ReadLine();
                    Console.ForegroundColor = oldFg;
                }

                // If we can print this line, we have enough info to connect and publish
                System.Diagnostics.Trace.WriteLine(string.Format("Publishing to deki server:\r\n\tServer:{0}\r\n\tRoot:{1}\r\n\tUser:{2}\r\n\tPassword:{3}",
                    url, path, user, new string('*', password.Length)), "information");

                if (parameters.ContainsKey("deki-prop"))
                    foreach (string kv in parameters["deki-prop"])
                    {
                        string[] kvData = kv.Split('=');
                        properties.Add(kvData[0], kvData.Length == 2 ? kvData[1] : "");
                    }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Can't publish, not enough information provided");
            }

            System.Diagnostics.Trace.Write("Requesting a session...", "debug");
            DekiCommunicator dc = new DekiCommunicator();
            dc.Address = url;
            bool ignoreSSL = false;

            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(delegate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    if(sslPolicyErrors == SslPolicyErrors.None || ignoreSSL)
                        return true;
                    Console.Write("deki: {0}\r\nWould you like to ignore this and attempt a re-connect? (y/n)", sslPolicyErrors);
                    ignoreSSL = (Console.ReadLine().StartsWith("y"));
                    return ignoreSSL;
                });

            // Request a session
            for (int i = 0; i < 2; i++)
            {
                dc.Authenticate(user, password);
                if (dc.State == DekiCommunicator.DekiCommState.Authenticated)
                {
                    System.Diagnostics.Trace.WriteLine("ok", "debug");
                    break;
                }
                else
                {
                    Console.Write("deki: Could not get a session ({0})\r\n", dc.State);
                    Console.Write(string.Format("deki: {0}'s password:", user));
                    ConsoleColor oldFg = Console.ForegroundColor;
                    Console.ForegroundColor = Console.BackgroundColor;
                    password = Console.ReadLine();
                    Console.ForegroundColor = oldFg;
                }
            }

            System.Diagnostics.Trace.Write("Publishing Documents.", "debug");
            try
            {
                foreach (Article.Article a in acoll)
                {
                    PublishArticle(dc, a, path, properties);
                }
                System.Diagnostics.Trace.WriteLine("Success", "debug");
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine("Fail", "debug");
                System.Diagnostics.Trace.WriteLine("Could not publish documents", "error");
                System.Diagnostics.Trace.WriteLine(e.ToString(), "error");
                //throw new Exception("Could not publish documents", e);
            }
        }
        //DOC: Documentation Required
        /// <summary>
        /// Publish an article and its children
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="a">The article to publish</param>
        /// <param name="path">The path to publish to</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        private void PublishArticle(DekiCommunicator dc, MohawkCollege.EHR.gpmr.Pipeline.Renderer.Deki.Article.Article a, string path, Dictionary<string,String> properties)
        {
            
            // Clean article to remove .html
            string Html = a.Content.Replace(".deki\"", "\"");
            Html = Html.Replace(".deki#", "#");
            Html = Html.Replace("\"~/", string.Format("\"{0}/", path));
            // Publish away!
            path = path.Replace("/", "%2f");
            // Try twice to publish
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    dc.Publish(Html, string.Format("{0}%2f{1}", path, a.Url.Replace("/", "%2f")), a.Title);
                    foreach (var kv in properties)
                    {
                        try
                        {
                            dc.PublishProperty(kv.Key, kv.Value, string.Format("{0}%2f{1}", path, a.Url.Replace("/", "%2f")));
                        }
                        catch { } // Don't care if properties are not published
                    }

                    // Throttling is required to allow time for the MySQL server in deki to catch-up with the
                    // GPMR process. This is needed because the MySQL server on Deki-Hayes can become bogged
                    // down when submitting massive amounts of content.
                    Thread.Sleep(250);
                    break;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Trace.WriteLine(string.Format("{0}, retrying publish operation for {1}", e.Message, a.Title), "warn");
                }
            }
            System.Diagnostics.Trace.Write(".", "information");

            if (a.Children != null)
                foreach (Article.Article ca in a.Children) 
                    PublishArticle(dc, ca, path, properties);
        }


        /// <summary>
        /// Persist an article to the specified path
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        private void PersistArticle(MohawkCollege.EHR.gpmr.Pipeline.Renderer.Deki.Article.Article art, string p)
        {

            Dictionary<string, StringCollection> parameters = hostContext.Data["CommandParameters"] as Dictionary<string, StringCollection>;

            string path = parameters.ContainsKey("deki-nopub-root") ? parameters["deki-nopub-root"][0] : p;
            // Step 1. Check that dir exists
            if (!Directory.Exists(p))
                Directory.CreateDirectory(p);
            if (!Directory.Exists(Path.GetDirectoryName(Path.Combine(p, art.Url.Replace("/","\\")))))
                Directory.CreateDirectory(Path.GetDirectoryName(Path.Combine(p, art.Url.Replace("/", "\\"))));

            // Step 2. Prepare a writer, etc...
            TextWriter tw = null;
            try
            {
                tw = File.CreateText(Path.ChangeExtension(Path.Combine(p, art.Url), ".html"));
                tw.Write(string.Format("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\"en\" lang=\"en\"><head><title>{1}</title><style type=\"text/css\"><!--{2}--></style></head><body><h1>{1}</h1>{0}</body></html>", art.Content.Replace("href=\"~", string.Format("href=\"file://{0}", path)).Replace(".deki\"",".html\"").Replace(".deki#",".html#"), art.Title, docTemplate.Style));
                tw.Close();

                System.Diagnostics.Trace.Write(".", "debug");

                // Children... create a dir and store em
                if (art.Children != null)
                    foreach (Article.Article child in art.Children)
                        PersistArticle(child, p);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (tw != null) tw.Close();
            }

        }
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        public override string Help
        {
            get 
            {
                StringBuilder helpString = new StringBuilder();
                foreach (string[] helpData in helpText)
                    helpString.AppendFormat("{0}\t{1}\r\n", helpData[0], helpData[1]);

                helpString.Append("\r\nExample:\r\ngpmr -s mif/*.* -r DEKI -o .\\output --deki-url=http://wikiserver.com --deki-user=user --deki-password=password --deki-path=/ExampleTOC\r\n");

                return helpString.ToString();
            }
        }
    }
}
