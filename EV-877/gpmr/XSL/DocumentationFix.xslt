<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl" xmlns:mif2="urn:hl7-org:v3/mif2"
                xmlns:mif="urn:hl7-org:v3/mif">


  <!-- Fix Annotations-->
  <xsl:template match="mif:description[not(./mif:text)]">
    <xsl:element name="{concat('mif2:',local-name(.))}" namespace="{$mifNamespace}">
      <mif2:text>
        <html:p xmlns:html="http://www.w3.org/1999/xhtml">
          <xsl:for-each select="*">
            <xsl:apply-templates select="." mode="html"/>
          </xsl:for-each>
        </html:p>
      </mif2:text>
    </xsl:element>
  </xsl:template>

  <xsl:template match="mif:description[not(parent::mif:documentation)]|mif:reason|mif:versioningPolicy|mif:voterComments|mif:text|mif:rationale[not(parent::mif:documentation)]">
    <xsl:element name="{concat('mif2:',local-name(.))}" namespace="{$mifNamespace}">
      <xsl:for-each select="*">
        <xsl:apply-templates select="." mode="html"/>
      </xsl:for-each>
    </xsl:element>
  </xsl:template>

  <xsl:template match="*" mode="html">
    <xsl:element name="{concat('html:',local-name(.))}" namespace="http://www.w3.org/1999/xhtml">
      <xsl:apply-templates select="@*" mode="html"/>
      <xsl:apply-templates select="node()" mode="html"/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="@*|text()|comment()" mode="html">
    <xsl:copy-of select="."/>
  </xsl:template>
  
</xsl:stylesheet>
