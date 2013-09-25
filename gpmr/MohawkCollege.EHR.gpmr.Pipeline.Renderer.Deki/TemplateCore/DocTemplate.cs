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
using System.Xml.Serialization;
using MohawkCollege.EHR.gpmr.COR;
using MohawkCollege.EHR.gpmr.Pipeline.Renderer.Deki.Article;
using System.Threading;
using MARC.Everest.Threading;

namespace MohawkCollege.EHR.gpmr.Pipeline.Renderer.Deki.TemplateCore
{
    /// <summary>
    /// Represents a documentation template
    /// </summary>
    [XmlRoot("docTemplate", Namespace = "http://marc.mohawkcollege.ca/hi")]
    [XmlType(TypeName = "DocTemplate", Namespace = "http://marc.mohawkcollege.ca/hi")]
    public class DocTemplate
    {
        private string name;
        private string author;
        private string output;
        private string version;
        private List<FeatureTemplate> templates;
        private NonParameterizedTemplate globalTemplate;
        internal static bool generateVocabulary = true;


        /// <summary>
        /// Default constructor
        /// </summary>
        public DocTemplate()
        {
        }

        /// <summary>
        /// Thread Worker Class
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public class Worker
        {
            /// <summary>
            /// Fires when the worker stops working
            /// </summary>
            public event EventHandler OnComplete;

            /// <summary>
            /// Feature template that is processing
            /// </summary>
            public FeatureTemplate FeatureTemplate { get; set; }

            /// <summary>
            /// The article collection
            /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            public ArticleCollection ArticleCollection { get; set; }
            //DOC: Documentation Required
            /// <summary>
            /// 
            /// </summary>
            public void Start(object state)
            {
                // Process the global template
                Article.Article art = new MohawkCollege.EHR.gpmr.Pipeline.Renderer.Deki.Article.Article();

                //if (FeatureTemplate.Feature.Contains(".SubSystem"))
                //    System.Diagnostics.Debugger.Break();

                // Process the global template
                
                art.Content = FeatureTemplate.FillTemplate();
                art.Title = FeatureTemplate.GetTitle();
                art.Name = FeatureTemplate.Context is Feature ? (FeatureTemplate.Context as Feature).Name : art.Title;
                art.Abstract = FeatureTemplate.GetAbstract();
                string ParentName = FeatureTemplate.GetPath();
                
                // Modify the article collection
                lock (ArticleCollection)
                {
                    // See if master appears anywhere?
                    if (ParentName == null && ArticleCollection.Find(ParentName) == null)
                        ArticleCollection.Add(art);
                    else if (ParentName == null)
                    {
                        foreach (Article.Article cart in ArticleCollection.Find(ParentName).Children ?? new ArticleCollection())
                            art.Children.Add(cart);

                        ArticleCollection.Remove(ArticleCollection.Find(ParentName));
                        ArticleCollection.Add(art);
                    }
                    else if (ArticleCollection.Find(ParentName) != null)
                    {
                        if (ArticleCollection.Find(ParentName).Children == null)
                            ArticleCollection.Find(ParentName).Children = new ArticleCollection();

                        ArticleCollection.Find(ParentName).Children.Add(art);
                    }
                    else
                    {
                        Article.Article Parent = new MohawkCollege.EHR.gpmr.Pipeline.Renderer.Deki.Article.Article();
                        Parent.Title = ParentName;
                        Parent.Children = new ArticleCollection();
                        ArticleCollection.Add(Parent);
                        Parent.Children.Add(art);
                    }
                }

                if (this.OnComplete != null) OnComplete(this, new EventArgs());
            }
        }

        /// <summary>
        /// Identifies the HTML that goes around all generated pages
        /// </summary>
        [XmlElement("globalTemplate")]
        public NonParameterizedTemplate GlobalTemplate
        {
            get { return globalTemplate; }
            set { globalTemplate = value; }
        }
	
        /// <summary>
        /// Identifies the feature templates in this doc
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement(ElementName = "featureTemplate")]
        public List<FeatureTemplate> Templates
        {
            get { return templates; }
            set { templates = value; }
        }

        /// <summary>
        /// Identifies the style of the site
        /// </summary>
        [XmlElement("style")]
        public string Style { get; set; }

        /// <summary>
        /// Version of this template
        /// </summary>
        [XmlAttribute("version")]
        public string Version
        {
            get { return version; }
            set { version = value; }
        }
	
        /// <summary>
        /// Output MIME type
        /// </summary>
        [XmlAttribute("output")]
        public string Output
        {
            get { return output; }
            set { output = value; }
        }
	
        /// <summary>
        /// The author of this template
        /// </summary>
        [XmlAttribute("author")]
        public string Author
        {
            get { return author; }
            set { author = value; }
        }
	
        /// <summary>
        /// The name of the template
        /// </summary>
        [XmlAttribute("name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Find a feature template
        /// </summary>
        internal FeatureTemplate FindTemplate(string Name, object context)
        {
            if (context is Feature && (context as Feature).Annotations.Find(o => o is SuppressBrowseAnnotation) != null)
            {
                System.Diagnostics.Trace.WriteLine(String.Format("Feature '{0}' won't be published as a SuppressBrowse annotation was found", (context as Feature).Name), "warn");
                return null;
            }

            foreach (FeatureTemplate tpl in Templates)
                if (tpl.Feature == Name && (context == null || tpl.MustHave == null || 
                    (context).GetType().GetProperty(tpl.MustHave) != null && (context).GetType().GetProperty(tpl.MustHave).GetValue(context, null) != null))
                    return tpl;

            return null;
        }

        /// <summary>
        /// Process each feature in the class repository and output them to the article collection
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.CompareTo(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "retVal"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public ArticleCollection Process(ClassRepository rep)
        {

            ArticleCollection artc = new ArticleCollection();

            
            List<Feature> features = new List<Feature>();
            foreach (KeyValuePair<string, Feature> kv in rep)
                features.Add(kv.Value);
            // Sort so classes are processed first
            features.Sort(delegate(Feature a, Feature b)
            {
                if ((a is SubSystem) && !(b is SubSystem)) return -1;
                else if ((b is SubSystem) && !(a is SubSystem)) return 1;
                else if ((a is Class) && !(b is Class)) return 1;
                else if ((b is Class) && !(a is Class)) return -1;
                else return a.GetType().Name.CompareTo(b.GetType().Name);
            });

            //var vocabArticle = new MohawkCollege.EHR.gpmr.Pipeline.Renderer.Deki.Article.Article()
            //{
            //    Title = "Vocabulary",
            //    Children = new ArticleCollection()
            //};
            //vocabArticle.Children.Add(new Article.Article() { Title = "Code Systems" });
            //vocabArticle.Children.Add(new Article.Article() { Title = "Concept Domains" });
            //vocabArticle.Children.Add(new Article.Article() { Title = "Value Sets" });

            //artc.Add(vocabArticle);
            WaitThreadPool wtp = new WaitThreadPool();

            // A thread that does the doohickey thing
            Thread doohickeyThread = new Thread((ThreadStart)delegate()
            {
                string[] hickeythings = { "|", "/", "-", "\\" };
                int hickeyThingCount = 0;
                try
                {
                    while (true)
                    {
                        int cPosX = Console.CursorLeft, cPosY = Console.CursorTop;
                        Console.SetCursorPosition(1, cPosY);
                        Console.Write(hickeythings[hickeyThingCount++ % hickeythings.Length]);
                        Console.SetCursorPosition(cPosX, cPosY);
                        Thread.Sleep(1000);
                    }
                }
                catch { }
            });
            doohickeyThread.Start();

            // Loop through each feature
            foreach (Feature f in features)
            {
                // Find the feature template
                FeatureTemplate ftpl = NonParameterizedTemplate.Spawn(FindTemplate(f.GetType().FullName, f) as NonParameterizedTemplate, this, f) as FeatureTemplate;

                if (ftpl == null) System.Diagnostics.Trace.WriteLine(string.Format("Feature '{0}' won't be published as no feature template could be located", f.Name), "warn");
                else if(f.Annotations.Find(o=>o is SuppressBrowseAnnotation) != null) System.Diagnostics.Trace.WriteLine(String.Format("Feature '{0}' won't be published as a SuppressBrowse annotation was found", f.Name), "warn");
                else if(ftpl.NewPage)
                {
                    System.Diagnostics.Trace.WriteLine(string.Format("Queueing ({1}) '{0}'...", f.Name, f.GetType().Name), "debug");

                    // Create a new worker
                    Worker w = new Worker();
                    w.ArticleCollection = artc;
                    w.FeatureTemplate = ftpl;
                    w.OnComplete += delegate(object sender, EventArgs e)
                    {
                        Worker wrkr = sender as Worker;
                        System.Diagnostics.Trace.WriteLine(String.Format("Rendered ({1}) '{0}'...", (wrkr.FeatureTemplate.Context as Feature).Name, wrkr.FeatureTemplate.Context.GetType().Name), "debug");
                    };
                    wtp.QueueUserWorkItem(w.Start);
                }
            }

            System.Diagnostics.Trace.WriteLine("Waiting for work items to complete...", "debug");
            wtp.WaitOne();
            doohickeyThread.Abort();

            ArticleCollection retVal = new ArticleCollection();
            Article.Article MasterTOC = new MohawkCollege.EHR.gpmr.Pipeline.Renderer.Deki.Article.Article();
            MasterTOC.Children = artc;
            System.Diagnostics.Trace.WriteLine("Creating Table of Contents...", "information");
            PrepareTOC(MasterTOC);
            MasterTOC.Children = null;
            artc.Add(MasterTOC);


            return artc;
        }

        /// <summary>
        /// Generate the TOC
        /// </summary>
        private void PrepareTOC(Article.Article artc)
        {
            // Create a TOC Article
            Article.Article tocArticle = artc;

            FeatureTemplate tocTemplate = NonParameterizedTemplate.Spawn(FindTemplate(tocArticle.GetType().FullName, tocArticle), this, tocArticle) as FeatureTemplate;
            tocArticle.Title = tocArticle.Title ?? tocTemplate.GetTitle();

            // Populate Toc Article
            tocArticle.Content = globalTemplate.FillTemplate(tocTemplate);

            // Create child TOC if they are empty
            if(tocArticle.Children != null)
                for (int i = 0; i < tocArticle.Children.Count; i++)
                    PrepareTOC(tocArticle.Children.Data[i]);

        }
    }
}