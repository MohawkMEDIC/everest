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
  <xsl:template match="valueSet[./conceptList]">
    <marc:enumerationTemplate>
      <xsl:attribute name="name">
        <xsl:value-of select="marc:PascalCaseName(@name)"/>
      </xsl:attribute>
      <marc:id>
        <xsl:value-of select="@id"/>
      </marc:id>
      <xsl:apply-templates/>
    </marc:enumerationTemplate>
  </xsl:template>

  <xsl:template match="element[contains(@name,'[')]">
    <xsl:comment>
      Cannot represent <xsl:value-of select="@name"/>
    </xsl:comment>
  </xsl:template>
  <!-- Class Template -->
  <xsl:template match="element[not(contains(@name,'['))]">

    <marc:propertyTemplate traversalName="{marc:CleanElementName(@name)}" >
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
            <xsl:attribute name="contains">
              <xsl:choose>
                <xsl:when test="$ref">
                  <xsl:value-of select="marc:PascalCaseName($ref/@name)"/>
                </xsl:when>
                <xsl:otherwise>
                  <xsl:value-of select="marc:PascalCaseName($refName)"/>
                </xsl:otherwise>
              </xsl:choose>
            </xsl:attribute>
            <!-- Inline processing for contains -->
            <xsl:comment>
              From <xsl:value-of select="$refName"/>
            </xsl:comment>
            <!--<xsl:apply-templates select="$ref/element"/>-->
          </xsl:when>
          <xsl:when test="$ref">
            <xsl:attribute name="contains">
              <xsl:value-of select="marc:PascalCaseName($ref/@name)"/>
            </xsl:attribute>
          </xsl:when>
          <xsl:otherwise>
            <xsl:attribute name="contains">
              <xsl:value-of select="marc:PascalCaseName($refName)"/>
            </xsl:attribute>
            <!--z<xsl:if test="not(local-name(parent::node()) = 'choice') and @minimumMultiplicity= 1">
              <marc:validationInstruction>
                <marc:where operator="NOT">
                  <marc:where operator="IS" propertyName="{marc:PascalCaseName(marc:CleanElementName(parent::node()/@name))}"  valueRef="{marc:PascalCaseName(marc:CleanElementName(@contains))}"/>
                  <marc:call variableName="retVal" method="Add">
                    <marc:param>
                      <marc:construct type="MARC.Everest.Sherpas.ResultDetail.TemplateMissingContentResultDetail">
                        <marc:set propertyName="Type" valueRef="MARC.Everest.Connectors.ResultDetailType.Warning"/>
                        <marc:set propertyName="Property" value="{marc:PascalCaseName(marc:CleanElementName(@name))}"/>
                        <marc:set propertyName="Required" value="{@contains}"/>
                      </marc:construct>
                    </marc:param>
                  </marc:call>                  
                </marc:where>
              </marc:validationInstruction>
            </xsl:if>-->
          </xsl:otherwise>
        </xsl:choose>
       
      </xsl:if>

      <xsl:if test="@datatype">
        <xsl:choose>
          <xsl:when test="contains(@datatype, '.')">
            <marc:type name="{substring-before(@datatype, '.')}" flavor="{substring-after(@datatype, '.')}">
              <xsl:if test="vocabulary[@valueSet or @domain]">
                <marc:type name="{vocabulary/@valueSet | vocabulary/@domain}"/>
              </xsl:if>

            </marc:type>
          </xsl:when>
          <xsl:otherwise>
            <marc:type name="{@datatype}">
              <xsl:if test="vocabulary[@valueSet or @domain]">
                <marc:type name="{vocabulary/@valueSet | vocabulary/@domain}"/>
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
        <marc:formalConstraint message="Property {marc:PascalCaseName(marc:CleanElementName(../@name))}.{marc:PascalCaseName(@name)} is prohibited">
          <marc:where propertyName="{@name}" operator="EQ" valueRef="null">
          </marc:where>
        </marc:formalConstraint>
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
              <marc:formalConstraint message="Property {marc:PascalCaseName(marc:CleanElementName(../../@name))}.{marc:PascalCaseName(.)} must carry value {../@value}">
                <marc:where propertyName="{.}" operator="EQ" value="{../@value}">
                </marc:where>
              </marc:formalConstraint>
            </xsl:when>
            <xsl:when test="local-name() = 'name' or (local-name() = 'value' and (../../vocabulary or ../@name)) " ></xsl:when>
            <xsl:otherwise>
              <marc:formalConstraint message="Property {marc:PascalCaseName(marc:CleanElementName(../../@name))}.{marc:PascalCaseName(local-name())} must carry value {.}">
                <marc:where propertyName="{marc:CleanElementName(local-name())}" operator="EQ" value="{.}">
                </marc:where>
              </marc:formalConstraint>
            </xsl:otherwise>
          </xsl:choose>
        </xsl:for-each>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <!-- Vocabulary Entry (attribute = initialization) -->
  <xsl:template match="vocabulary[not(@valueSet) and not(@domain)]">

    <xsl:if test="count(../vocabulary) = 1">
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
    </xsl:if>
    <xsl:for-each select="@*">
      <xsl:variable name="name" select="local-name()"/>

      <xsl:if test="not(../../attribute[@name = $name])">
        <marc:formalConstraint message="Property {marc:PascalCaseName(marc:CleanElementName(../../@name))}.{marc:PascalCaseName(local-name())} must carry value {.}">
          <marc:where propertyName="{marc:CleanElementName(local-name())}" operator="EQ" value="{.}">
          </marc:where>
        </marc:formalConstraint>
      </xsl:if>
    </xsl:for-each>
  </xsl:template>

  <!-- Equivalent to setting a temporary variable -->
  <xsl:template match="let">
    <!--<marc:validationInstruction>
      <marc:call variableName="ValidationContext" method="SetXPathVariable">
        <marc:param value="{@name}"/>
        <marc:param value="{@value}"/>
      </marc:call>
    </marc:validationInstruction>-->
  </xsl:template>

  <!-- An Assertion = Validation Instruction -->
  <xsl:template match="assert">
    <!--<marc:validationInstruction>
      <marc:call variableName="ValidationContext" method="EvaluateXPathTest">
        <marc:param value="{@test}"/>
        <marc:return valueRef="x"/>
      </marc:call>
      <marc:where variableName="x" operator="EQ" valueRef="true">
        <marc:call variableName="retVal" method="Add">
          <marc:param>
            <marc:construct type="MARC.Everest.Sherpas.ResultDetail.TemplateFormalConstraintViolationResultDetail">
              <xsl:choose>
                <xsl:when test="@role = 'error'">
                  <marc:set propertyName="Type" valueRef="MARC.Everest.Connectors.ResultDetailType.Warning"/>
                </xsl:when>
                <xsl:when test="@role = 'warning'">
                  <marc:set propertyName="Type" valueRef="MARC.Everest.Connectors.ResultDetailType.Warning"/>
                </xsl:when>
                <xsl:otherwise>
                  <marc:set propertyName="Type" valueRef="MARC.Everest.Connectors.ResultDetailType.Information"/>
                </xsl:otherwise>
              </xsl:choose>
              <marc:set propertyName="Message" value="{marc:CopyInnerXmlAsString(.)}"/>
            </marc:construct>
          </marc:param>
        </marc:call>
      </marc:where>
    </marc:validationInstruction>-->
  </xsl:template>

  <!-- Root Class Template -->
  <xsl:template match="template">
    <xsl:variable name="name" select="@name"/>
    <xsl:variable name="id" select="@id"/>
    <xsl:comment>
      <xsl:value-of select="@name"/>
      Has 
      <xsl:value-of select="count(//*[(@ref=$name or @contains = $name or @ref=$id or @contains = $id)])"/>
    </xsl:comment>
    <xsl:if test="count(//*[(@ref=$name or @contains = $name or @ref=$id or @contains = $id)]) > 0">
      <marc:classTemplate name="{marc:PascalCaseName(@name)}">
        <xsl:if test="count(element) = 1">
          <xsl:attribute name="traversalName">
            <xsl:value-of select="marc:CleanElementName(./element/@name)"/>
          </xsl:attribute>
        </xsl:if>
        <xsl:for-each select=".//element[marc:CleanElementName(@name) = 'templateId' and attribute/@root]">
          <marc:id>
            <xsl:value-of select="attribute/@root"/>
          </marc:id>
        </xsl:for-each>
        <marc:baseClass name="{marc:GetBaseClass(./classification/@type)}"/>
        <xsl:apply-templates select="*[local-name() != 'element']"/>
        <xsl:choose>
          <xsl:when test="count(element) = 1">
            <xsl:for-each select="element">
              <xsl:apply-templates />
            </xsl:for-each>
          </xsl:when>
          <xsl:otherwise>
            <xsl:apply-templates select="*[local-name() = 'element']"/>
          </xsl:otherwise>
        </xsl:choose>
      </marc:classTemplate>
    </xsl:if>
  </xsl:template>


  <xsl:template match="choice" priority="-1">
    <xsl:comment>
      Choice was removed because no choice elements actually exist
    </xsl:comment>
  </xsl:template>
  
  <!-- Choice = Restrict the allowed choices -->
  <xsl:template match="choice[count(./element[not(contains(@name,'['))])>0]">

    <marc:propertyChoiceTemplate minOccurs="{@minimumMultiplicity}" maxOccurs="{@maximumMultiplicity}">
      <xsl:apply-templates />
      <!--<marc:validationInstruction>
        <marc:where operator="NXOR">
          <xsl:for-each select="element[@contains]">
            <marc:where propertyName="{marc:CleanElementName(@name)}" operator="IS" valueRef="{@contains}"/>
          </xsl:for-each>
          <xsl:for-each select="include">
            <xsl:variable name="refName" select="@ref"/>
            <xsl:variable name="ref" select="//template[@name = $refName]"/>
            <marc:where propertyName="{marc:CleanElementName($ref/element[1]/@name)}" operator="IS" valueRef="{@ref}"/>
          </xsl:for-each>
          <marc:call variableName="retVal" method="Add">
            <marc:param>
              <marc:construct type="MARC.Everest.Sherpas.ResultDetail.TemplateNotSupportedChoiceResultDetail">
                <marc:set propertyName="Type" valueRef="MARC.Everest.Connectors.ResultDetailType.Warning"/>
                <xsl:for-each select="element">
                  <marc:set propertyName="AllowedChoice" value="{marc:CleanElementName(@name)} of {@contains}"/>
                </xsl:for-each>
                <xsl:for-each select="element">
                  <xsl:variable name="refName" select="@ref"/>
                  <xsl:variable name="ref" select="//template[@name = $refName]"/>
                  <marc:set propertyName="AllowedChoice" value="{marc:CleanElementName($ref/element[1]/@name)} of {@ref}"/>
                </xsl:for-each>
              </marc:construct>
            </marc:param>
          </marc:call>
        </marc:where>
      </marc:validationInstruction>-->
    </marc:propertyChoiceTemplate>
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
        <div>
          <xsl:apply-templates mode="doc"/>
        </div>
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
    <xsl:element name="{local-name()}" >
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
