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

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Deki.Article
{

    /// <summary>
    /// Identifies that an article has been published to a collection
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Repository"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Published")]
    public delegate void ArticlePublishedHandler(Article PublishedArticle, ArticleCollection Repository);

    /// <summary>
    /// Represents a collection of articles
    /// </summary>
    public class ArticleCollection : ICollection<Article>
    {

        /// <summary>
        /// Comparator for articles
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public class ArticleComparator : IComparer<Article>
        {
            #region IComparer<Article> Members
            /// <summary>
            /// Compare <paramref name="x"/> to <paramref name="y"/> based on their titles
            /// </summary>
            /// <param name="x">The first article</param>
            /// <param name="y">The second article</param>
            /// <returns>Returns the difference between the two articles</returns>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.CompareTo(System.String)")]
            public int Compare(Article x, Article y)
            {
                return x.Title.CompareTo(y.Title);
            }

            #endregion
        }

        List<Article> articles = new List<Article>();

        internal List<Article> Data
        {
            get { return articles; }
        }

        /// <summary>
        /// Fired when a new article is added to this collection
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
        public event ArticlePublishedHandler ArticleAdded;
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
        public event ArticlePublishedHandler ArticleRemoved;

        #region ICollection<Article> Members
        //DOC: Documentation Required
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(Article item)
        {
            articles.Add(item);
            if (ArticleAdded != null) ArticleAdded(item, this);
            articles.Sort(new ArticleComparator());
        }

        /// <summary>
        /// Clear this collection
        /// </summary>
        public void Clear()
        {
            foreach (Article a in this)
                if (ArticleRemoved != null) ArticleRemoved(a, this);
            articles.Clear();
        }

        /// <summary>
        /// Check if this collection contains an item
        /// </summary>
        public bool Contains(Article item)
        {
            return articles.Contains(item);
        }

        /// <summary>
        /// Copy to an array
        /// </summary>
        public void CopyTo(Article[] array, int arrayIndex)
        {
            articles.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Get the count of items
        /// </summary>
        public int Count
        {
            get { return articles.Count; }
        }

        /// <summary>
        /// Returns true if this collection is readonly
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Remove an item
        /// </summary>
        public bool Remove(Article item)
        {
            if (ArticleRemoved != null) ArticleRemoved(item, this);
            return articles.Remove(item);
        }

        #endregion

        #region IEnumerable<Article> Members

        /// <summary>
        /// Get an enumerator
        /// </summary>
        public IEnumerator<Article> GetEnumerator()
        {
            return articles.GetEnumerator();
        }

        #endregion

        /// <summary>
        /// Find an article
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.IndexOf(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "e"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Article")]
        public Article Find(string ArticleTitle)
        {

            ArticleCollection scope = this;

            // Find 
            while (scope != null && ArticleTitle.Contains("/"))
            {
                Article cArticle = scope.Find(ArticleTitle.Substring(0, ArticleTitle.IndexOf("/")));
                if (cArticle == null) return null; // Done here so return null
                scope = cArticle.Children;
                ArticleTitle = ArticleTitle.Substring(ArticleTitle.IndexOf("/") + 1);
            }

            // Find in current scope
            try
            {
                Article searchItem = new Article();
                searchItem.Title = ArticleTitle;
                return scope.Data[scope.Data.BinarySearch(searchItem, new ArticleComparator())];
            }
            catch (Exception)
            {
                return null;
            }

        }


        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
