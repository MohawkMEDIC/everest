<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:sdtc="urn:hl7-org:sdtc"
                xmlns:xhtml="http://www.w3.org/1999/xhtml"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:marc="urn:marc-hi:everest/sherpas/template"
>
    <xsl:output method="xml" indent="yes"/>

  <xsl:include href="CommonFunctions.xslt"/>

  <xsl:param name="language" select="'en-US'"/>
  
  
  <xsl:template match="decor">
      <marc:Template>
        <marc:projectInfo>
          <xsl:apply-templates select="project"/>
        </marc:projectInfo>

        <xsl:apply-templates select="terminology/valueSet | rules/template"/>
      </marc:Template>
    </xsl:template>

  <!-- Project Level Templates - This is information that will end up on the AssemblyInfo -->
  <xsl:template match="project">
    <marc:name>
      <xsl:value-of select="name[@language = $language]"/>
    </marc:name>
    <xsl:apply-templates mode="project"/>
  </xsl:template>

  <!-- Value Set -->
  <xsl:template match="valueSet[./*]">
    <marc:enumerationTemplate>
      <xsl:attribute name="name">
        <xsl:value-of select="marc:PascalCaseName(@name)"/>
      </xsl:attribute>
      <xsl:attribute name="valueSetId">
        <xsl:value-of select="@id"/>
      </xsl:attribute>
      <xsl:apply-templates/>
    </marc:enumerationTemplate>
  </xsl:template>

  <!-- Class Template -->
  <xsl:template match="element">
    
    <marc:propertyTemplate name="{marc:PascalCaseName(marc:CleanElementName(@name))}" minOccurs="{@minimumMultiplicity}" maxOccurs="{@maximumMultiplicity}" >
      <xsl:attribute name="conformance">
        <xsl:choose>
          <xsl:when test="@conformance = 'M' or @isMandatory = 'true'">Mandatory</xsl:when>
          <xsl:when test="@minimumMultiplicity = '1'">Populated</xsl:when>
          <xsl:when test="@conformance = 'R'">Required</xsl:when>
          <xsl:otherwise>Optional</xsl:otherwise>
        </xsl:choose>
      </xsl:attribute>
      
      <xsl:if test="@datatype">
        <marc:type name="{@datatype}">
          <xsl:if test="vocabulary[@valueSet]">
            <marc:type name="{vocabulary/@valueSet}"/>
          </xsl:if>
        </marc:type>
      </xsl:if>
      
      <!-- Contains? -->
      <xsl:if test="@contains">
        <marc:contains type="{@contains}"/>
        <marc:validationInstruction>
          <marc:when propertyName="{marc:PascalCaseName(marc:CleanElementName(@name))}" operator="NCONT" valueRef="{marc:PascalCaseName(marc:CleanElementName(@name))}">
            <marc:emit type="MARC.Everest.Sherpas.ResultDetail.TemplateMissingContentResultDetail">
              <marc:set propertyName="Type" valueRef="MARC.Everest.ResultDetailType.Warning"/>
              <marc:set propertyName="Property" value="{marc:PascalCaseName(marc:CleanElementName(@name))}"/>
              <marc:set propertyName="Required" value="{@contains}"/>
            </marc:emit>
          </marc:when>
        </marc:validationInstruction>
      </xsl:if>
      <xsl:apply-templates />

    </marc:propertyTemplate>

  </xsl:template>

  <xsl:template match="attribute">
    <xsl:choose>
      <!-- Describing -->
      <xsl:when test="@name and not(../@datatype)">
        <marc:propertyTemplate name="{marc:PascalCaseName(@name)}">
          <xsl:if test="@isOptional='true'">
            <xsl:attribute name="conformance">Optional</xsl:attribute>
          </xsl:if>
          <xsl:if test="@prohibited='true'">
            <xsl:attribute name="maxOccurs">0</xsl:attribute>
          </xsl:if>
          <xsl:if test="./vocabulary">
            <marc:type name="CS">
              <marc:type name="{./vocabulary/@valueSet}"/>
            </marc:type>
          </xsl:if>
        </marc:propertyTemplate>
      </xsl:when>
      <xsl:when test="@name and @prohibited = 'true'">
        <marc:validationInstruction>
          <marc:where propertyName="{@name}" operator="NE" valueRef="null">
            <marc:emit type="MARC.Everest.Sherpas.ResultDetail.TemplatePopulatedProhibitedElementResultDetail">
              <marc:set propertyName="Type" valueRef="MARC.Everest.ResultDetailType.Warning"/>
              <marc:set propertyName="Property" value="{@name}"/>
            </marc:emit>
          </marc:where>
        </marc:validationInstruction>
      </xsl:when>
      <xsl:when test="@name and @isOptional = 'false'">
        <marc:validationInstruction>
          <marc:where propertyName="{@name}" operator="EQ" valueRef="null">
            <marc:emit type="MARC.Everest.Sherpas.ResultDetail.TemplateRequiredElementMissingResultDetail">
              <marc:set propertyName="Type" valueRef="MARC.Everest.ResultDetailType.Warning"/>
              <marc:set propertyName="Property" value="{@name}"/>
            </marc:emit>
          </marc:where>
        </marc:validationInstruction>
      </xsl:when>      
      <!-- Setting -->
      <xsl:otherwise>
        <marc:initialize>
          <!--
          propertyName="{marc:PascalCaseName(marc:CleanElementName(../@name))}">-->
          <xsl:for-each select="@*">
            <xsl:choose>
              <xsl:when test="local-name() = 'isOptional' or local-name() = 'prohibited' or local-name() = 'datatype'"></xsl:when>
              <xsl:when test="local-name() = 'name' and ../@value">
                <marc:set propertyName="{marc:PascalCaseName(.)}" value="{../@value}"/>
              </xsl:when>
              <xsl:when test="local-name() = 'name'"></xsl:when>
              <xsl:otherwise>
                <marc:set propertyName="{marc:PascalCaseName(local-name())}" value="{.}"/>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:for-each>
        </marc:initialize>
        
          <xsl:for-each select="@*">

              <xsl:choose>
              <xsl:when test="local-name() = 'isOptional' or local-name() = 'prohibited' or local-name() = 'datatype'"></xsl:when>
              <xsl:when test="local-name() = 'name' and ../@value">
                <marc:validationInstruction>

                  <marc:where propertyName="{marc:PascalCaseName(.)}" operator="NE" value="{../@value}">
                    <marc:emit type="MARC.Everest.Sherpas.ResultDetail.TemplateFixedValueMisMatchResultDetail">
                      <marc:set propertyName="Type" valueRef="MARC.Everest.ResultDetailType.Warning"/>
                      <marc:set propertyName="Property" value="{marc:PascalCaseName(.)}"/>
                      <marc:set propertyName="Expected" value="{../@value}"/>
                      <marc:set propertyName="Actual" valueRef="{marc:PascalCaseName(.)}"/>
                      <marc:set propertyName="IsIgnored" value="true"/>
                    </marc:emit>
                  </marc:where>
                </marc:validationInstruction>
              </xsl:when>
              <xsl:when test="local-name() = 'name'"></xsl:when>
              <xsl:otherwise>
                <marc:validationInstruction>
                  <marc:where propertyName="{marc:PascalCaseName(marc:CleanElementName(local-name()))}" operator="NE" value="{.}">
                    <marc:emit type="MARC.Everest.Sherpas.ResultDetail.TemplateFixedValueMisMatchResultDetail">
                      <marc:set propertyName="Type" valueRef="MARC.Everest.ResultDetailType.Warning"/>
                      <marc:set propertyName="Property" value="{marc:PascalCaseName(local-name())}"/>
                      <marc:set propertyName="Expected" value="{.}"/>
                      <marc:set propertyName="Actual" valueRef="{marc:PascalCaseName(local-name())}"/>
                      <marc:set propertyName="IsIgnored" value="true"/>
                    </marc:emit>
                  </marc:where>
                </marc:validationInstruction>
              </xsl:otherwise>
            </xsl:choose>


          </xsl:for-each>
          
      </xsl:otherwise>
    </xsl:choose>

  </xsl:template>

  <xsl:template match="vocabulary[not(@valueSet)]">

    <marc:initialize>
      <!--
          propertyName="{marc:PascalCaseName(marc:CleanElementName(../@name))}">-->
      <xsl:for-each select="@*">
          <marc:set propertyName="{marc:PascalCaseName(local-name())}" value="{.}"/>
      </xsl:for-each>
    </marc:initialize>

    <xsl:for-each select="@*">
          <marc:validationInstruction>
            <marc:where propertyName="{marc:PascalCaseName(marc:CleanElementName(local-name()))}" operator="NE" value="{.}">
              <marc:emit type="MARC.Everest.Sherpas.ResultDetail.TemplateFixedValueMisMatchResultDetail">
                <marc:set propertyName="Type" valueRef="MARC.Everest.ResultDetailType.Warning"/>
                <marc:set propertyName="Property" value="{marc:PascalCaseName(local-name())}"/>
                <marc:set propertyName="Expected" value="{.}"/>
                <marc:set propertyName="Actual" valueRef="{marc:PascalCaseName(local-name())}"/>
                <marc:set propertyName="IsIgnored" value="true"/>
              </marc:emit>
            </marc:where>
          </marc:validationInstruction>
    </xsl:for-each>
    
  </xsl:template>
  
  <!-- Equivalent to setting a temporary variable -->
  <xsl:template match="let">
    <marc:validationInstruction>
      <marc:call method="ValidationContext.SetXPathVariable">
        <marc:param value="{@name}"/>
        <marc:param value="{@value}"/>
      </marc:call> 
    </marc:validationInstruction>
  </xsl:template>

  <xsl:template match="assert">
    <marc:validationInstruction>
      <marc:call method="ValidationContext.EvaluateXPathTest">
        <marc:param value="{@test}"/>
        <marc:return valueRef="x"/>
      </marc:call>
      <marc:where variableName="x" operator="EQ" value="true">
        <marc:emit type="MARC.Everest.Sherpas.ResultDetail.TemplateFormalConstraintViolationResultDetail">
          <xsl:choose>
            <xsl:when test="@role = 'error'">
              <marc:set propertyName="Type" valueRef="MARC.Everest.ResultDetailType.Warning"/>
            </xsl:when>
            <xsl:when test="@role = 'warning'">
              <marc:set propertyName="Type" valueRef="MARC.Everest.ResultDetailType.Warning"/>
            </xsl:when>
            <xsl:otherwise>
              <marc:set propertyName="Type" valueRef="MARC.Everest.ResultDetailType.Information"/>
            </xsl:otherwise>
          </xsl:choose>
          <marc:set propertyName="Message" value="{marc:CopyInnerXmlAsString(.)}"/>
        </marc:emit>
      </marc:where>
    </marc:validationInstruction>
  </xsl:template>
  
  <!-- Root Class Template -->
  <xsl:template match="template">
    <marc:classTemplate name="{marc:PascalCaseName(@name)}" baseClass="{marc:GetBaseClass(./classification/@type)}" traversalName="{marc:CleanElementName(./element/@name)}">
      <xsl:apply-templates select="*[local-name() != 'element']"/>
      <xsl:for-each select="element">
        <xsl:apply-templates />
      </xsl:for-each>
    </marc:classTemplate>
  </xsl:template>
  
  <xsl:template match="concept">
    <marc:literal>
      <xsl:attribute name="literalName">
        <xsl:value-of select="marc:PascalCaseName(@displayName)"/>
      </xsl:attribute>
      <xsl:attribute name="supplierDomain">
        <xsl:value-of select="@codeSystem"/>
      </xsl:attribute>
      <xsl:attribute name="code">
        <xsl:value-of select="@code"/>
      </xsl:attribute>
      <xsl:attribute name="displayName">
        <xsl:value-of select="@displayName"/>
      </xsl:attribute>
    </marc:literal>
  </xsl:template>

  <xsl:template match="completeCodeSystem">
    <marc:conceptDomainRef value="{@codeSystem}"/>
  </xsl:template>
  
  <xsl:template match="copyright" mode="project">
    <marc:copyrightHolder>
      <xhtml:p><xsl:value-of select="@years"/> <xsl:value-of select="@by"/></xhtml:p>
      <xhtml:p>
        <xsl:for-each select="./addrLine">
          <xsl:value-of select="text()"/><xhtml:br/>
        </xsl:for-each>
      </xhtml:p>
    </marc:copyrightHolder>
  </xsl:template>

  <xsl:template match="author" mode="project">
    <marc:originalAuthor>
      <xsl:value-of select="text()"/> (<xsl:value-of select="@email"/>)
    </marc:originalAuthor>
  </xsl:template>

  <xsl:template match="version" mode="project">
    <marc:version>
      <xsl:value-of select="@date"/>
    </marc:version>
  </xsl:template>

  <xsl:template match="desc">
    <xsl:if test="@language = $language">
      <marc:documentation>
        <xsl:apply-templates mode="doc"/>
      </marc:documentation>
    </xsl:if>
  </xsl:template>

  <xsl:template match="@* | *" mode="doc">
    <xsl:copy-of select="."/>
  </xsl:template>
  
  <xsl:template match="@* | *" priority="-1" mode="project"></xsl:template>
  <xsl:template match="@* | *">
    <xsl:apply-templates/>
  </xsl:template>
  <xsl:template match="@* | *" priority="-1"></xsl:template>
  
</xsl:stylesheet>
