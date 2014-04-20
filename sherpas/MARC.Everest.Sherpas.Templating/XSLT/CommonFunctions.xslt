<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
                xmlns:marc="urn:marc-hi:everest/sherpas/template"
>
  <msxsl:script implements-prefix="marc" language="CSharp">
    <msxsl:using namespace="System.Text.RegularExpressions"/>
    <msxsl:using namespace="System.Text"/>
    <msxsl:using namespace="System"/>
    <msxsl:using namespace="System.Collections.Generic"/>
    <msxsl:using namespace="System.Collections"/>

    <!--<msxsl:using namespace="System.Linq"/>-->
    <!--<msxsl:assembly name="System.Linq"/>-->
    <![CDATA[
    
    public bool matches(String text, String regex)
    {
      Regex re = new Regex(regex);
      return re.Match(text).Success;
    }
    //HACK: What madness is this?
    public String CopyInnerXmlAsString(Object node)
    {
      foreach(var nd in node as IEnumerable)
        return nd.GetType().GetProperty("InnerXml").GetValue(nd, null).ToString();
       return String.Empty;
    }
    
    public String CleanElementName(String name)
    {
      String retVal = name;
      if(retVal.Contains(":"))
        retVal = retVal.Substring(retVal.IndexOf(":") + 1);
      if(retVal.Contains("["))
        retVal = retVal.Substring(0, retVal.IndexOf("["));
      return retVal;
    }
    
    public String GetBaseClass(string classification)
    {
      switch(classification)
      {
        case "cdadocumentlevel":
          return "POCD_MT000040UV.ClinicalDocument";
         case "cdasectionlevel":
         case "cdaentrylevel":
         case "cdaheaderlevel":
          return "MARC.Everest.Interfaces.IGraphable";
         default:
          return "System.Object";
      }
    }
    
    public String PascalCaseName(String original)
    {
    
      if (original == null || original.Length == 0) return original;
      original = original.Trim();
      string retVal = "";
      foreach (string s in original.Split(' ', '/'))
      {
          if (s.Length > 1)
              retVal += s.ToUpper().Substring(0, 1) + s.Substring(1);
          else
              retVal += s.ToUpper() + "_";
     }
      return MakeFriendly(retVal);
    }
    
    public String MakeFriendly(String original)
    {
        if (original == null || original.Length == 0) return original;

        string retVal = original;
        foreach (char c in original)
        {
            Regex validChars = new Regex("[A-Za-z0-9_]");
            if (!validChars.IsMatch(c.ToString()))
                retVal = retVal.Replace(c.ToString(), "");
        }

        // Remove non-code chars
        //foreach (char c in nonCodeChars)
        //    retVal = retVal.Replace(c.ToString(), "");
        //retVal = retVal.Replace("-", "_");


        // Check for numeric start
        Regex re = new Regex("^[0-9]");
        if (re.Match(retVal).Success)
            retVal = "_" + retVal;

        return retVal.Length == 0 ? null : retVal;
    }
    ]]>
  </msxsl:script>
</xsl:stylesheet>
