/* 
 * Copyright 2008-2012 Mohawk College of Applied Arts and Technology
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
 * User: $user$
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Deki.Communications
{
    /// <summary>
    /// Class is responsible for pushing articles and negotiating with deki server
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Deki")]
    public class DekiCommunicator
    {

        const int BUFFER = 1024;
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Deki"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Comm")]
        public enum DekiCommState
        {
            //DOC: Documentation Required
            /// <summary>
            /// 
            /// </summary>
            Closed,
            //DOC: Documentation Required
            /// <summary>
            /// 
            /// </summary>
            Open,
            //DOC: Documentation Required
            /// <summary>
            /// 
            /// </summary>
            Authenticated,
            /// <summary>
            /// 
            /// </summary>
            TrustFailure
        }

        
        private string address;
        private string authToken;
        private DekiCommState state = DekiCommState.Closed;

        /// <summary>
        /// The current state
        /// </summary>
        public DekiCommState State
        {
            get { return state; }
            set { state = value; }
        }
	
        /// <summary>
        /// The authorization token
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public string AuthToken
        {
            get { return authToken; }
            private set { authToken = value; }
        }
	
        /// <summary>
        /// The URL of the server
        /// </summary>
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        /// <summary>
        /// Publish an article to the wiki
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2234:PassSystemUriObjectsInsteadOfStrings"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Path"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Html"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object,System.Object)")]
        public void Publish(string HtmlText, string Path, string Title)
        {

            try
            {
                System.Net.ServicePointManager.Expect100Continue = false;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/@api/deki/pages/={1}/contents?authtoken={2}&edittime={3}&title={4}",
                        Address, System.Web.HttpUtility.UrlEncode(Path), AuthToken, DateTime.Now, System.Web.HttpUtility.UrlEncode(Title)));
                request.ProtocolVersion = new Version(1, 0); // HACK: For some reason some apache servers don't like
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = HtmlText.Length;

                Stream tw = null;
                TextReader tr = null;
                try
                {
                    tw = request.GetRequestStream();

                    string output = HtmlText;

                    //if (Path.Contains("RIM")) System.Diagnostics.Debugger.Break();

                    while (output.Length > BUFFER)
                    {
                        byte[] buffer = new byte[BUFFER];
                        buffer = System.Text.Encoding.ASCII.GetBytes(output.Substring(0, BUFFER));
                        output = output.Substring(BUFFER);
                        tw.Write(buffer, 0, BUFFER);
                    }

                    byte[] last = new byte[output.Length];
                    last = System.Text.Encoding.ASCII.GetBytes(output);
                    tw.Write(last, 0, output.Length);

                    tw.Flush();
                    //request.GetRequestStream().Flush();

                    tw.Close();

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    tr = new StreamReader(response.GetResponseStream());
                    string responseContent = tr.ReadToEnd();
                }
                finally
                {
                    if (tw != null) tw.Close();
                    if (tr != null) tr.Close();
                }
            }
            catch (WebException e)
            {
                
                HttpWebResponse response = (HttpWebResponse)e.Response;
                TextReader tr = new StreamReader(response.GetResponseStream());
                string responseContent = tr.ReadToEnd();
                System.Diagnostics.Debug.Print(responseContent);
                throw e;
            }
            catch (Exception e)
            {
                throw new Exception("Publishing error", e);
            }
        }

        /// <summary>
        /// Authenticate against the deki server
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2234:PassSystemUriObjectsInsteadOfStrings"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "User"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Password"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public void Authenticate(string UserName, string Password)
        {
            TextReader tw = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/@api/deki/users/authenticate", Address));
                request.Credentials = new NetworkCredential(UserName, Password);
                request.Method = "GET";
                
                // Now get the response
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                tw = new StreamReader(response.GetResponseStream());

                // Read auth token
                this.authToken = tw.ReadToEnd();
                state = DekiCommState.Authenticated;
            }
            catch (WebException e)
            {

                if (e.Status == WebExceptionStatus.TrustFailure)
                    state = DekiCommState.TrustFailure;
            }
            catch (Exception e)
            {
                throw new Exception("Authentication has failed", e);
            }
            finally
            {
                if (tw != null) tw.Close();
            }
        }


        /// <summary>
        /// Publish a property
        /// </summary>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        /// <param name="path"></param>
        public void PublishProperty(string propertyName, string propertyValue, string path)
        {
            try
            {
                System.Net.ServicePointManager.Expect100Continue = false;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/@api/deki/pages/={1}/properties?authtoken={2}",
                        Address, System.Web.HttpUtility.UrlEncode(path), AuthToken, DateTime.Now));
                request.ProtocolVersion = new Version(1, 0); // HACK: For some reason some apache servers don't like
                request.Method = "POST";
                request.ContentType = "text/plain";
                request.ContentLength = propertyValue.Length;
                request.Headers.Add("Slug", propertyName);
                

                Stream tw = null;
                TextReader tr = null;
                try
                {
                    tw = request.GetRequestStream();

                    string output = propertyValue;

                    //if (Path.Contains("RIM")) System.Diagnostics.Debugger.Break();

                    while (output.Length > BUFFER)
                    {
                        byte[] buffer = new byte[BUFFER];
                        buffer = System.Text.Encoding.ASCII.GetBytes(output.Substring(0, BUFFER));
                        output = output.Substring(BUFFER);
                        tw.Write(buffer, 0, BUFFER);
                    }

                    byte[] last = new byte[output.Length];
                    last = System.Text.Encoding.ASCII.GetBytes(output);
                    tw.Write(last, 0, output.Length);

                    tw.Flush();
                    //request.GetRequestStream().Flush();

                    tw.Close();

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    tr = new StreamReader(response.GetResponseStream());
                    string responseContent = tr.ReadToEnd();
                }
                finally
                {
                    if (tw != null) tw.Close();
                    if (tr != null) tr.Close();
                }
            }
            catch (WebException e)
            {

                HttpWebResponse response = (HttpWebResponse)e.Response;
                TextReader tr = new StreamReader(response.GetResponseStream());
                string responseContent = tr.ReadToEnd();
                System.Diagnostics.Debug.Print(responseContent);
                throw e;
            }
            catch (Exception e)
            {
                throw new Exception("Publishing error", e);
            }
        }
    }
}