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
 * User: $user$
 * Date: 01-09-2009
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Deki.Article
{
    /// <summary>
    /// This class represents an article
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1036:OverrideMethodsOnComparableTypes")]
    public class Article : IComparable<Article>
    {
        private string htmlContent;
        private ArticleCollection children = null;
        private Article parent;
        private string title;
        private string @abstract;
        private string m_fileName = null;
        //private static string m_tempDir = null;

        /// <summary>
        /// Identifies style information for the article
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// Provide an abstract data about the contents of the package
        /// </summary>
        public string Abstract
        {
            get { return @abstract; }
            set { @abstract = value; }
        }
        /// <summary>
        /// Represents this article's URL
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        public string Url { get { return string.Format("{0}{1}",
            parent != null ? parent.Url + "/" : "",
            (this.Name ?? this.Title) != null ? (Name ?? Title).Replace(":","_") : null); } }

        /// <summary>
        /// Gets or sets the name of the page
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or set the title of the article
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
	
        /// <summary>
        /// The parent of this article
        /// </summary>
        public Article Parent
        {
            get { return parent; }
            set { parent = value; }
        }
	
        /// <summary>
        /// Child articles
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ArticleCollection Children
        {
            get 
            { 
                return children; 
            }
            set 
            {
                if (children != null)
                {
                    children.ArticleAdded -= children_ArticleAdded;
                    children.ArticleRemoved -= children_ArticleRemoved;
                }

                children = value;
                // Append added event

                if (children != null)
                {
                    children.ArticleAdded += new ArticlePublishedHandler(children_ArticleAdded);
                    children.ArticleRemoved += new ArticlePublishedHandler(children_ArticleRemoved);
                }
            }
        }

        /// <summary>
        /// Content of the article
        /// </summary>
        public string Content
        {
            get 
            {
                if (String.IsNullOrEmpty(this.m_fileName))
                    return null;
                return File.ReadAllText(this.m_fileName);
            }
            set 
            {

                // Create the file if it doesn't exist!
                if (String.IsNullOrEmpty(this.m_fileName))
                {
                    this.m_fileName = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()); ; //Path.Combine(Article.m_tempDir, Guid.NewGuid().ToString());
                    if (File.Exists(this.m_fileName))
                        Debug.WriteLine(String.Format("Warning, file '{0}' already exists!", this.m_fileName));
                }

                File.WriteAllText(this.m_fileName, value);
            }
        }

        /// <summary>
        /// Construct a new instance of article
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1805:DoNotInitializeUnnecessarily")]
        public Article()
        {
            //this.Children = new ArticleCollection();
        }

        /// <summary>
        /// Clear the parent of the published article
        /// </summary>
        void children_ArticleRemoved(Article PublishedArticle, ArticleCollection Repository)
        {
            PublishedArticle.parent = null;
        }

        /// <summary>
        /// Set the parent of the published article
        /// </summary>
        void children_ArticleAdded(Article PublishedArticle, ArticleCollection Repository)
        {
            PublishedArticle.parent = this;
        }

        #region IComparable<Article> Members
        /// <summary>
        /// Compares this Deki Article to another Deki Article using their titles
        /// </summary>
        /// <param name="other">The other deki article</param>
        /// <returns>Returns the difference between the two articles</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.CompareTo(System.String)")]
        public int CompareTo(Article other)
        {
            return this.title.CompareTo(other.Title);
        }

        #endregion
        /// <summary>
        /// Represents this object as a string
        /// </summary>
        public override string ToString()
        {
            return Title;
        }
    }
}