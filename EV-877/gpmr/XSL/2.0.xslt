<?xml version="1.0" encoding="UTF-8"?>
<!-- This file is taken from the v3 Generator tool (acquired from http://gforge.hl7.org/gf/project/v3-generator/ )
     and is covered under the terms of the HL7 Basic License.
     
     All modifications to this file are noted below:
     
      * Modified this file to function under XSLT 1.0 
      * Renamed file to 2.0 to support GPMR translation process
     -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:mif="urn:hl7-org:v3/mif"
                xmlns:mif2="urn:hl7-org:v3/mif2"
                exclude-result-prefixes="mif">
  <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>
  <xsl:include href="DocumentationFix.xslt"/>
  
  <xsl:variable name="mifNamespace" select="'urn:hl7-org:v3/mif2'"/>

  
  <xsl:template match="mif:staticModel">
    <mif2:staticModel schemaVersion="2.1.4">
      <xsl:for-each select="*|@*">
        <xsl:choose>
          <xsl:when test="local-name() = 'realmNamespace'"></xsl:when>
          <xsl:otherwise>
            <xsl:apply-templates select="."/>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:for-each>
    </mif2:staticModel>
  </xsl:template>

  <xsl:template match="mif:attribute">
    <mif2:attribute>
      <xsl:for-each select="*|@*">
        <xsl:choose>
          <xsl:when test="local-name() = 'supplierVocabulary'">
            <mif2:vocabulary>
              <xsl:for-each select="@*|*">
                <xsl:apply-templates select="." mode="WithoutStrength"/>
              </xsl:for-each>
            </mif2:vocabulary>
          </xsl:when>
          <xsl:otherwise>
            <xsl:apply-templates select="."/>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:for-each>
    </mif2:attribute>  
  </xsl:template>

  <!-- HACK: Some transform puts an XMLNS into the vocab domain-->
  <xsl:template match="mif:vocabularyDomain | vocabularyDomain" mode="WithoutStrength">
    <mif2:conceptDomain>
      <xsl:for-each select="*|@*">
        <xsl:apply-templates select="."/>
      </xsl:for-each>
    </mif2:conceptDomain>
  </xsl:template>

  
  <!-- HACK: Some transform puts an XMLNS into the vocab domain-->
  <xsl:template match="mif:vocabularyDomain | vocabularyDomain">
    <mif2:conceptDomain>
      <xsl:for-each select="*|@*">
        <xsl:apply-templates select="."/>
      </xsl:for-each>
    </mif2:conceptDomain>
  </xsl:template>
  
  <xsl:template match="mif:enumerationValues">
    <mif2:enumerationValue>
      <xsl:value-of select="text()"/>
    </mif2:enumerationValue>
  </xsl:template>
  
  <xsl:template match="*/@codingStrength" mode="WithoutStrength">
  </xsl:template>

  <xsl:template match="mif:code" mode="WithoutStrength">
    <mif2:code>
      <xsl:if test="@code">
        <xsl:attribute name="code">
          <xsl:value-of select="@code"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="@codeSystemName">
        <xsl:attribute name="codeSystemName">
          <xsl:value-of select="@codeSystemName"/>
        </xsl:attribute>
      </xsl:if>
    </mif2:code>
  </xsl:template>
  
  <xsl:template match="mif:supplierVocabulary" >

    <mif2:definingVocabulary>
      <xsl:for-each select="*|@*">
        <xsl:apply-templates select="."/>
      </xsl:for-each> 
    </mif2:definingVocabulary>
    
  </xsl:template>

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

 
  
  <xsl:template match="mif:childClass|mif:parentClass">
    <mif2:childClass>
      <xsl:for-each select="*|@*">
        <xsl:choose>
          <xsl:when test="local-name() = 'className'">
            <xsl:attribute name="name">
              <xsl:value-of select="."/>
            </xsl:attribute>
          </xsl:when>
          <xsl:when test="local-name() = 'isMandatory'"></xsl:when>
          <xsl:otherwise>
            <xsl:apply-templates select="."/>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:for-each>
    </mif2:childClass>
  </xsl:template>

  <xsl:template match="mif:commonModelElementRef" priority="2">
    <xsl:element name="mif2:commonModelElementRef" namespace="{$mifNamespace}">
      <xsl:for-each select="@*">
        <xsl:apply-templates select="."/>
      </xsl:for-each>
      <xsl:for-each select="./mif:cmet">
        <xsl:choose>
          <xsl:when test="local-name() = 'cmet'">
            <!--<xsl:if test="./@name">
              <xsl:attribute name="cmetName">
                <xsl:value-of select="./@name"/>
              </xsl:attribute>
            </xsl:if>-->
            <xsl:for-each select="./mif:argument">
              <mif2:argument>
                <xsl:apply-templates select="."/>
              </mif2:argument>
            </xsl:for-each>
          </xsl:when>
          <xsl:otherwise>
            <xsl:apply-templates select="."/>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:for-each>
      <xsl:for-each select="*[local-name() != 'cmet']">
        <xsl:apply-templates select="."/>
      </xsl:for-each>
    </xsl:element>
  </xsl:template>
  
  <xsl:template match="mif:historyItem">
    <xsl:for-each select="*|@*">
      <xsl:choose>
        <xsl:when test="local-name() = 'modifiedForPackageVersion'">
        </xsl:when>
        <xsl:otherwise>
          <xsl:apply-templates select="."/>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:for-each>
  </xsl:template>

  <xsl:template match="mif:participantClassSpecialization|mif:specialization">
    <mif2:choiceItem>
      <xsl:for-each select="*|@*">
        <xsl:apply-templates select="."/>
      </xsl:for-each>
    </mif2:choiceItem>
  </xsl:template>
  <xsl:template match="@*|text()|comment()" priority="0">
    <xsl:copy-of select="."/>
  </xsl:template>
  <xsl:template match="*" >
    <xsl:element name="{concat('mif2:', local-name(.))}" namespace="{$mifNamespace}">
      <xsl:apply-templates select="@*"/>
      <xsl:apply-templates select="node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="text()[normalize-space(.)='']"/>
</xsl:stylesheet>
