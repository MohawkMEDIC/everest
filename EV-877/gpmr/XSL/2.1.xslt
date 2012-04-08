<?xml version="1.0" encoding="UTF-8"?>
<!-- This file is taken from the v3 Generator tool (acquired from http://gforge.hl7.org/gf/project/v3-generator/ )
     and is covered under the terms of the HL7 Basic License.
     
     All modifications to this file are noted below:
     
      * Modified this file to function under XSLT 1.0 
      * Renamed file to 2.1 to support GPMR translation process
      * Added correction that translates MT-deprecated artifact type to MT
     -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:mif="urn:hl7-org:v3/mif"
                xmlns:mif2="urn:hl7-org:v3/mif2"
                xmlns:html="http://www.w3.org/1999/xhtml"
                exclude-result-prefixes="mif">
  <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>
  <xsl:include href="DocumentationFix.xslt"/>
  <xsl:variable name="mifNamespace" select="'urn:hl7-org:v3/mif2'"/>

  <xsl:template match="*/@artifact">
    <xsl:attribute name="artifact">
      <xsl:choose>
        <xsl:when test=". = 'MT-deprecated'">
          <xsl:value-of select="'MT'"/>
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="."/>

        </xsl:otherwise>
      </xsl:choose>
    </xsl:attribute>
  </xsl:template>
  
  <xsl:template match="*/@schemaVersion">
    <xsl:attribute name="schemaVersion">
      <xsl:value-of select="'2.1.4'"/>
    </xsl:attribute>
  </xsl:template>
  
  
  <xsl:template match="@*|text()|comment()" priority="0">
    <xsl:copy-of select="."/>
  </xsl:template>
  <xsl:template match="mif2:text">
    <mif2:text>
      <html:div>
        <xsl:apply-templates select="@*" mode="html"/>
        <xsl:apply-templates select="node()" mode="html"/>
      </html:div>
    </mif2:text>
  </xsl:template>
  <xsl:template match="*" >
    <xsl:element name="{concat('mif2:', local-name(.))}" namespace="{$mifNamespace}">
      <xsl:apply-templates select="@*"/>
      <xsl:apply-templates select="node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="text()[normalize-space(.)='']"/>
</xsl:stylesheet>
