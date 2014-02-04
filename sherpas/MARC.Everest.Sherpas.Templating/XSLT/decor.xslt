<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:sdtc="urn:hl7-org:sdtc"
                xmlns:xhtml="http://www.w3.org/1999/xhtml"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:marc="urn:marc-hi:everest/sherpas/template"
                xmlns:v3="urn:hl7-org:v3"
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
      <xsl:attribute name="id">
        <xsl:value-of select="@id"/>
      </xsl:attribute>
      <xsl:apply-templates/>
    </marc:enumerationTemplate>
  </xsl:template>

  <!-- Class Template -->
  <xsl:template match="element">

    <marc:propertyTemplate traversalName="{marc:CleanElementName(@name)}"  >
      <xsl:if test="@minimumMultiplicity">
        <xsl:attribute name="minOccurs">
          <xsl:value-of select="@minimumMultiplicity"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="@maximumMultiplicity">
        <xsl:attribute name="maxOccurs">
          <xsl:value-of select="@maximumMultiplicity"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:attribute name="conformance">
        <xsl:choose>
          <xsl:when test="@conformance = 'M' or @isMandatory = 'true'">Mandatory</xsl:when>
          <xsl:when test="@minimumMultiplicity = '1'">Populated</xsl:when>
          <xsl:when test="@conformance = 'R'">Required</xsl:when>
          <xsl:otherwise>Optional</xsl:otherwise>
        </xsl:choose>
      </xsl:attribute>

      <!-- Contains? -->
      <xsl:if test="@contains">

        <xsl:variable name="refName" select="@contains"/>
        
        <xsl:variable name="ref" select="//template[@name = $refName or @id = $refName]"/>

        <xsl:choose>
          <xsl:when test="count($ref/element) != 1 or element[1]/@datatype">
            <!-- Inline processing for contains -->
            <xsl:comment>
              From <xsl:value-of select="$refName"/>
            </xsl:comment>
            <xsl:apply-templates select="$ref/element"/>
          </xsl:when>
          <xsl:otherwise>
            <xsl:attribute name="contains">
              <xsl:value-of select="marc:PascalCaseName($ref/@name)"/>
            </xsl:attribute>
            <xsl:if test="not(local-name(parent::node()) = 'choice') and @minimumMultiplicity= 1">
              <marc:validationInstruction>
                <marc:where operator="NCONT" valueRef="{marc:PascalCaseName(marc:CleanElementName(@contains))}">
                  <marc:construct type="MARC.Everest.Sherpas.ResultDetail.TemplateMissingContentResultDetail">
                    <marc:set propertyName="Type" valueRef="MARC.Everest.ResultDetailType.Warning"/>
                    <marc:set propertyName="Property" value="{marc:PascalCaseName(marc:CleanElementName(@name))}"/>
                    <marc:set propertyName="Required" value="{@contains}"/>
                  </marc:construct>
                  
                </marc:where>
              </marc:validationInstruction>
            </xsl:if>
          </xsl:otherwise>
        </xsl:choose>
       
      </xsl:if>

      <xsl:if test="@datatype">
        <xsl:choose>
          <xsl:when test="contains(@datatype, '.')">
            <marc:type name="{substring-before(@datatype, '.')}" flavor="{substring-after(@datatype, '.')}">
              <xsl:if test="vocabulary[@valueSet]">
                <marc:type name="{vocabulary/@valueSet}"/>
              </xsl:if>
            </marc:type>
          </xsl:when>
          <xsl:otherwise>
            <marc:type name="{@datatype}">
              <xsl:if test="vocabulary[@valueSet]">
                <marc:type name="{vocabulary/@valueSet}"/>
              </xsl:if>
            </marc:type>
          </xsl:otherwise>
        </xsl:choose>

      </xsl:if>
      <xsl:apply-templates />

    </marc:propertyTemplate>

  </xsl:template>

  <!-- Include -->
  <xsl:template match="include">
    <xsl:variable name="refName" select="@ref"/>
    <xsl:variable name="ref" select="//rules/template[@name = $refName]"/>
    <xsl:choose>
      <xsl:when test="count($ref/element) = 1 and not($ref/element[1]/@datatype)">
        <marc:propertyTemplate>
          <xsl:attribute name="traversalName">
            <xsl:value-of select="marc:CleanElementName($ref/element[1]/@name)"/>
          </xsl:attribute>
          <xsl:attribute name="ref">
            <xsl:value-of select="$refName"/>
          </xsl:attribute>
        </marc:propertyTemplate>

      </xsl:when>
      <xsl:otherwise>
        <xsl:apply-templates select="$ref/element"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <!-- Attribute -->
  <xsl:template match="attribute">
    <xsl:choose>
      <!-- Describing -->
      <xsl:when test="@name and not(../@datatype or ./@value)">
        <marc:propertyTemplate traversalName="{@name}">
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
            <marc:set variableName="retVal">
              <marc:construct type="MARC.Everest.Sherpas.ResultDetail.TemplatePopulatedProhibitedElementResultDetail">
                <marc:set propertyName="Type" valueRef="MARC.Everest.ResultDetailType.Warning"/>
                <marc:set propertyName="Property" value="{@name}"/>
              </marc:construct>
            </marc:set>
          </marc:where>
        </marc:validationInstruction>
      </xsl:when>
      <xsl:when test="@name and @isOptional = 'false'">
        <marc:validationInstruction>
          <marc:where propertyName="{@name}" operator="EQ" valueRef="null">
            <marc:set variableName="retVal">
              <marc:construct type="MARC.Everest.Sherpas.ResultDetail.TemplateRequiredElementMissingResultDetail">
                <marc:set propertyName="Type" valueRef="MARC.Everest.ResultDetailType.Warning"/>
                <marc:set propertyName="Property" value="{@name}"/>
              </marc:construct>
            </marc:set>
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
                <marc:set propertyName="{.}" value="{../@value}"/>
              </xsl:when>
              <xsl:when test="local-name() = 'name' or (local-name() = 'value' and (../../vocabulary or ../@name)) " ></xsl:when>
              <xsl:otherwise>
                <marc:set propertyName="{local-name()}" value="{.}"/>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:for-each>
        </marc:initialize>

        <xsl:for-each select="@*">

          <xsl:choose>
            <xsl:when test="local-name() = 'isOptional' or local-name() = 'prohibited' or local-name() = 'datatype'"></xsl:when>
            <xsl:when test="local-name() = 'name' and ../@value">
              <marc:validationInstruction>

                <marc:where propertyName="{.}" operator="NE" value="{../@value}">
                  <marc:set variableName="retVal">
                    <marc:construct type="MARC.Everest.Sherpas.ResultDetail.TemplateFixedValueMisMatchResultDetail">
                      <marc:set propertyName="Type" valueRef="MARC.Everest.ResultDetailType.Warning"/>
                      <marc:set propertyName="Property" value="{marc:PascalCaseName(.)}"/>
                      <marc:set propertyName="Expected" value="{../@value}"/>
                      <marc:set propertyName="Actual" valueRef="{.}"/>
                      <marc:set propertyName="IsIgnored" value="true"/>
                    </marc:construct>
                  </marc:set>
                </marc:where>
              </marc:validationInstruction>
            </xsl:when>
            <xsl:when test="local-name() = 'name' or (local-name() = 'value' and (../../vocabulary or ../@name)) " ></xsl:when>
            <xsl:otherwise>
              <marc:validationInstruction>
                <marc:where propertyName="{marc:CleanElementName(local-name())}" operator="NE" value="{.}">
                  <marc:set variableName="retVal">
                    <marc:construct type="MARC.Everest.Sherpas.ResultDetail.TemplateFixedValueMisMatchResultDetail">
                      <marc:set propertyName="Type" valueRef="MARC.Everest.ResultDetailType.Warning"/>
                      <marc:set propertyName="Property" value="{marc:PascalCaseName(local-name())}"/>
                      <marc:set propertyName="Expected" value="{.}"/>
                      <marc:set propertyName="Actual" valueRef="{local-name()}"/>
                      <marc:set propertyName="IsIgnored" value="true"/>
                    </marc:construct>
                  </marc:set>
                </marc:where>
              </marc:validationInstruction>
            </xsl:otherwise>
          </xsl:choose>
        </xsl:for-each>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <!-- Vocabulary Entry (attribute = initialization) -->
  <xsl:template match="vocabulary[not(@valueSet)]">
    
    <marc:initialize>
      <!--
          propertyName="{marc:PascalCaseName(marc:CleanElementName(../@name))}">-->
      <xsl:for-each select="@*">
        <xsl:variable name="name" select="local-name()"/>

        <xsl:if test="not(../../attribute[@name = $name])">
          <marc:set propertyName="{$name}" value="{.}"/>
        </xsl:if>
      </xsl:for-each>
    </marc:initialize>

    <xsl:for-each select="@*">
      <xsl:variable name="name" select="local-name()"/>

      <xsl:if test="not(../../attribute[@name = $name])">
        <marc:validationInstruction>
          <marc:where propertyName="{marc:CleanElementName(local-name())}" operator="NE" value="{.}">
            <marc:set variableName="retVal">
              <marc:construct type="MARC.Everest.Sherpas.ResultDetail.TemplateFixedValueMisMatchResultDetail">
                <marc:set propertyName="Type" valueRef="MARC.Everest.ResultDetailType.Warning"/>
                <marc:set propertyName="Property" value="{marc:PascalCaseName(local-name())}"/>
                <marc:set propertyName="Expected" value="{.}"/>
                <marc:set propertyName="Actual" valueRef="{local-name()}"/>
                <marc:set propertyName="IsIgnored" value="true"/>
              </marc:construct>
            </marc:set>
          </marc:where>
        </marc:validationInstruction>
      </xsl:if>
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

  <!-- An Assertion = Validation Instruction -->
  <xsl:template match="assert">
    <marc:validationInstruction>
      <marc:call method="ValidationContext.EvaluateXPathTest">
        <marc:param value="{@test}"/>
        <marc:return valueRef="x"/>
      </marc:call>
      <marc:where variableName="x" operator="EQ" value="true">
        <marc:set variableName="retVal">
          <marc:construct type="MARC.Everest.Sherpas.ResultDetail.TemplateFormalConstraintViolationResultDetail">
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
          </marc:construct>
        </marc:set>
      </marc:where>
    </marc:validationInstruction>
  </xsl:template>

  <!-- Root Class Template -->
  <xsl:template match="template">
    <xsl:variable name="name" select="@name"/>
    <xsl:variable name="id" select="@id"/>
    <xsl:if test="count(element) = 1 and not(element[1]/@datatype) and count(//*[@ref=$name or @contains = $name or @ref=$id or @contains = $id]) > 0">
      <marc:classTemplate name="{marc:PascalCaseName(@name)}" traversalName="{marc:CleanElementName(./element/@name)}" id="{@id}">
        <marc:baseClass name="{marc:GetBaseClass(./classification/@type)}"/>
        <xsl:apply-templates select="*[local-name() != 'element']"/>
        <xsl:for-each select="element">
          <xsl:apply-templates />
        </xsl:for-each>
      </marc:classTemplate>
    </xsl:if>
  </xsl:template>


  <!-- Choice = Restrict the allowed choices -->
  <xsl:template match="choice">

    <marc:propertyChoiceTemplate>
      <xsl:apply-templates />
    </marc:propertyChoiceTemplate>
    <marc:validationInstruction>
      <marc:where operation="XOR">
        <xsl:for-each select="element">
          <marc:where propertyName="{marc:CleanElementName(@name)}" operator="IS" valueRef="{@contains}"/>
        </xsl:for-each>
        <xsl:for-each select="include">
          <xsl:variable name="refName" select="@ref"/>
          <xsl:variable name="ref" select="//template[@name = $refName]"/>
          <marc:where propertyName="{marc:CleanElementName($ref/element[1]/@name)}" operator="IS" valueRef="{@ref}"/>
        </xsl:for-each>
        <marc:construct type="MARC.Everest.Sherpas.ResultDetail.TemplateNotSupportedChoiceResultDetail">
          <marc:set propertyName="Type" valueRef="MARC.Everest.ResultDetailType.Warning"/>
          <xsl:for-each select="element">
            <marc:set propertyName="AllowedChoice" value="{marc:CleanElementName(@name)} of {@contains}"/>
          </xsl:for-each>
          <xsl:for-each select="element">
            <xsl:variable name="refName" select="@ref"/>
            <xsl:variable name="ref" select="//template[@name = $refName]"/>
            <marc:set propertyName="AllowedChoice" value="{marc:CleanElementName($ref/element[1]/@name)} of {@ref}"/>
          </xsl:for-each>
        </marc:construct>
      </marc:where>
    </marc:validationInstruction>
  </xsl:template>

  <!-- Concept -->
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
    <marc:conceptDomainRef name="{@codeSystem}"/>
  </xsl:template>

  <xsl:template match="copyright" mode="project">
    <marc:copyrightHolder>
      <xhtml:div>
        <xhtml:p>
          <xsl:value-of select="@years"/>
          <xsl:value-of select="@by"/>
        </xhtml:p>
        <xhtml:p>
          <xsl:for-each select="./addrLine">
            <xsl:value-of select="text()"/>
            <xhtml:br/>
          </xsl:for-each>
        </xhtml:p>
      </xhtml:div>
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

  <xsl:template match="example">
    <marc:sampleRendering>
      <xsl:apply-templates mode="sample"/>
    </marc:sampleRendering>
  </xsl:template>
  <xsl:template match="desc">
    <xsl:if test="@language = $language">
      <marc:documentation>
        <xhtml:div>
          <xsl:apply-templates mode="doc"/>
        </xhtml:div>
      </marc:documentation>
    </xsl:if>
  </xsl:template>

  <xsl:template match="*" mode="sample">
    <xsl:element name="v3:{local-name()}" >
      <xsl:apply-templates mode="sample" select="@*|*|text()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="@*" mode="sample">
    <xsl:attribute name="{local-name()}">
      <xsl:value-of select="."/>
    </xsl:attribute>
  </xsl:template>
  <xsl:template match="text()" mode="sample">
    <xsl:value-of select="."/>
  </xsl:template>
  
  <xsl:template match="*" mode="doc">
    <xsl:element name="xhtml:{local-name()}" >
      <xsl:apply-templates mode="doc" select="@*|*|text()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="@*" mode="doc">
    <xsl:attribute name="{local-name()}">
      <xsl:value-of select="."/>
    </xsl:attribute>
  </xsl:template>
  <xsl:template match="text()" mode="doc">
    <xsl:value-of select="."/>
    </xsl:template>
  
  <xsl:template match="@* | *" priority="-1" mode="project"></xsl:template>
  <xsl:template match="@* | *">
    <xsl:apply-templates/>
  </xsl:template>
  <xsl:template match="@* | *" priority="-1"></xsl:template>

</xsl:stylesheet>
