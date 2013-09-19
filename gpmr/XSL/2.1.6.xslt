<?xml version="1.0" encoding="UTF-8"?>
<!-- This file is taken from the v3 Generator tool (acquired from http://gforge.hl7.org/gf/project/v3-generator/ )
     and is covered under the terms of the HL7 Basic License.
     
     All modifications to this file are noted below:
     
      * Modified this file to function under XSLT 1.0 
      * Renamed file to 2.1.6 to support GPMR translation process
      * Added correction to the annotations that ensures annotations appear under the <text> element
      * Added template that removes <comment> elements
     -->
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:mif="urn:hl7-org:v3/mif2" xmlns:html="http://www.w3.org/1999/xhtml">
  <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="no"/>
  <xsl:variable name="mifNamespace" select="'urn:hl7-org:v3/mif2'"/>

  <xsl:include href="DocumentationFix.xslt"/>
  <xsl:template match="/comment()">
  </xsl:template>

  <!-- Fix Annotations-->
  <xsl:template match="mif:description[not(./mif:text)]">
    <xsl:element name="{concat('mif:',local-name(.))}" namespace="{$mifNamespace}">
      <mif:text>
        <html:div>
          <xsl:apply-templates select="@*" mode="html"/>
          <xsl:apply-templates select="node()" mode="html"/>
        </html:div>
      </mif:text>
    </xsl:element>
  </xsl:template>

  <xsl:template match="mif:text[not(namespace-uri(./*[1]) = 'http://www.w3.org/1999/xhtml')]">
    <mif:text>
      <html:div>
        <xsl:apply-templates select="@*" mode="html"/>
        <xsl:apply-templates select="node()" mode="html"/>
      </html:div>
    </mif:text>
  </xsl:template>
  <xsl:template match="mif:attribute">
    <mif:attribute>
      <xsl:for-each select="@*|*">
        <xsl:choose>
          <xsl:when test="local-name() = 'conformance'">
            <xsl:choose>
              <xsl:when test=". = 'U'"></xsl:when>
              <xsl:otherwise>
                <xsl:attribute name="conformance">
                  <xsl:value-of select="."/>
                </xsl:attribute>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:when>
          <xsl:otherwise>
            <xsl:apply-templates select="."/>
          </xsl:otherwise>
        </xsl:choose>
        
      </xsl:for-each>
    </mif:attribute>
  </xsl:template>

  <xsl:template match="mif:property"></xsl:template>
  <!-- Standard rules -->
  <xsl:template match="node()|@*">
    <xsl:copy>
      <xsl:apply-templates select="node()|@*"/>
    </xsl:copy>
  </xsl:template>
  <xsl:template match="text()[normalize-space(.)='']"/>
	<xsl:template match="@schemaVersion[.='2.1.6']">
    <xsl:attribute name="schemaVersion">
      <xsl:value-of select="'2.1.4'"/>
    </xsl:attribute>
	</xsl:template>
	<xsl:template match="mif:supplementedObject|mif:description[not(parent::mif:documentation)]|mif:reason|mif:existingContent|mif:suggestedReplacement|mif:resolutionComments|mif:renderingNotes|mif:notation|
                      mif:disclaimer|mif:licenseTerms|mif:versioningPolicy|mif:voterComments|mif:text|mif:combinedText|mif:rationale[not(parent::mif:documentation)]|mif:activityDiagramFigure|mif:figure">
    <xsl:copy>
      <xsl:apply-templates mode="AddXHTML" select="node()|@*"/>
    </xsl:copy>
	</xsl:template>
  <xsl:template match="*" mode="AddXHTML">
    <xsl:element name="{concat('html:',local-name(.))}" namespace="http://www.w3.org/1999/xhtml">
      <xsl:apply-templates select="@*" mode="AddXHTML"/>
      <xsl:apply-templates select="node()" mode="AddXHTML"/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="@*|text()|comment()" mode="AddXHTML">
    <xsl:copy-of select="."/>
  </xsl:template>
  
</xsl:stylesheet>
